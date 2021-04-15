using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bb.Galileo.Files.Schemas
{
    public class LinkDefinition
    {


        public LinkDefinition()
        {
            this.Properties = new List<PropertyDefinition>();
        }

        [Description("relationship's reference")]
        [JsonRequired]
        public string Name { get; set; }


        public List<PropertyDefinition> Properties { get; set; }

        [JsonRequired]
        public MultiplicityEnum Multiplicty { get; set; }

        //public string[] Restrictions { get; set; }



    }


}
