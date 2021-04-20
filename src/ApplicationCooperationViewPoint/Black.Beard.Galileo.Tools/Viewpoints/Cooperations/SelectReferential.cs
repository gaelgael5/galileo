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
                    new ColumnHeader() { Name = "Type", Text = "Name", Width = 200 },
                    new ColumnHeader() { Name = "Name", Text = "Name", Width = 300 },
                    new ColumnHeader() { Name = "Label", Text = "Label", Width = 400  },
                }
            );

            CompositionListView.View = View.Details;
            CompositionListView.Alignment = ListViewAlignment.Left;
            CompositionListView.CheckBoxes = true;

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;


            RefreshItems();
        }

        private void RefreshItems()
        {

            if (_lastQueryItems == null)
                return;

            CompositionListView.Items.Clear();

            IEnumerable<ReferentialEntity> items = _lastQueryItems;
            var txt = searchEntitiesTextBox.Text;
            if (!string.IsNullOrEmpty(txt))
            {
                var o = items.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.ToLower().Contains(txt.ToLower())
                                     || (!string.IsNullOrEmpty(c.Label) && c.Label.ToLower().Contains(txt.ToLower())));
                if (!o.Any())
                    o = items.Where(c => c.Name.DamerauLevenshteinDistance(txt) <= 2 || c.Label.DamerauLevenshteinDistance(txt) <= 3);

                items = o;

            }

            if (items.Any())
            {

                foreach (var item in items)
                {

                    TreeNode[] selected = new TreeNode[0];
                    if (this._lastFilterEntitySelected != null)
                        selected = this._lastFilterEntitySelected.Nodes.Find(item.Name, false);

                    CompositionListView.Items
                        .Add
                        (new ListViewItem(new string[]
                         {
                            item.TypeEntity,
                            item.Name,
                            item.Label
                         })
                        {
                            Tag = item,
                            Checked = selected != null && selected.Length > 0
                        });
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
                ConceptsTreeView.Nodes.Add(new ConceptItem(item));

        }

        public ViewpointProjectionEntities GetSelectedKeys()
        {

            var entities = new ViewpointProjectionEntities();

            foreach (ConceptItem item in ConceptsTreeView.Nodes)
            {

                foreach (ConceptItemEntity item2 in item.Nodes)
                {

                    var e1 = new ViewpointProjectionEntity()
                    {
                        Entity = item2.CurrentItem,
                        Kind = item.Viewpoint.Kind,
                    };

                    foreach (ConceptItemEntity item3 in item2.Nodes)
                    {
                        var e2 = new ViewpointProjectionEntity()
                        {
                            Entity = item3.CurrentItem,
                            Kind = item2.Viewpoint.Kind,
                        };
                        e1.Children.Add(e2);
                    }

                    entities.Entities.Add(e1);
                }

            }

            return entities;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (this._lastFilterEntitySelected != null)
                this._lastFilterEntitySelected.LastSearchEntity = searchEntitiesTextBox.Text;

            timer1.Enabled = true;
        }

        private void ConceptsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (ConceptsTreeView.SelectedNode is ConceptItemEntity f)
            {
                this._lastFilterEntitySelected = f;
                var view = f.Viewpoint;

                var filter = view.Definition.Name;

                var list = new List<ReferentialEntity>(1000);
                foreach (var item in view.Children)
                {

                    var r1 = item.Relationship.GetReference();
                    r1.Kind = ElementEnum.Relationship;

                    var r2 = item.Definition.GetReference();
                    r2.Kind = ElementEnum.Entity;

                    var relationships = r1.GetReferentials(view.Definition.File.Parent.Models).OfType<ReferentialRelationship>();

                    if (item.Relationship.Origin.Name == filter)
                        relationships = relationships.Where(c => c.Origin.Name == f.Name);
                    else
                        relationships = relationships.Where(c => c.Target.Name == f.Name);

                    var relations = new HashSet<string>(relationships.Select(c => c.Name));

                    list.AddRange
                        (
                            r2.GetReferentials(view.Definition.File.Parent.Models)
                                .OfType<ReferentialEntity>()
                                .Where(c => relations.Contains(c.Name))
                        );

                }

                _lastQueryItems = list;
                searchEntitiesTextBox.Text = this._lastFilterEntitySelected.LastSearchEntity;

                RefreshItems();

            }
            else if (ConceptsTreeView.SelectedNode is ConceptItem c)
            {

                this._lastFilterEntitySelected = c;
                var view = c.Viewpoint;

                var r = view.Definition.GetReference();
                r.Kind = ElementEnum.Entity;

                _lastQueryItems = r.GetReferentials(view.Definition.File.Parent.Models)
                                   .OfType<ReferentialEntity>()
                                   .ToList();

                searchEntitiesTextBox.Text = this._lastFilterEntitySelected.LastSearchEntity;

                RefreshItems();

            }

        }

        private void CompositionListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

            if (e.Item.Tag is ReferentialEntity entity)
            {
                if (this._lastFilterEntitySelected != null)
                {
                    var result = this._lastFilterEntitySelected.Nodes.Find(entity.Name, false);
                    if (e.Item.Checked)
                    {
                        if (result == null || result.Length == 0)
                        {
                            var p = new ConceptItemEntity(this._lastFilterEntitySelected.Viewpoint, entity);
                            this._lastFilterEntitySelected.Nodes.Add(p);
                        }
                    }
                    else
                    {
                        if (result != null && result.Length == 1)
                            this._lastFilterEntitySelected.Nodes.Remove(result[0]);
                    }

                    if (this._lastFilterEntitySelected.Nodes.Count > 0)
                        this._lastFilterEntitySelected.Expand();

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
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private readonly Files.ModelRepository _models;
        private List<ReferentialEntity> _lastQueryItems;
        private ConceptItem _lastFilterEntitySelected;
    }



    public class CooperationCheckedItem
    {

        public ReferentialEntity Item { get; set; }

        public int Level { get; internal set; }
        public int MaxLevel { get; internal set; }
    }

}
