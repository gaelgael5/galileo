using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Files.Viewpoints;
using Bb.Galileo.Models;
using NJsonSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bb.Galileo.Files
{


    public class SchemaManager
    {

        public SchemaManager(ModelRepository modelRepository)
        {
            this._parent = modelRepository;
        }

        public void GenerateSchemas(DirectoryInfo targetDir = null)
        {

            if (targetDir == null)
                targetDir = new DirectoryInfo(System.IO.Path.Combine(this._parent.Files.Folder.FullName, "Schemas"));

            if (!targetDir.Exists)
                targetDir.Create();

            var layers = this._parent.GetLayers();

            if (layers != null)
            {
                GenerateSchema(targetDir, typeof(MetaDefinitions),
                (s) =>
                {

                    var n = s.Definitions[nameof(EntityDefinition)]
                             .Properties[nameof(ModelDefinition.Name)];
                    n.Pattern = _parent.Config.RestrictionNamePattern;

                    n = s.Definitions[nameof(RelationshipDefinition)]
                        .Properties[nameof(ModelDefinition.Name)];
                    n.Pattern = _parent.Config.RestrictionNamePattern;

                    var k = s.Definitions[nameof(EntityDefinition)]
                             .Properties[nameof(EntityDefinition.Kind)];

                    foreach (var item in layers.GetList())
                        if (item.Enabled)
                            k.Enumeration.Add(item.Name);

                    k = s.Definitions[nameof(PropertyDefinition)]
                        .Properties[nameof(PropertyDefinition.Enums)];

                    k.UniqueItems = true;

                }
                );
            }
            else
            {

            }

            GenerateSchema(targetDir, typeof(ConfigModel),
            (s) =>
            {
                var n = s.Definitions[nameof(TargetDefinition)]
                         .Properties[nameof(TargetDefinition.Name)];
                n.Pattern = _parent.Config.RestrictionNamePattern;
            }
            );

            GenerateSchema(targetDir, typeof(LayersDefinition),
            (s) =>
            {

            }
            );

            var entityDefinitions = this._parent.GetEntityDefinitions().Select(c => c.Name).ToList();
            var relationshipDefinitions = this._parent.GetRelationshipDefinitions().Select(c => c.Name).ToList();

            GenerateSchema(targetDir, typeof(CooperationViewpoint),
            (s) =>
            {

                var concept = s.Definitions[nameof(CooperationConcept)]
                       .Properties[nameof(CooperationBase.Name)];

                var rootElement = s.Definitions[nameof(CooperationRootElement)]
                       .Properties[nameof(CooperationBase.Name)];

                foreach (var item in entityDefinitions)
                {
                    concept.Enumeration.Add(item);
                    rootElement.Enumeration.Add(item);
                }

                var element = s.Definitions[nameof(CooperationElement)]
                      .Properties[nameof(CooperationBase.Name)];

                var relationship = s.Definitions[nameof(CooperationRelationship)]
                  .Properties[nameof(CooperationBase.Name)];

                foreach (var item in relationshipDefinitions)
                {
                    element.Enumeration.Add(item);
                    relationship.Enumeration.Add(item);
                }

            }
            );



        }

        private void GenerateSchema(DirectoryInfo dir, Type type, Action<JsonSchema> act = null)
        {

            string id = this._parent.Config.GetUri(type.Name);
            string filename = System.IO.Path.Combine(dir.FullName, type.Name + extension);
            var schema = SchemaHelper.GenerateSchemaForClass(type, type.Name);
            schema.AllowAdditionalProperties = true;
            schema.Id = id;

            if (act != null)
                act(schema);

            var payload = schema.ToJson();

            string old = null;

            if (File.Exists(filename))
                old = filename.LoadContentFromFile();

            if (old == null || string.Compare(old, payload) != 0)
            {

                if (File.Exists(filename))
                    File.Delete(filename);

                filename.Save(payload);

                _parent.Diagnostic.Append(new DiagnositcMessage()
                {
                    Severity = SeverityEnum.Verbose,
                    File = filename,
                    Text = $"File {filename} is refreshed",
                });
            }

        }

        public void GenerateDefinitions(DirectoryInfo targetDir = null)
        {

            if (targetDir == null)
                targetDir = new DirectoryInfo(System.IO.Path.Combine(this._parent.Files.Folder.FullName, "Schemas"));

            if (!targetDir.Exists)
                targetDir.Create();

            SchemaGenerator generator = new SchemaGenerator(this._parent.Config, this._parent)
            {

            };

            foreach (var item in this._parent.Get<EntityDefinition>())
                generator.Add(item);

            foreach (var item in this._parent.Get<RelationshipDefinition>())
                generator.Add(item);

            generator.GenerateTo(targetDir);

        }

        public bool GenerateDefinition(IEnumerable<ModelDefinition> items, DirectoryInfo targetDir = null)
        {

            if (targetDir == null)
                targetDir = new DirectoryInfo(System.IO.Path.Combine(this._parent.Files.Folder.FullName, "Schemas"));

            if (!targetDir.Exists)
                targetDir.Create();

            SchemaGenerator generator = new SchemaGenerator(this._parent.Config, this._parent)
            {

            };

            bool result = false;
            foreach (var item in items)
            {

                if (item is EntityDefinition e)
                {
                    generator.Add(e);
                    result = true;
                }

                else if (item is RelationshipDefinition r)
                {
                    generator.Add(r);
                    result = true;
                }

            }

            if (result)
                result = generator.GenerateTo(targetDir);

            return result;
        }

        private ModelRepository _parent;
        private string extension = ".schema.json";

    }

}