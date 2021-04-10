using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Schemas
{

    public class EntityDefinition : ModelDefinition
    {

        public EntityDefinition()
        {
            this.Properties = new List<PropertyDefinition>();

        }

        public List<PropertyDefinition> Properties { get; set; }

        [JsonRequired]
        public string Kind { get; set; }
    
    }



}
