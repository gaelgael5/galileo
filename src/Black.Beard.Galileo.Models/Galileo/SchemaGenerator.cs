using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Models;
using NJsonSchema;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bb.Galileo
{
    public class SchemaGenerator
    {

        public SchemaGenerator(ConfigModel config)
        {
            _schemas = new Dictionary<string, JsonSchema>();
            this._config = config;
        }

        public void Add(EntityDefinition item)
        {

            JsonSchema _schemaRoot = new JsonSchema()
            {
                Id = this._config.GetUri("entities", item.Name),
                Description = item.Description,
                Type = JsonObjectType.Object,
            };

            var def1 = new JsonSchema()
            {
                Type = JsonObjectType.Object,
            };

            _schemaRoot.Definitions.Add(item.Name, def1);
            _schemas.Add(item.Name + ".entities", _schemaRoot);
            _schemaRoot.Properties["Target"] = new JsonSchemaProperty() { Type = JsonObjectType.String, IsRequired = true };
            _schemaRoot.Properties["InheritFromTarget"] = new JsonSchemaProperty() { Type = JsonObjectType.String };
            _schemaRoot.Properties["Referentials"] = new JsonSchemaProperty() { Item = new JsonSchema() { Reference = def1 } };

            var dic = new Dictionary<string, JsonSchemaProperty>();
            foreach (var property in item.Properties)
                dic.Add(property.Name, GetProperty(property));

            def1.Properties[nameof(ReferentialBase.Name)] = dic[nameof(ReferentialBase.Name)];
            def1.Properties[nameof(ReferentialEntity.Label)] = dic[nameof(ReferentialEntity.Label)];
            def1.Properties[nameof(ReferentialEntity.Description)] = dic[nameof(ReferentialEntity.Description)];

            foreach (var item2 in dic.Keys.OrderBy(c => c))
                if (!def1.Properties.ContainsKey(item2))
                    def1.Properties[item2] = dic[item2];


        }

        internal void Add(RelationshipDefinition item)
        {

            JsonSchema _schemaRoot = new JsonSchema()
            {
                Id = this._config.GetUri("entities", item.Name),
                Description = item.Description,
                Type = JsonObjectType.Object,
            };

            var def1 = new JsonSchema()
            {
                Type = JsonObjectType.Object,
            };
            _schemaRoot.Definitions.Add(item.Name, def1);
            _schemas.Add(item.Name + ".entities", _schemaRoot);

            _schemaRoot.Properties["Target"] = new JsonSchemaProperty() { Type = JsonObjectType.String, IsRequired = true };
            _schemaRoot.Properties["InheritFromTarget"] = new JsonSchemaProperty() { Type = JsonObjectType.String };
            _schemaRoot.Properties["Referentials"] = new JsonSchemaProperty() { Item = new JsonSchema() { Reference = def1 } };

            var dic = new Dictionary<string, JsonSchemaProperty>();

            SetLinkProperties(item, dic);

            foreach (var property in item.Properties)
                dic.Add(property.Name, GetProperty(property));



            def1.Properties[nameof(ReferentialBase.Name)] = dic[nameof(ReferentialBase.Name)];
            def1.Properties[nameof(ReferentialEntity.Label)] = dic[nameof(ReferentialEntity.Label)];
            def1.Properties[nameof(ReferentialEntity.Description)] = dic[nameof(ReferentialEntity.Description)];

            foreach (var item2 in dic.Keys.OrderBy(c => c))
                if (!def1.Properties.ContainsKey(item2))
                    def1.Properties[item2] = dic[item2];

        }


        private void SetLinkProperties(RelationshipDefinition item, Dictionary<string, JsonSchemaProperty> dic)
        {

            var p = new JsonSchemaProperty()
            {
                Type = JsonObjectType.Object,
                IsRequired = true,
            };
            dic.Add("OriginLink", p);

            Dictionary<string, JsonSchemaProperty> dic2 = new Dictionary<string, JsonSchemaProperty>();
            foreach (var property in item.Origin.Properties)
                dic2.Add(property.Name, GetProperty(property));

            p.Properties[nameof(ReferentialBase.Name)] = dic2[nameof(ReferentialBase.Name)];
            foreach (var item2 in dic2.Keys.OrderBy(c => c))
                if (!p.Properties.ContainsKey(item2))
                    p.Properties[item2] = dic2[item2];

            p = new JsonSchemaProperty()
            {
                Type = JsonObjectType.Object,
                IsRequired = true,
            };
            dic.Add("TargetLink", p);

            dic2 = new Dictionary<string, JsonSchemaProperty>();
            foreach (var property in item.Target.Properties)
                dic2.Add(property.Name, GetProperty(property));

            p.Properties[nameof(ReferentialBase.Name)] = dic2[nameof(ReferentialBase.Name)];
            foreach (var item2 in dic2.Keys.OrderBy(c => c))
                if (!p.Properties.ContainsKey(item2))
                    p.Properties[item2] = dic2[item2];

        }

        public void GenerateTo(DirectoryInfo targetFolder)
        {
            foreach (var item in this._schemas)
            {

                var file = new FileInfo(System.IO.Path.Combine(targetFolder.FullName, item.Key + ".schema.json"));
                if (file.Exists)
                    file.Delete();

                file.FullName.Save(item.Value.ToJson());

            }
        }


        private JsonSchemaProperty GetProperty(PropertyDefinition prop)
        {

            var value = new JsonSchemaProperty()
            {
                IsRequired = prop.Required
            };

            if (!string.IsNullOrEmpty(prop.Description))
                value.Description = prop.Description;

            switch (prop.Type)
            {

                case PropertyDefinitionEnum.Integer:
                case PropertyDefinitionEnum.Double:
                    GetTypeNumeric(prop, value);
                    break;

                case PropertyDefinitionEnum.DateTime:
                case PropertyDefinitionEnum.Time:
                case PropertyDefinitionEnum.Date:
                case PropertyDefinitionEnum.Email:
                case PropertyDefinitionEnum.IdnEmail:
                case PropertyDefinitionEnum.Hostname:
                case PropertyDefinitionEnum.IdnHostname:
                case PropertyDefinitionEnum.Ipv4:
                case PropertyDefinitionEnum.Ipv6:
                case PropertyDefinitionEnum.Uri:
                case PropertyDefinitionEnum.UriReference:
                case PropertyDefinitionEnum.Iri:
                case PropertyDefinitionEnum.IriReference:
                case PropertyDefinitionEnum.UriTemplate:
                case PropertyDefinitionEnum.JsonPointer:
                case PropertyDefinitionEnum.Regex:
                case PropertyDefinitionEnum.Text:
                    GetTypeString(prop, value);
                    break;

                case PropertyDefinitionEnum.Bool:
                    value.Type = JsonObjectType.Boolean;
                    break;

                default:
                    break;
            }

            return value;

        }

        /// <summary>
        /// https://json-schema.org/understanding-json-schema/reference/string.html
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        private void GetTypeString(PropertyDefinition prop, JsonSchemaProperty value)
        {


            if (prop.TextConstraints != null)
            {
                if (prop.TextConstraints.MinLength.HasValue)
                    value.MinLength = prop.TextConstraints.MinLength.Value;

                if (prop.TextConstraints.MaxLength.HasValue)
                    value.MaxLength = prop.TextConstraints.MaxLength.Value;

            }

            switch (prop.Type)
            {
                case PropertyDefinitionEnum.DateTime:
                    value.Pattern = "date-time";

                    break;
                case PropertyDefinitionEnum.Time:
                    value.Pattern = "time";
                    break;
                case PropertyDefinitionEnum.Date:
                    value.Pattern = "date";
                    break;
                case PropertyDefinitionEnum.Email:
                    value.Pattern = "email";
                    break;
                case PropertyDefinitionEnum.IdnEmail:
                    value.Pattern = "idn-email";
                    break;
                case PropertyDefinitionEnum.Hostname:
                    value.Pattern = "hostname";
                    break;
                case PropertyDefinitionEnum.IdnHostname:
                    value.Pattern = "idn-hostname";
                    break;
                case PropertyDefinitionEnum.Ipv4:
                    value.Pattern = "ipv4";
                    break;
                case PropertyDefinitionEnum.Ipv6:
                    value.Pattern = "ipv6";
                    break;
                case PropertyDefinitionEnum.Uri:
                    value.Pattern = "uri";
                    break;
                case PropertyDefinitionEnum.UriReference:
                    value.Pattern = "uri-reference";
                    break;
                case PropertyDefinitionEnum.Iri:
                    value.Pattern = "iri";
                    break;
                case PropertyDefinitionEnum.IriReference:
                    value.Pattern = "iri-reference";
                    break;
                case PropertyDefinitionEnum.UriTemplate:
                    value.Pattern = "uri-template";
                    break;
                case PropertyDefinitionEnum.JsonPointer:
                    value.Pattern = "json-pointer";
                    break;
                case PropertyDefinitionEnum.Regex:
                    value.Pattern = prop.TextConstraints.Pattern;
                    break;
                default:
                    break;
            }

            if (prop.Enums != null && prop.Enums.Length > 0)
                foreach (var item in prop.Enums)
                    value.Enumeration.Add(item);

            //if (prop.Restrictions.Count > 0)
            //{
            //    foreach (var restriction in prop.Restrictions)
            //    {
            //        var model = (ModelWorkRestrictionDefinition)restriction.GetModel();
            //        if (model != null)
            //        {
            //            var array = new JArray();
            //            var items = model.Item.GetItems().ToList();
            //            foreach (var item in items)
            //                array.Add(item.Value);
            //            if (array.Count > 0)
            //                value.Add(new JProperty("enum", array));
            //        }
            //        else
            //        {
            //        }
            //    }
            //}

        }

        private void GetTypeNumeric(PropertyDefinition prop, JsonSchemaProperty item)
        {

            if (prop.Type == PropertyDefinitionEnum.Integer)
                item.Type = JsonObjectType.Integer;

            else if (prop.Type == PropertyDefinitionEnum.Double)
                item.Type = JsonObjectType.Number;

            if (prop.NumberConstraints != null)
            {

                if (prop.NumberConstraints.MultipleOf > 1)
                    item.MultipleOf = prop.NumberConstraints.MultipleOf;


                if (prop.NumberConstraints.Minimum.HasValue)
                {
                    item.Minimum = prop.NumberConstraints.Minimum;
                    if (prop.NumberConstraints.ExclusiveMinimum)
                        item.IsExclusiveMinimum = true;
                }

                if (prop.NumberConstraints.Maximum.HasValue)
                {
                    item.Maximum = prop.NumberConstraints.Maximum;
                    if (prop.NumberConstraints.ExclusiveMaximum)
                        item.IsExclusiveMaximum = true;
                }

            }

        }


        private readonly Dictionary<string, JsonSchema> _schemas;
        private readonly ConfigModel _config;
    }



}
