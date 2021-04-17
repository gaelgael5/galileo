using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Schemas
{


    public class MetaDefinitions
    {

        public MetaDefinitions()
        {
            Restrictions = new List<RestrictionDefinition>();
        }

        [JsonRequired]
        public string Name { get; set; }

        public List<EntityDefinition> Entities { get; set; }

        public List<RelationshipDefinition> Relationships { get; set; }

        public List<RestrictionDefinition> Restrictions { get; set; }

        //public List<RuleDefinition> Rules { get; set; }

    }

}
