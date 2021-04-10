using Newtonsoft.Json;

namespace Bb.Galileo.Files.Schemas
{
    public class EntityTypeDefinition
    {

        [JsonRequired]
        public string Name { get; set; }

        public bool Enabled { get; set; }

        [JsonIgnore]
        public string Element { get; set; }

        [JsonIgnore]
        public string LayerName { get; set; }


    }

}
