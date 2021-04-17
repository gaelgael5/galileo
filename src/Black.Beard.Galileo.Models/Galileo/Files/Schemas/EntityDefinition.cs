using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bb.Galileo.Files.Schemas
{

    public class EntityDefinition : ModelDefinition
    {

        public EntityDefinition()
        {
            this.Properties = new List<PropertyDefinition>();

        }

        [Description("Added specific properties")]
        public List<PropertyDefinition> Properties { get; set; }

        [Description("Entity kind. the availables items are managed in the layer file")]
        [JsonRequired]
        public string Kind { get; set; }

    }



}
