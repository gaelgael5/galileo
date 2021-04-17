using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bb.Galileo.Files.Schemas
{

    public class RelationshipDefinition : ModelDefinition
    {

        public RelationshipDefinition()
        {
            this.Properties = new List<PropertyDefinition>();
        }

        [Description("Added specific properties")]
        public List<PropertyDefinition> Properties { get; set; }

        [Description("Kind relationship")]
        [JsonRequired]
        public RelationshipKindEnum Kind { get; set; }

        [Description("Origin specification on the Type of entities expected")]
        [JsonRequired]
        public LinkDefinition Origin { get; set; }

        [Description("Target specification on the Type of entities expected")]
        [JsonRequired]
        public LinkDefinition Target { get; set; }

        public EntityDefinition GetOriginDefinition()
        {
            return this.File.Parent.Models.GetEntityDefinition(this.Origin.Name);
        }

        public override void Evaluate()
        {
            
            base.Evaluate();

            File.Parent.Models.EvaluateRestrictions(this.Origin, this.File, this.Origin.Restrictions);
            File.Parent.Models.EvaluateRestrictions(this.Target, this.File, this.Target.Restrictions);

        }


        public EntityDefinition GetTargetDefinition()
        {
            return this.File.Parent.Models.GetEntityDefinition(this.Target.Name);
        }


    }

}
