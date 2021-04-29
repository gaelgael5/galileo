using Bb.Galileo.Files;
using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Schemas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bb.Galileo.Models
{

    internal class ConvertEntities<T> : Newtonsoft.Json.JsonConverter
    where T : ReferentialBase
    {

        public ConvertEntities(ModelRepository parent, IDiagnostic diagnostic, FileModel fullPath)
        {
            this._referenceSchema = fullPath.Schema;
            this._parent = parent;
            this._file = fullPath;
            this._diagnostic = diagnostic;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Entities<T>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            Entities<T> target = new Entities<T>();

            try
            {

                // Load JObject from stream
                JObject jObject = JObject.Load(reader);

                bool hasChanged = false;

                List<T> _items = new List<T>();
                foreach (var property in jObject.Properties())
                {

                    if (property.Name.ToLower() == "$schema")
                    {

                    }
                    else if (property.Name.ToLower() == "target")
                        target.Target = (string)property.Value;

                    else if (property.Name.ToLower() == "referentials")
                    {
                        var j = (JArray)property.Value;
                        IEnumerable<ReferentialBase> targetItem = this._referenceSchema.Kind == KindSchemaEnum.Relationship
                            ? (IEnumerable<ReferentialBase>)PopulateLinks(j)
                            : (IEnumerable<ReferentialBase>)PopulateEntities(j)
                            ;

                        foreach (var item in targetItem)
                        {

                            item.TargetName = target.Target;
                            item.ResetChanges();
                            _items.Add((T)item);

                            if (string.IsNullOrEmpty(item.Id))
                            {
                                item.Id = Guid.NewGuid().ToString("N");
                                hasChanged = true;
                            }

                        }

                    }
                    else
                    {

                    }

                }

                var p1 = _items.OrderBy(c1 => c1.Name).ToList();
                int c = 0;
                foreach (var item in p1)
                {
                    target.Add(item);
                    if (p1[c].Id != _items[c].Id)
                        hasChanged = true;
                    c++;
                }

                target.HasChangedOnLoading = hasChanged;


            }
            catch (Exception e)
            {
                this.Exception = e;
                throw;
            }

            return target;

        }

        private IEnumerable<ReferentialBase> PopulateEntities(JArray array)
        {

            var schema = (EntityDefinition)_parent.GetEntityDefinition(this._referenceSchema.Type);

            if (schema != null)
                foreach (JObject item in array)
                    yield return PopulateEntity(item, schema);

        }

        private ReferentialEntity PopulateEntity(JObject jObject, EntityDefinition schema)
        {

            ReferentialEntity target = new ReferentialEntity(this._referenceSchema.Type, _file)
            {
                Schema = this._referenceSchema,
            };

            foreach (var item in jObject.Properties())
            {

                var p = schema.Properties.FirstOrDefault(c => c.Name.ToLower() == item.Name.ToLower());
                if (p != null)
                {
                    try
                    {
                        var value = ConvertValue(p.Type, item.Value);
                        target[p.Name] = value;
                    }
                    catch (Exception e)
                    {
                        this._diagnostic.Append(new DiagnositcMessage() { Severity = SeverityEnum.Error, Text = e.Message, Exception = e });
                    }
                }
            }

            return target;
        }

        private object ConvertValue(PropertyDefinitionEnum type, JToken value)
        {

            switch (type)
            {

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
                case PropertyDefinitionEnum.Email:
                case PropertyDefinitionEnum.Text:
                    return (string)value;

                case PropertyDefinitionEnum.Integer:
                    return int.Parse((string)value);

                case PropertyDefinitionEnum.Double:
                    return double.Parse((string)value);

                case PropertyDefinitionEnum.Bool:
                    return ((string)value).ToLower() == "true";

                case PropertyDefinitionEnum.Date:
                case PropertyDefinitionEnum.DateTime:
                    return DateTime.Parse((string)value);

                case PropertyDefinitionEnum.Time:
                    return TimeSpan.Parse((string)value);

                default:
                    break;
            }

            return null;

        }

        private IEnumerable<ReferentialBase> PopulateLinks(JArray array)
        {

            var schema = (RelationshipDefinition)_parent.GetRelationshipDefinition(this._referenceSchema.Type);

            if (schema != null)
                foreach (JObject item in array)
                    yield return PopulateLink(item, schema);

        }

        private ReferentialRelationship PopulateLink(JObject jObject, RelationshipDefinition schema)
        {
            var target = new ReferentialRelationship(this._referenceSchema.Type, _file)
            {
                Schema = this._referenceSchema,
            };

            foreach (var item in jObject.Properties())
            {
                if (item.Name == "OriginLink")
                {
                    var i = (JObject)item.Value;
                    foreach (var item2 in i.Properties())
                    {

                        var p = schema.Origin.Properties.FirstOrDefault(c => c.Name.ToLower() == item2.Name.ToLower());
                        if (p != null)
                        {
                            try
                            {
                                var value = ConvertValue(p.Type, item2.Value);
                                target.Origin[p.Name] = value;
                            }
                            catch (Exception e1)
                            {
                                this._diagnostic.Append(new DiagnositcMessage() { Severity = SeverityEnum.Error, Text = e1.Message, Exception = e1 });
                            }
                        }
                        else
                        {

                        }
                    }
                }
                else if (item.Name == "TargetLink")
                {
                    var i = (JObject)item.Value;
                    foreach (var item2 in i.Properties())
                    {
                        var p = schema.Target.Properties.FirstOrDefault(c => c.Name == item2.Name);
                        if (p != null)
                        {
                            try
                            {
                                var value = ConvertValue(p.Type, item2.Value);
                                target.Target[p.Name] = value;
                            }
                            catch (Exception e2)
                            {
                                this._diagnostic.Append(new DiagnositcMessage() { Severity = SeverityEnum.Error, Text = e2.Message, Exception = e2 });
                            }
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    var p = schema.Properties.FirstOrDefault(c => c.Name == item.Name);
                    if (p != null)
                        try
                        {
                            var value = ConvertValue(p.Type, item.Value);
                            target[item.Name] = value;
                        }
                        catch (Exception e3)
                        {
                            this._diagnostic.Append(new DiagnositcMessage() { Severity = SeverityEnum.Error, Text = e3.Message, Exception = e3 });
                        }
                }
            }

            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            var source = (Entities<T>)value;

            writer.WriteStartObject();

            writer.WritePropertyName("Target");
            writer.WriteValue(source.Target);

            writer.WritePropertyName("Referentials");
            writer.WriteStartArray();


            if (this._referenceSchema.Kind == KindSchemaEnum.Relationship)
            {

                foreach (object item in source)
                {

                    writer.WriteStartObject();

                    ReferentialRelationship e = (ReferentialRelationship)item;
                    foreach (var propertyName in e.PropertyKeys())
                    {
                        writer.WritePropertyName(propertyName);
                        writer.WriteValue(e[propertyName]);
                    }

                    writer.WritePropertyName("OriginLink");
                    writer.WriteStartObject();
                    foreach (var propertyName in e.Origin.PropertyKeys())
                    {
                        writer.WritePropertyName(propertyName);
                        writer.WriteValue(e[propertyName]);
                    }
                    writer.WriteEndObject();


                    writer.WritePropertyName("TargetLink");
                    writer.WriteStartObject();
                    foreach (var propertyName in e.Target.PropertyKeys())
                    {
                        writer.WritePropertyName(propertyName);
                        writer.WriteValue(e[propertyName]);
                    }
                    writer.WriteEndObject();

                    writer.WriteEndObject();

                }
            }
            else
            {

                foreach (object item in source)
                {
                    writer.WriteStartObject();
                    ReferentialEntity e = (ReferentialEntity)item;
                    foreach (var propertyName in e.PropertyKeys())
                    {
                        writer.WritePropertyName(propertyName);
                        writer.WriteValue(e[propertyName]);
                    }
                    writer.WriteEndObject();

                }

            }

            writer.WriteEndArray();

            writer.WriteEndObject();

        }

        private SchemaReference _referenceSchema;
        private ModelRepository _parent;
        private readonly FileModel _file;
        private readonly IDiagnostic _diagnostic;

        public Exception Exception { get; private set; }

    }


}
