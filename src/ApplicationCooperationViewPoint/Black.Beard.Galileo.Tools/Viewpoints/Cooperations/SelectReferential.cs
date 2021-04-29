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
                    new ColumnHeader() { Name = "Type", Text = "Type", Width = 200 },
                    new ColumnHeader() { Name = "Name", Text = "Name", Width = 300 },
                    new ColumnHeader() { Name = "Label", Text = "Label", Width = 400  },
                }
            );

            RelationshiplistView.Columns.AddRange
            (
                new ColumnHeader[]
                {
                    new ColumnHeader() { Name = "Relationship", Text = "Relationship", Width = 200 },
                    new ColumnHeader() { Name = "Name", Text = "Name", Width = 200 },
                    new ColumnHeader() { Name = "Target", Text = "Target", Width = 200  },
                    new ColumnHeader() { Name = "Description", Text = "Description", Width = 400  },
                }
            );

            CompositionListView.View = View.Details;
            CompositionListView.Alignment = ListViewAlignment.Left;
            CompositionListView.CheckBoxes = true;

            RelationshiplistView.View = View.Details;
            RelationshiplistView.Alignment = ListViewAlignment.Left;
            RelationshiplistView.CheckBoxes = true;

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Refreshing();

        }

        private void Refreshing()
        {
            refreshInProgress = true;
            try
            {
                RefreshItems();
                RefreshItems2();
            }
            finally
            {
                refreshInProgress = false;
            }
        }

        #region Show

        private void RefreshItems()
        {

            if (_lastQueryCompositionItems == null)
                return;

            CompositionListView.Items.Clear();

            IEnumerable<ReferentialEntity> items = _lastQueryCompositionItems;
            var txt = searchEntitiesTextBox.Text.ToLower();
            if (!string.IsNullOrEmpty(txt))
            {
                var o = items.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.ToLower().Contains(txt)
                                     || (!string.IsNullOrEmpty(c.Label) && c.Label.ToLower().Contains(txt)));
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

        private void RefreshItems2()
        {

            if (_lastQueryReferenceItems == null)
                return;

            RelationshiplistView.Items.Clear();

            IEnumerable<RelationshipItem> items = _lastQueryReferenceItems;
            var txt = searchEntitiesTextBox2.Text;
            if (!string.IsNullOrEmpty(txt))
            {
                var o = items.Where(c => !string.IsNullOrEmpty(c.Entity.Name) && c.Entity.Name.ToLower().Contains(txt.ToLower())
                                     || (!string.IsNullOrEmpty(c.Entity.Label) && c.Entity.Label.ToLower().Contains(txt.ToLower())));
                if (!o.Any())
                    o = items.Where(c => c.Entity.Name.DamerauLevenshteinDistance(txt) <= 2 || c.Entity.Label.DamerauLevenshteinDistance(txt) <= 3);

                items = o;

            }

            if (items.Any())
            {

                foreach (var item in items)
                {

                    TreeNode[] selected = new TreeNode[0];
                    if (this._lastFilterEntitySelected != null)
                        selected = this._lastFilterEntitySelected.Nodes.Find(item.Entity.Name, false);


                    var label = item.Relationship.Name;
                    if (label != item.Relationship.Label)
                        label += " : " + item.Relationship.Label;

                    RelationshiplistView.Items
                        .Add
                        (new ListViewItem(new string[]
                         {
                            item.Relationship.TypeEntity,
                            label,
                            item.Entity.Name,
                            item.Relationship.Description,
                         })
                        {
                            Tag = item,
                            Checked = selected != null && selected.Length > 0
                        });
                }

            }

        }

        #endregion Show

        private void SelectReferential_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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

                    foreach (ConceptItem item3 in item2.Nodes)
                    {
                        if (item3 is ConceptItemEntity ce)
                        {

                            var e2 = new ViewpointProjectionEntity()
                            {
                                Entity = ce.CurrentItem,
                                Kind = item2.Viewpoint.Kind,
                            };
                            e1.Children.Add(e2);
                        }
                        else if (item3 is ConceptItemRelationship cr)
                        {
                            var e2 = new ViewpointProjectionRelationship()
                            {
                                Relationship = cr.CurrentItem.Relationship,
                                TargetEntity = cr.CurrentItem.Entity,
                                Kind = item2.Viewpoint.Kind,
                            };
                            e1.Relationships.Add(e2);
                        }
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


        #region selection

        private void ConceptsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (ConceptsTreeView.SelectedNode is ConceptItemEntity f)
            {

                this._lastFilterEntitySelected = f;
                var parent = f.Tag as ReferentialEntity;
                var view = f.Viewpoint;

                ManageSubComposition(f, parent, view);
                ManageSubRelationship(parent, view);

            }
            else if (ConceptsTreeView.SelectedNode is ConceptItem c)
                ManageComposition(c);

            Refreshing();

        }

        private void ManageSubRelationship(ReferentialEntity parent, ViewpointModelItem view)
        {
            List<ReferentialEntity> targetFilters = new List<ReferentialEntity>();
            foreach (ConceptItem concept in ConceptsTreeView.Nodes)
                foreach (TreeNode entity in concept.Nodes)
                {
                    if (entity.Tag is ReferentialEntity e)
                        targetFilters.Add(e);
                }

            var list2 = new List<RelationshipItem>(1000);
            foreach (ReferenceItem reference in view.References)
            {
                var def = reference.RelationshipDefinition;
                IEnumerable<ReferentialRelationship> children = parent.GetRelationships(def).ToList();
                foreach (var item in children)
                {
                    var e1 = item.GetTargetEntity(def.Target.Name);
                    if (e1 != null)
                    {

                        if (targetFilters.Any(c => c.Name == e1.Name))
                            list2.Add(new RelationshipItem(item, e1));

                    }

                }

            }
            _lastQueryReferenceItems = list2;
            searchEntitiesTextBox2.Text = this._lastFilterEntitySelected.LastSearchEntity2;

        }

        private void ManageComposition(ConceptItem c)
        {
            this._lastFilterEntitySelected = c;
            var view = c.Viewpoint;

            var r = view.Definition.GetReference();
            r.Kind = ElementEnum.Entity;

            _lastQueryCompositionItems = r.GetReferentials(view.Definition.File.Parent.Models)
                               .OfType<ReferentialEntity>()
                               .ToList();

            searchEntitiesTextBox.Text = this._lastFilterEntitySelected.LastSearchEntity;


        }

        private void ManageSubComposition(ConceptItemEntity f, ReferentialEntity parent, ViewpointModelItem view)
        {
            var referential = view.Definition.File.Parent.Models;

            var filter = view.Definition.Name;

            var list = new List<ReferentialEntity>(1000);
            foreach (var item in view.Children)
            {
                Files.Schemas.RelationshipDefinition itemDef = item.Relationship;
                var children = parent.GetTargetEntities(itemDef).ToList();
                list.AddRange(children);
            }

            _lastQueryCompositionItems = list;
            searchEntitiesTextBox.Text = this._lastFilterEntitySelected.LastSearchEntity;
            
        }

        #endregion selection


        private void CompositionListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

            if (refreshInProgress)
                return;

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

        private void RelationshiplistView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

            if (refreshInProgress)
                return;

            if (e.Item.Tag is RelationshipItem entity)
            {
                if (this._lastFilterEntitySelected != null)
                {
                    var result = this._lastFilterEntitySelected.Nodes.Find(entity.Relationship.Name, false);
                    if (e.Item.Checked)
                    {
                        if (result == null || result.Length == 0)
                        {
                            var p = new ConceptItemRelationship(this._lastFilterEntitySelected.Viewpoint, entity);
                            this._lastFilterEntitySelected.Nodes.Add(p);
                        }
                    }
                    else
                    {
                        if (result != null && result.Length > 0)
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
        private List<ReferentialEntity> _lastQueryCompositionItems;
        private List<RelationshipItem> _lastQueryReferenceItems;
        private ConceptItem _lastFilterEntitySelected;
        private bool refreshInProgress = false;

    }

}
