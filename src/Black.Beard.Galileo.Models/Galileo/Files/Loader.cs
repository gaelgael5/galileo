using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Bb.Galileo.Files
{
    internal class Loader
    {

        public Loader(ModelRepository parent)
        {
            this._parent = parent;
        }

        public void Deserialize(JObject payload, FileModel file)
        {

            var schema = payload.GetSchemaReference();

            switch (schema.Kind)
            {

                case KindSchemaEnum.SchemaDefinitions:
                    LoadMetaDefinitions(payload, file, schema);
                    break;

                case KindSchemaEnum.SchemaLayerDefinitions:
                    LoadLayerDefinitions(payload, file);
                    break;

                case KindSchemaEnum.Entity:
                    LoadEntity(payload, file, schema);
                    break;
                case KindSchemaEnum.Relationship:
                    LoadRelationship(payload, file, schema);
                    break;

                case KindSchemaEnum.Undefined:
                case KindSchemaEnum.Definition:
                case KindSchemaEnum.Schema:
                default:
                    this._parent.Diagnostic.Append(new DiagnositcMessage() { Severity = SeverityEnum.Warning, File = file.FullPath, Text = "Failed to resolve the schema" });
                    break;
            }

        }

        private void LoadRelationship(JObject payload, FileModel file, SchemaReference schema)
        {

            JsonSerializer serializer = new JsonSerializer();
            var reader = new JTokenReader(payload);
            var converter = new ConvertEntities<ReferentialRelationship>(schema, this._parent, this._parent.Diagnostic, file);
            serializer.Converters.Add(converter);
            var model = (Entities<ReferentialRelationship>)serializer.Deserialize(reader, typeof(Entities<ReferentialRelationship>));

            if (model.HasChangedOnLoading)
            {
                model.Save(file.FullPath, Formatting.Indented, converter);
            }

            foreach (var item in model)
            {
                if (!string.IsNullOrEmpty(item.Name))
                    this._parent.Add(item);
            }

        }

        private void LoadEntity(JObject payload, FileModel file, SchemaReference schema)
        {

            JsonSerializer serializer = new JsonSerializer();
            var reader = new JTokenReader(payload);
            var converter = new ConvertEntities<ReferentialEntity>(schema, this._parent, this._parent.Diagnostic, file);
            serializer.Converters.Add(converter);
            var model = (Entities<ReferentialEntity>)serializer.Deserialize(reader, typeof(Entities<ReferentialEntity>));

            if (model.HasChangedOnLoading)
            {
                model.Save(file.FullPath, Formatting.Indented, converter);
            }

            foreach (var item in model)
            {
                if (!string.IsNullOrEmpty(item.Name))
                    this._parent.Add(item);
            }

        }

        private void LoadMetaDefinitions(JObject payload, FileModel file, SchemaReference schema)
        {

            JsonSerializer serializer = new JsonSerializer();

            var model = (MetaDefinitions)serializer.Deserialize(new JTokenReader(payload), typeof(MetaDefinitions));

            foreach (EntityDefinition entity in model.Entities)
            {
                entity.File = file;
                entity.Schema = schema;
                entity.Properties.Insert(0, Property(nameof(ReferentialEntity.Description), "entity's Description", "Specify the description for this entity", string.Empty));
                entity.Properties.Insert(0, Property(nameof(ReferentialEntity.Label), "Label of the entity", "Specify the label for this entity", string.Empty, true));
                entity.Properties.Insert(0, Property(nameof(ReferentialBase.Name), "entity'name", "Specify the functional name of the entity", this._parent.Config.RestrictionNamePattern, true));
                entity.Properties.Insert(0, Property(nameof(ReferentialBase.Id), "entity'key", "Specify the key of the entity", this._parent.Config.RestrictionNamePattern, true));

                this._parent.Add(entity);

            }

            foreach (RelationshipDefinition relationship in model.Relationships)
            {
                relationship.File = file;
                relationship.Schema = schema;

                relationship.Properties.Insert(0, Property(nameof(ReferentialRelationship.Description), "relationship's Description", "Specify the description for this relationship", string.Empty));
                relationship.Properties.Insert(0, Property(nameof(ReferentialRelationship.Label), "Label of the relationship", "Specify the label for this relationship", string.Empty, true));
                relationship.Properties.Insert(0, Property(nameof(ReferentialBase.Name), "entity'key", "Specify the functional name of the entity", this._parent.Config.RestrictionNamePattern, true));
                relationship.Properties.Insert(0, Property(nameof(ReferentialBase.Id), "entity'key", "Specify the key of the entity", this._parent.Config.RestrictionNamePattern, true));

                relationship.Origin.Properties.Insert(0, Property(nameof(ModelDefinition.Name), "reference of the entity", "Specify the origin reference.", this._parent.Config.RestrictionNamePattern, true));
                relationship.Target.Properties.Insert(0, Property(nameof(ModelDefinition.Name), "reference of the entity", "Specify the target reference", this._parent.Config.RestrictionNamePattern, true));
                this._parent.Add(relationship);
            };


        }

        private static PropertyDefinition Property(string name, string label, string description, string pattern, bool required = false)
        {
            var prop = new PropertyDefinition()
            {
                Name = name,
                Label = label,
                Description = description,
                Required = required,
                Type = PropertyDefinitionEnum.Text,
            };

            prop.TextConstraints.Pattern = pattern;

            return prop;

        }

        private void LoadLayerDefinitions(JObject payload, FileModel file)
        {

            JsonSerializer serializer = new JsonSerializer()
            {
                //Converters
            };

            var model = (LayersDefinition)serializer.Deserialize(new JTokenReader(payload), typeof(LayersDefinition));
            model.File = file;
            model.Name = nameof(LayersDefinition);

            foreach (var layer in model.Layers)
                foreach (var element in layer.Elements)
                    foreach (var concept in element.Concepts)
                    {
                        concept.LayerName = layer.Name;
                        concept.Element = element.Name;
                    }

            _parent.Add(model);

        }

        public void LoadConfig(DirectoryInfo folder)
        {
            var Filename = new FileInfo(System.IO.Path.Combine(folder.FullName, "config.json"));
            if (Filename.Exists)
                this._parent.Config = ContentHelper.LoadContentFromFile(Filename.FullName)
                    .Deserialize<ConfigModel>();

            else
            {
                this._parent.Config = new ConfigModel()
                {
                    Si = "MyCompany"
                };
                this._parent.Config.Save(Filename.FullName);
            }
        }

        private readonly ModelRepository _parent;

    }

}
