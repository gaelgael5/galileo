using System.Collections.Generic;

namespace Bb.Galileo.Files.Viewpoints
{
    public class CooperationRootElement : CooperationBase
    {

        public CooperationRootElement()
        {
            Children = new List<CooperationRelationship>();
        }


        public List<CooperationRelationship> Children { get; set; }

    }


}

