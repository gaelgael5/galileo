using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bb.Galileo.Files.Schemas
{
    public class ElementDefinition
    {

        
        [Description("element's name")]
        [JsonRequired]
        public string Name { get; set; }

        public List<EntityTypeDefinition> Concepts { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }


    }

}
