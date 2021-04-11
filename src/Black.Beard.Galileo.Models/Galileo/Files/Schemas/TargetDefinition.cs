using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Schemas
{
    public class TargetDefinition
    {

        [JsonRequired]
        public string Name { get; set; }

        public List<TargetDefinition> Targets { get; set; }


    }



}
