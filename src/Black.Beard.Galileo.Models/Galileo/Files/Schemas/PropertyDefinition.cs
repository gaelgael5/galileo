using Newtonsoft.Json;
using System.ComponentModel;

namespace Bb.Galileo.Files.Schemas
{
    public class PropertyDefinition
    {

        public PropertyDefinition()
        {
            TextConstraints = new TextConstraint();
            NumberConstraints = new NumberConstraints();
        }

        [Description("Property's name")]
        [JsonRequired]
        public string Name { get; set; }


        [Description("Property's type")]
        [JsonRequired]
        public PropertyDefinitionEnum Type { get; set; }


        [Description("Property's label for display in the propertygrid")]
        public string Label { get; set; }


        [Description("Description of the property")]
        public string Description { get; set; }


        [Description("Specify where the propertyy is required")]
        public bool Required { get; set; }

        //public string[] Restrictions { get; set; } = new string[0];

        //public string[] Rules { get; set; } = new string[0];


        [Description("restrict the values")]
        public string[] Enums { get; set; } = new string[0];


        public TextConstraint TextConstraints { get; set; }

        public NumberConstraints NumberConstraints { get; set; }

    }


}
