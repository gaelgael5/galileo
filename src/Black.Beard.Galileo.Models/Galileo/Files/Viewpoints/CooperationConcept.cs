using System.Collections.Generic;

namespace Bb.Galileo.Files.Viewpoints
{

    public class CooperationConcept : CooperationBase
    {

        public CooperationConcept()
        {
            Children = new List<CooperationElement>();
        }

        public List<CooperationElement> Children { get; set; }

    }

}

