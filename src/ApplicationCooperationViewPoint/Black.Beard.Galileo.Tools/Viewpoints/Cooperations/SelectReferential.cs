using Bb.Galileo.Files;
using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Viewpoints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bb.Galileo.Viewpoints.Cooperations
{
    public partial class SelectReferential : Form
    {

        public SelectReferential(Files.ModelRepository modelRepository = null)
        {

            InitializeComponent();
            this._models = modelRepository;

            CompositionListView.Columns.AddRange
            (
                new ColumnHeader[]
                {
                    new ColumnHeader() { Name = "Name", Text = "Name", Width = 100 },
                    new ColumnHeader() { Name = "Label", Text = "Label", Width = 700  },
                }
            );

            CompositionListView.View = View.Details;
            CompositionListView.Alignment = ListViewAlignment.Left;
            CompositionListView.CheckBoxes = true;

        }

        private void ConceptsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (ConceptsTreeView.SelectedNode is ConceptItem c)
            {

                var v = c.Viewpoint;

                var r = v.Definition.GetReference();
                r.Kind = ElementEnum.Entity;

                _lastQueryItems = r.GetReferentials(v.Definition.File.Parent.Models).OfType<ReferentialEntity>().ToList();

                RefreshItems();

            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            RefreshItems();
        }

        private void RefreshItems()
        {

            CompositionListView.Items.Clear();

            IEnumerable<ReferentialEntity> items = _lastQueryItems;
            var txt = searchEntitiesTextBox.Text;
            if (!string.IsNullOrEmpty(txt))
            {
                var o = items.Where(c => c.Name.ToLower().Contains(txt.ToLower()) || c.Label.ToLower().Contains(txt.ToLower()));
                if (!o.Any())
                    o = items.Where(c => c.Name.DamerauLevenshteinDistance(txt) <= 2 || c.Label.DamerauLevenshteinDistance(txt) <= 3);

                items = o;

            }

            if (items.Any())
            {

                Dictionary<string, ListViewGroup> _groups = new Dictionary<string, ListViewGroup>();
                foreach (var item in items)
                {
                    if (!_groups.TryGetValue(item.TargetName, out ListViewGroup group))
                        _groups.Add(item.TargetName, group = new ListViewGroup(item.TypeEntity + " " + item.TargetName, HorizontalAlignment.Left));

                    var c = _itemschecked.ContainsKey(item.Id);

                    CompositionListView.Items.Add(new ListViewItem(new string[] { item.Name, item.Label }, group) { Tag = item, Checked = c });

                }

            }

        }

        private void SelectReferential_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        public void SetViewpoint(CooperationViewpoint config)
        {

            var viewpointModel = config.GetViewpointModel();

            foreach (ViewpointModelItem item in viewpointModel.Children)
                ConceptsTreeView.Nodes.Add(new ConceptItem(item, 0, config));

        }

        public Dictionary<string, CooperationCheckedItem> GetSelectedKeys()
        {
            return _itemschecked;
        }

        public void SetSelectedKeys(HashSet<string> keys)
        {

            //if (keys != null)
            //    foreach (var item in keys)
            //    {
            //        var q = new ResolveQuery(item);
            //        var i = q.GetReferentials(this._models).OfType<ReferentialEntity>().FirstOrDefault();
            //        if (i != null)
            //        {
            //            if (!_itemschecked.TryGetValue(i.Id, out CooperationCheckedItem e))
            //                _itemschecked.Add(i.Id, new CooperationCheckedItem() { });
            //        }
            //    }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void CompositionListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

            if (e.Item.Tag is ReferentialEntity s)
            {
                if (e.Item.Checked)
                {
                    int levels = Count(ConceptsTreeView.SelectedNode);
                    if (!_itemschecked.TryGetValue(s.Id, out CooperationCheckedItem i))
                    {
                        var x = new CooperationCheckedItem()
                        {
                            Item = s,
                            Level = ConceptsTreeView.SelectedNode.Level,
                            MaxLevel = levels,
                        };
                        _itemschecked.Add(s.Id, x);
                    }
                    else
                    {
                        i.Level = ConceptsTreeView.SelectedNode.Level;
                        i.MaxLevel = levels;
                    }
                }
                else
                {
                    if (_itemschecked.ContainsKey(s.Id))
                        _itemschecked.Remove(s.Id);
                }
            }
        }

        private int Count(TreeNode selectedNode)
        {
            int result = selectedNode.Level;
            foreach (TreeNode item in selectedNode.Nodes)
                result = Math.Max(result, Count(item));
            return result;

        }

        private void ValidateButton_Click_1(object sender, EventArgs e)
        {

            //_selectedKeys.Clear();
            //foreach (var item in _itemschecked.Values)
            //{
            //    string key = item.Item.GetReference().ToString();
            //    _selectedKeys.Add(new KeyValuePair<int, string>(item.Level, key));
            //}
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private List<KeyValuePair<int, string>> _selectedKeys = new List<KeyValuePair<int, string>>();
        private readonly Files.ModelRepository _models;
        public Dictionary<string, CooperationCheckedItem> _itemschecked = new Dictionary<string, CooperationCheckedItem>();

        private List<ReferentialEntity> _lastQueryItems;

    }



    public class CooperationCheckedItem
    {

        public ReferentialEntity Item { get; set; }

        public int Level { get; internal set; }
        public int MaxLevel { get; internal set; }
    }

}
