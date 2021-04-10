using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Models;
using System;
using System.IO;
using System.Text;

namespace Bb.Galileo.Files
{
    public class ResolveQuery
    {

        public ResolveQuery()
        {

        }

        public override string ToString()
        {

            StringBuilder sb = new StringBuilder(50);

            switch (this.Kind)
            {
                case ElementEnum.Entity:
                    sb.Append("e:");
                    sb.Append(this.TypeName);
                    sb.Append(";");
                    sb.Append(Identifier);
                    break;

                case ElementEnum.Relationship:
                    sb.Append("l:");
                    sb.Append(this.TypeName);
                    sb.Append(";");
                    sb.Append(Identifier);
                    break;

                case ElementEnum.EntityDefinition:
                    sb.Append("de:");
                    sb.Append(this.TypeName);
                    sb.Append(";");
                    break;

                case ElementEnum.RelationshipDefinition:
                    sb.Append("dl:");
                    sb.Append(this.TypeName);
                    sb.Append(";");
                    break;

                default:
                    break;

            }


            return sb.ToString();
        }

        public ResolveQuery(string query)
        {

            var items = query.Split(';');
            if (items.Length == 2)
            {

                //e:Application;i:280d589ab1014cef85af664414316b85

                TypeName = items[0];

                var e = TypeName.Split(':');
                switch (e[0].ToLower())
                {
                    case "e":
                        this.Kind = ElementEnum.Entity;
                        break;
                    case "l":
                        this.Kind = ElementEnum.Relationship;
                        break;
                    case "de":
                        this.Kind = ElementEnum.EntityDefinition;
                        break;
                    case "dl":
                        this.Kind = ElementEnum.RelationshipDefinition;
                        break;

                    default:
                        throw new InvalidDataException($"invalid kind key {e[0].ToLower()}:");
                }

                this.TypeName = e[1];

                if (items.Length == 2)
                    Identifier = items[1];

                return;
            }

            throw new InvalidDataException($"invalid key {query}");

        }

        public void SetIdentifier(IBase item)
        {

            if (item is ReferentialEntity e)
            {
                this.Kind = ElementEnum.Entity;
                this.TypeName = e.TypeEntity;
                this.Identifier = "i:" + e.Id;
            }
            else if (item is ReferentialRelationship r)
            {
                this.Kind = ElementEnum.Relationship;
                this.TypeName = r.TypeEntity;
                this.Identifier = "i:" + r.Id;
            }
            else if (item is EntityDefinition e1)
            {
                this.Kind = ElementEnum.EntityDefinition;
                this.TypeName = e1.Name;
            }
            else if (item is RelationshipDefinition r1)
            {
                this.Kind = ElementEnum.RelationshipDefinition;
                this.TypeName = r1.Name;
            }
            else
                throw new NotImplementedException(item.GetType().Name);

        }

        public IBase Get(ModelRepository rep)
        {
            switch (this.Kind)
            {
                case ElementEnum.Entity:
                    return rep.GetEntity(this.TypeName, this.Identifier);

                case ElementEnum.Relationship:
                    return rep.GetRelationship(this.TypeName, this.Identifier);

                case ElementEnum.EntityDefinition:
                    return rep.GetEntityDefinition(this.TypeName);

                case ElementEnum.RelationshipDefinition:
                    return rep.GetRelationshipDefinition(this.TypeName);

                default:
                    break;
            }

            return null;

        }

        public ElementEnum Kind { get; private set; }

        public string TypeName { get; private set; }

        public string Identifier { get; private set; }

    }


}
