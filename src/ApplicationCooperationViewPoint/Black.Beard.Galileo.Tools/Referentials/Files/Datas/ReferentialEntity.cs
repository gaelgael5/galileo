using Bb.Galileo.Files.Schemas;
using System.Collections.Generic;
using System.Linq;

namespace Bb.Galileo.Files.Datas
{
    public class ReferentialEntity : ReferentialBase
    {

        public ReferentialEntity(string type, FileModel fullPath)
            : base(type, fullPath)
        {

        }


        public string Label
        {
            get => (string)this[nameof(Label)];
            set => this[nameof(Label)] = value;
        }

        public string Description
        {
            get => (string)this[nameof(Description)];
            set => this[nameof(Description)] = value;
        }

        public EntityDefinition GetDefinition()
        {
            var model = this.File.Parent.Models;
            return model.GetEntityDefinition(this.TypeEntity);
        }

        public IEnumerable<ReferentialEntity> GetTargetEntities(RelationshipDefinition relationshipDefinition)
        {

            var t = relationshipDefinition.GetRelationships().Where(c => c.Origin.Name == this.Name)
                .Select(c => c.Name)
                .ToList();

            var datas = relationshipDefinition.GetTargetEntities().ToList();

            var model = this.File.Parent.Models;
            foreach (var item in datas)
                if (t.Any(c => c == item.Name))
                    yield return item;
        }



    }


}
