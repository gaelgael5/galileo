using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Models;
using System;
using System.IO;
using System.Text;

namespace Bb.Galileo.Files
{


    /// <summary>
    /// Syntax : e:Application;t:Existant;i:280d589ab1014cef85af664414316b85
    ///          e -> entity
    ///          l -> Relationship
    ///          de -> Entity definition
    ///          dl -> Relationship definition
    ///          
    ///          t -> Target. if item is not found in current specified, it is search in the parent layer
    ///          
    ///          i -> the identifier is an id
    ///          n -> the identifier is the functional name
    ///          
    /// </summary>
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

            foreach (var item in items)
            {
                
                var e = item.Split(':');

                switch (e[0].ToLower())
                {
                    case "t":
                        this.Target = e[1];
                        break;

                    case "n":
                    case "i":
                        this.Identifier = item;
                        break;

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

            }

        }

        public ResolveQuery SetIdentifier(IBase item)
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

            return this;

        }

        public IBase Get(ModelRepository rep)
        {
            switch (this.Kind)
            {
                case ElementEnum.Entity:
                    return rep.GetEntity(this.TypeName, this.Target, this.Identifier);

                case ElementEnum.Relationship:
                    return rep.GetRelationship(this.TypeName, this.Target, this.Identifier);

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

        public string Target { get; private set; }

        public string TypeName { get; private set; }

        public string Identifier { get; private set; }

    }


}
