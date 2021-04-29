using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Viewpoints;
using System.Windows.Forms;

namespace Bb.Galileo.Viewpoints.Cooperations
{
    public class ConceptItemRelationship : ConceptItem
    {


        public ConceptItemRelationship(ViewpointModelItem config, RelationshipItem entity)
            : base(config)
        {
            this.CurrentItem = entity;
            this.Name = entity.Relationship.Name;
            this.Text = entity.Relationship.Label ?? entity.Relationship.Name;
            this.Tag = entity;
            this.ImageIndex = 0;
            this.SelectedImageIndex = 0;
        }

        public RelationshipItem CurrentItem { get; }

    }

}
