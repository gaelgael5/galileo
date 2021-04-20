using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Viewpoints;
using System.Windows.Forms;

namespace Bb.Galileo.Viewpoints.Cooperations
{
    public class ConceptItemEntity : ConceptItem
    {


        public ConceptItemEntity(ViewpointModelItem config, ReferentialEntity entity) 
            : base(config)
        {
            this.CurrentItem = entity;
            this.Name = entity.Name;
            this.Text = entity.Label ?? entity.Name;
        }

        public ReferentialEntity CurrentItem { get; }

    
    }

}
