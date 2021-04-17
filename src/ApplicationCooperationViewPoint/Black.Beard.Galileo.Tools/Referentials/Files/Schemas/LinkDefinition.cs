using Bb.Galileo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bb.Galileo.Files.Schemas
{
    public class LinkDefinition : IBase
    {

        public LinkDefinition()
        {
            this.Properties = new List<PropertyDefinition>();
            this.Restrictions = new List<string>();
        }

        [Description("relationship's reference")]
        [JsonRequired]
        public string Name { get; set; }


        public List<PropertyDefinition> Properties { get; set; }

        [JsonRequired]
        public MultiplicityEnum Multiplicty { get; set; }

        public List<string> Restrictions { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }

    }


}
