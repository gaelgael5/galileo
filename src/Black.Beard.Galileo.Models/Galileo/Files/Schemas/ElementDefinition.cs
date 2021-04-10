using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Schemas
{
    public class ElementDefinition
    {

        [JsonRequired]
        public string Name { get; set; }

        public List<EntityTypeDefinition> Concepts { get; set; }


    }

}
