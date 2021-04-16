using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Files.Viewpoints;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bb.Galileo.Viewpoints.Cooperations
{
    public class ConceptItem : TreeNode
    {


        public EntityDefinition Definition { get; }

        public RelationshipDefinition Relationship { get; }

        public ConceptItem(CooperationConcept item, int level, CooperationViewpoint config)
        {

            this.Definition = config.File.Parent.Models.GetEntityDefinition(item.Name);

            BuildCaption(level, item.Name);

            foreach (var subItem in item.Children)
                this.Nodes.Add(new ConceptItem(subItem, level + 1, item, config));

        }

        public ConceptItem(CooperationRootElement item, int level, CooperationViewpoint config)
        {

            this.Definition = config.File.Parent.Models.GetEntityDefinition(item.Name);

            BuildCaption(level, item.Name);

            foreach (var subItem in item.Children)
                this.Nodes.Add(new ConceptItem(subItem, level + 1, item, config));

        }

        public ConceptItem(CooperationElement item, int level, CooperationConcept parent, CooperationViewpoint config)
        {

            this.Relationship = config.File.Parent.Models.GetRelationshipDefinition(item.Name);
            if (Relationship != null)
            {

                if (Relationship.Origin.Name == parent.Name)
                    this.Definition = Relationship.GetTargetDefinition();

                else if (Relationship.Target.Name == parent.Name)
                    this.Definition = Relationship.GetOriginDefinition();

                if (this.Definition != null)
                {

                    BuildCaption(level, this.Definition.Name);

                    foreach (var subItem in item.Children)
                        this.Nodes.Add(new ConceptItem(subItem, level + 1, Definition, config));
                }

            }

        }

        internal void AddReference(RelationshipDefinition r)
        {
            
            if (Definition != null)
            {

                if (r.Origin.Name == Definition.Name)
                    this._references.Add(new ReferenceItem() { SourceDefinition = Definition, Target = r.GetTargetDefinition() });

                else if (r.Target.Name == Definition.Name)
                    this._references.Add(new ReferenceItem() { SourceDefinition = Definition, Target = r.GetOriginDefinition() });

                foreach (ConceptItem c in this.Nodes)
                    c.AddReference(r);
            }

        }

        public ConceptItem(CooperationRelationship item, int level, CooperationRootElement parent, CooperationViewpoint config)
        {

            this.Relationship = config.File.Parent.Models.GetRelationshipDefinition(item.Name);
            if (Relationship != null)
            {

                if (Relationship.Origin.Name == parent.Name)
                    this.Definition = Relationship.GetTargetDefinition();

                else if (Relationship.Target.Name == parent.Name)
                    this.Definition = Relationship.GetOriginDefinition();

                if (this.Definition != null)
                    BuildCaption(level, this.Definition.Name);

            }

        }

        public ConceptItem(CooperationRelationship item, int level, EntityDefinition parent, CooperationViewpoint config)
        {

            this.Relationship = config.File.Parent.Models.GetRelationshipDefinition(item.Name);
            if (Relationship != null)
            {

                if (Relationship.Origin.Name == parent.Name)
                    this.Definition = Relationship.GetTargetDefinition();

                else if (Relationship.Target.Name == parent.Name)
                    this.Definition = Relationship.GetOriginDefinition();

                if (this.Definition != null)
                    BuildCaption(level, this.Definition.Name);

            }

        }

        private void BuildCaption(int level, string name)
        {

            string levelText = string.Empty;

            if (level == 0)
                levelText = "Root";
            else
                levelText = "Sub " + level;

            this.Text = levelText;

        }

        private List<ReferenceItem> _references = new List<ReferenceItem>(10);

    }



}
