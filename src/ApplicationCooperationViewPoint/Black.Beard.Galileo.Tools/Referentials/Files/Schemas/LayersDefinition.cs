using Bb.Galileo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Bb.Galileo.Files.Schemas
{
    public class LayersDefinition : IBase
    {

        [JsonIgnore]
        public string Name { get; set; }

        public List<LayerDefinition> Layers { get; set; }

        public IEnumerable<EntityTypeDefinition> GetList()
        {
            foreach (var layer in this.Layers)
                foreach (var element in layer.Elements)
                    foreach (var item in element.Concepts)
                        yield return item;
        }

        [JsonIgnore]
        public FileModel File { get; internal set; }

    }



}
