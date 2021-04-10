using Bb.Galileo.Models;
using Newtonsoft.Json;

namespace Bb.Galileo.Files.Schemas

{
    public class ModelDefinition : IBase
    {


        [JsonRequired]
        public string Name { get; set; }

        public string Label { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public FileModel File { get; internal set; }

        [JsonIgnore]
        public SchemaReference Schema { get; internal set; }

    }

}
