using Bb.Galileo.Files.Datas;
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

        

        public override void Evaluate()
        {
            
            base.Evaluate();

            File.Parent.Models.EvaluateRestrictions(this.Origin, this.File, this.Origin.Restrictions);
            File.Parent.Models.EvaluateRestrictions(this.Target, this.File, this.Target.Restrictions);

        }

        public EntityDefinition GetOriginDefinition()
        {
            return this.File.Parent.Models.GetEntityDefinition(this.Origin.Name);
        }

        public EntityDefinition GetTargetDefinition()
        {
            return this.File.Parent.Models.GetEntityDefinition(this.Target.Name);
        }

        public IEnumerable<ReferentialRelationship> GetRelationships()
        {
            var model = this.File.Parent.Models;
            var items = model.GetReferentials(typeof(ReferentialRelationship), this.Name);
            foreach (ReferentialRelationship item in items)
                yield return item;
        }

        public IEnumerable<ReferentialEntity> GetTargetEntities()
        {

            var model = this.File.Parent.Models;

            var items = model.GetEntityDefinition(this.Target.Name).GetEntities();
            foreach (ReferentialEntity item in items)
                yield return item;

        }

    }

}
