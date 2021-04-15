using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Galileo.Files.Viewpoints
{

    public class CooperationViewpoint : CooperationBase
    {

        public CooperationViewpoint()
        {
            Concepts = new List<CooperationConcept>();
            Elements = new List<CooperationRootElement>();
            References = new List<CooperationRelationship>();
        }


        public List<CooperationConcept> Concepts { get; set; }

        public List<CooperationRootElement> Elements { get; set; }

        public List<CooperationRelationship> References { get; set; }

        [JsonIgnore]
        public FileModel File { get; internal set; }

    }

}
