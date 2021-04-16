using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Files.Viewpoints;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bb.Galileo.Viewpoints.Cooperations
{

    public class ConceptItem : TreeNode
    {

        public ViewpointModelItem Viewpoint { get; }

        public RelationshipDefinition Relationship { get; }

        public ConceptItem(ViewpointModelItem item, int level, CooperationViewpoint config)
        {

            this.Viewpoint = item;

            BuildCaption(level, item.Definition.Name);

            foreach (var subItem in item.Children)
                this.Nodes.Add(new ConceptItem(subItem, level + 1, config));

        }

        private void BuildCaption(int level, string name)
        {

            string levelText = string.Empty;

            if (level == 0)
                levelText = "Root " + name;
            else
                levelText = "Sub " + level + " " + name;

            this.Text = levelText;

        }

    }



}
