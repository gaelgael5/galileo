using Bb.Galileo.Files.Datas;
using System.Windows.Forms;

namespace Bb.Galileo.Viewpoints.Cooperations
{
    public class ConceptItemRelationship : TreeNode
    {

        public ConceptItemRelationship(ReferentialRelationship entity)
        {
            this.CurrentItem = entity;
            this.Text = entity.Name;
        }

        public ReferentialRelationship CurrentItem { get; }

    }

}
