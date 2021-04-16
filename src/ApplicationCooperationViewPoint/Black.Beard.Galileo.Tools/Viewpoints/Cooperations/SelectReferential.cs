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
        private List<ReferentialEntity> _lastQueryItems;

        public SelectReferential(Files.ModelRepository modelRepository = null)
        {

            InitializeComponent();

            this._models = modelRepository;

            CompositionListView.Columns.AddRange
            (
                new ColumnHeader[]
                {
                    new ColumnHeader() { Name = "Name", Text = "Name", Width = 100 },
                    new ColumnHeader() { Name = "Label", Text = "Label" },
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
                _lastQueryItems = r.GetReferentials<ReferentialEntity>(v.Definition.File.Parent.Models).ToList();

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
                var o = items.Where(c => c.Name.Contains(txt) || c.Label.Contains(txt));
                if (!o.Any())
                    o = items.Where(c => c.Name.DamerauLevenshteinDistance(txt) <= 2 || c.Label.DamerauLevenshteinDistance(txt) <= 2);

                items = o;

            }

            if (items.Any())
            {

                Dictionary<string, ListViewGroup> _groups = new Dictionary<string, ListViewGroup>();
                foreach (var item in items)
                {
                    if (!_groups.TryGetValue(item.Target, out ListViewGroup group))
                        _groups.Add(item.Target, group = new ListViewGroup(item.TypeEntity + " " + item.Target, HorizontalAlignment.Left));

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

        private void label1_Click(object sender, EventArgs e)
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

        public HashSet<string> GetSelectedKeys()
        {
            return _selectedKeys;
        }

        public void SetSelectedKeys(HashSet<string> keys)
        {

            if (keys != null)
                foreach (var item in keys)
                {
                    var q = new ResolveQuery(item);
                    var i = q.GetReferentials<ReferentialEntity>(this._models).FirstOrDefault();
                    if (i != null)
                    {
                        if (!_itemschecked.TryGetValue(i.Id, out ReferentialEntity e))
                            _itemschecked.Add(i.Id, i);
                    }
                }

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

                    if (!_itemschecked.ContainsKey(s.Id))
                        _itemschecked.Add(s.Id, s);

                }
                else
                {

                    if (_itemschecked.ContainsKey(s.Id))
                        _itemschecked.Remove(s.Id);

                }
            }
        }



        Dictionary<string, ReferentialEntity> _itemschecked = new Dictionary<string, ReferentialEntity>();
        private HashSet<string> _selectedKeys = new HashSet<string>();
        private readonly Files.ModelRepository _models;

        private void ValidateButton_Click_1(object sender, EventArgs e)
        {

            _selectedKeys.Clear();
            foreach (var item in _itemschecked.Values)
            {
                string key = item.GetReference().ToString();
                _selectedKeys.Add(key);
            }

            this.Close();

        }
    }


}
