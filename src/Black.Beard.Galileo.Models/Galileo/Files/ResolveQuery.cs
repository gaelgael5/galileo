using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bb.Galileo.Files
{


    /// <summary>
    /// Syntax : e:Application;t:Existant;i:280d589ab1014cef85af664414316b85
    ///          e -> entity
    ///          l -> Relationship
    ///          r -> Restriction
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

        public ResolveQuery(ElementEnum kind, string Entityname)
        {
            this.Kind = kind;
            this.TypeName = Entityname;
        }

        public ResolveQuery(IBase item)
        {
            SetIdentifier(item);
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
                        this.Identifier = item;
                        break;
                    case "i":
                        this.Identifier = item;
                        break;

                    case "e":
                        this.Kind = ElementEnum.Entity;
                        this.TypeName = e[1];
                        break;

                    case "r":
                        this.Kind = ElementEnum.RestrictionDefinition;
                        this.TypeName = e[1];
                        break;
                    case "l":
                        this.Kind = ElementEnum.Relationship;
                        this.TypeName = e[1];
                        break;
                    case "de":
                        this.Kind = ElementEnum.EntityDefinition;
                        this.TypeName = e[1];
                        break;
                    case "dl":
                        this.Kind = ElementEnum.RelationshipDefinition;
                        this.TypeName = e[1];
                        break;

                    default:
                        throw new InvalidDataException($"invalid kind key {e[0].ToLower()}:");
                }

            }

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

                case ElementEnum.RestrictionDefinition:
                    sb.Append("r:");
                    sb.Append(this.TypeName);
                    sb.Append(";");
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


            return sb.ToString().Trim(';');
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
            else if (item is RestrictionDefinition r2)
            {
                this.Kind = ElementEnum.RestrictionDefinition;
                this.TypeName = r2.Name;
            }
            else if (item is ReferentialRelationshipLink l)
            {
                this.Kind = ElementEnum.Entity;
                this.Identifier = "n:" + l.Name;
            }
            else
                throw new NotImplementedException(item.GetType().Name);

            return this;

        }

        public IEnumerable<IBase> GetReferentials(ModelRepository rep)
        {

            QueryFilter query = new QueryFilter();

            Type type = null;
            switch (this.Kind)
            {
                case ElementEnum.Entity:
                    type = typeof(ReferentialEntity);
                    if (!string.IsNullOrEmpty(this.Identifier))
                        query.AddName(this.Identifier);

                    if (!string.IsNullOrEmpty(this.TypeName))
                        query.AddTypeName(this.TypeName);
                    break;

                case ElementEnum.Relationship:
                    type = typeof(ReferentialRelationship);
                    if (!string.IsNullOrEmpty(this.Identifier))
                        query.AddName(this.Identifier);

                    if (!string.IsNullOrEmpty(this.TypeName))
                        query.AddTypeName(this.TypeName);
                    break;

                case ElementEnum.RelationshipDefinition:
                    type = typeof(RelationshipDefinition);
                    if (!string.IsNullOrEmpty(this.TypeName))
                        query.AddName(this.TypeName);
                    break;

                case ElementEnum.EntityDefinition:
                    type = typeof(EntityDefinition);
                    if (!string.IsNullOrEmpty(this.TypeName) && this.TypeName != "*")
                        query.AddName(this.TypeName);
                    break;

                case ElementEnum.RestrictionDefinition:
                default:
                    break;
            }

            if (type != null)
            {
                var source = rep.Get(type, query).ToList();
                return source;
            }

            return new IBase[0];

        }

        public ElementEnum Kind { get; set; }

        public string Target { get; set; }

        public string TypeName { get; set; }

        public string Identifier { get; set; }

        private void Stop()
        {
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Break();

        }

        public static implicit operator ResolveQuery(string txt)
        {
            return new ResolveQuery(txt);
        }

    }

    public static class ResolveQueryExtension
    {

        public static ResolveQuery AsQuery(this string txt)
        {
            return txt;
        }

    }

}
