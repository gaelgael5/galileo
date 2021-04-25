using Bb.Galileo.Files.Datas;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bb.Galileo.Files.Schemas
{

    public class EntityDefinition : ModelDefinition
    {

        public EntityDefinition()
        {
            this.Properties = new List<PropertyDefinition>();

        }

        [Description("Added specific properties")]
        public List<PropertyDefinition> Properties { get; set; }

        [Description("Entity kind. the availables items are managed in the layer file")]
        [JsonRequired]
        public string Kind { get; set; }

        public IEnumerable<ReferentialEntity> GetEntities()
        {
            var model = this.File.Parent.Models;
            var items = model.GetReferentials(typeof(ReferentialEntity), this.Name);
            foreach (ReferentialEntity item in items)
                yield return item;
        }

        public ReferentialEntity GetEntityByName(string key, string target = null)
        {
            var model = this.File.Parent.Models;

            if (string.IsNullOrEmpty(target))
                target = "Current";

            if (!key.StartsWith("e:"))
                key = "e:" + key;

            return model.GetEntity(this.Name, target, key);

        }

        public IEnumerable<RelationshipDefinition> AvailablesRelationshipDefinitionTo()
        {

            var model = this.File.Parent.Models;

            var items = model.Get<RelationshipDefinition>();
            foreach (var item in items)
                if (item.Origin.Name == this.Name)
                    yield return item;
        }

        public IEnumerable<RelationshipDefinition> AvailablesRelationshipDefinitionFrom()
        {

            var model = this.File.Parent.Models;

            var items = model.Get<RelationshipDefinition>();
            foreach (var item in items)
                if (item.Target.Name == this.Name)
                    yield return item;
        }


    }



}
