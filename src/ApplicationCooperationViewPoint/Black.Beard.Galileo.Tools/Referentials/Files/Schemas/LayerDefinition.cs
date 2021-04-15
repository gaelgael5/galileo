using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Schemas
{


    public class LayerDefinition
    {

        [JsonRequired]
        public string Name { get; set; }

        public List<ElementDefinition> Elements { get; set; }


    }



}
