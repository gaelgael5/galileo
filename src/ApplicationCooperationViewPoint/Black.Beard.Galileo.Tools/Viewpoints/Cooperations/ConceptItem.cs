using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Files.Viewpoints;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bb.Galileo.Viewpoints.Cooperations
{

    public class ConceptItem : TreeNode
    {

        public ConceptItem(ViewpointModelItem item)
        {
            this.Viewpoint = item;
            this.Name = item.Definition.Name;
            this.Text = item.Definition.Label;
            this.ImageIndex = 2;
            this.SelectedImageIndex = 2;
        }

        public ViewpointModelItem Viewpoint { get; }
        public string LastSearchEntity { get; internal set; }
        public string LastSearchEntity2 { get; internal set; }

    }

}
