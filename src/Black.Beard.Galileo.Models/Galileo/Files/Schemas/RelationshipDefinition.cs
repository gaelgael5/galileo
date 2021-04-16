using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Schemas
{

    public class RelationshipDefinition : ModelDefinition
    {

        public RelationshipDefinition()
        {
            this.Properties = new List<PropertyDefinition>();
        }

        public List<PropertyDefinition> Properties { get; set; }

        [JsonRequired]
        public RelationshipKindEnum Kind { get; set; }

        [JsonRequired]
        public LinkDefinition Origin { get; set; }

        [JsonRequired]
        public LinkDefinition Target { get; set; }

        public EntityDefinition GetOriginDefinition()
        {
            return this.File.Parent.Models.GetEntityDefinition(this.Origin.Name);
        }

        public EntityDefinition GetTargetDefinition()
        {
            return this.File.Parent.Models.GetEntityDefinition(this.Target.Name);
        }


    }

}
