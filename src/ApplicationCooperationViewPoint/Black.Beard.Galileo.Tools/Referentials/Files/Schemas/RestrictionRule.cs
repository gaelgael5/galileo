using Bb.Galileo.Files.Datas;
using Bb.Galileo.Models;
using System.Linq;

namespace Bb.Galileo.Files.Schemas
{

    public abstract class RestrictionRule
    {

        public RestrictionRule(RestrictionDefinition restriction)
        {
            this._restriction = restriction;
        }

        public abstract void Evaluate(IBase item, FileModel file);

        protected readonly RestrictionDefinition _restriction;

        protected static ElementTypeEnum ResolveKind(IBase item)
        {
            ElementTypeEnum kind = ElementTypeEnum.Undefined;

            if (item is ReferentialEntity)
                kind = ElementTypeEnum.Entity;

            else if (item is ReferentialRelationship)
                kind = ElementTypeEnum.Entity;

            else if (item is ReferentialRelationshipLink)
                kind = ElementTypeEnum.Link;

            else if (item is EntityDefinition)
                kind = ElementTypeEnum.EntityDefinition;

            else if (item is RelationshipDefinition)
                kind = ElementTypeEnum.RelationshipDefinition;

            else if (item is LinkDefinition)
                kind = ElementTypeEnum.RelationshipLinkDefinition;
            return kind;
        }

    }

    public class TypeRestrictionRule : RestrictionRule
    {


        public TypeRestrictionRule(RestrictionDefinition restriction)
            : base(restriction)
        {
            this._rule = new ResolveQuery(restriction.Value.ToString());
        }


        public override void Evaluate(IBase item, FileModel file)
        {

            var models = file.Parent.Models;
            var result = this._rule.GetReferentials(models)
                .Any(c => c.Name == item.Name)
                ;

            if (!result)
            {

                string msg = this._restriction.ErrorMessage
                    .Replace("!name", this._restriction.Name)
                    .Replace("!ibase.name", item.Name)
                    .Replace("!rule", this._rule.ToString())
                    ;

                models.Diagnostic.Append
                (
                    new DiagnositcMessage()
                    {
                        File = file.FullPath,
                        Text = msg,

                        Severity = SeverityEnum.Error,
                    }
                );

            }

        }

        private ResolveQuery _rule;

    }

}
