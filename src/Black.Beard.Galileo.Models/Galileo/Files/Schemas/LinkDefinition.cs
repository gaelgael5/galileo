using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Schemas
{
    public class LinkDefinition
    {


        public LinkDefinition()
        {
            this.Properties = new List<PropertyDefinition>();
        }

        public List<PropertyDefinition> Properties { get; set; }

        [JsonRequired]
        public MultiplicityEnum Multiplicty { get; set; }

        //public string[] Restrictions { get; set; }



    }


}
