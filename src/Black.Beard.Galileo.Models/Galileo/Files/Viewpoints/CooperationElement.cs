using System.Collections.Generic;

namespace Bb.Galileo.Files.Viewpoints
{


    public class CooperationElement : CooperationBase
    {

        public CooperationElement()
        {
            Children = new List<CooperationRelationship>();
        }


        public List<CooperationRelationship> Children { get; set; }

    }


}

