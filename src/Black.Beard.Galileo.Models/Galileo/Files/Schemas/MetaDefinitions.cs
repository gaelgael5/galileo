using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Schemas
{


    public class MetaDefinitions
    {


        [JsonRequired]
        public string Name { get; set; }

        public List<EntityDefinition> Entities { get; set; }

        public List<RelationshipDefinition> Relationships { get; set; }

        //public List<RestrictionDefinition> FilterRestrictions { get; set; }

        //public List<RuleDefinition> Rules { get; set; }


    }

}
