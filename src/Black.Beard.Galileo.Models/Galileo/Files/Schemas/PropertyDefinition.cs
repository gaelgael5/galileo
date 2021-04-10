using Newtonsoft.Json;

namespace Bb.Galileo.Files.Schemas
{
    public class PropertyDefinition
    {

        public PropertyDefinition()
        {
            TextConstraints = new TextConstraint();
            NumberConstraints = new NumberConstraints();
        }

        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public PropertyDefinitionEnum Type { get; set; }

        public string Label { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }

        //public string[] Restrictions { get; set; } = new string[0];

        //public string[] Rules { get; set; } = new string[0];


        public string[] Enums { get; set; } = new string[0];


        public TextConstraint TextConstraints { get; set; }

        public NumberConstraints NumberConstraints { get; set; }

    }


}
