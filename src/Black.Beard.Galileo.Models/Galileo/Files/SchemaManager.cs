using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Models;
using NJsonSchema;
using System;
using System.IO;

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

            var layers = (LayersDefinition)this._parent.GetLayers();

            GenerateSchema(targetDir, typeof(MetaDefinitions),
            (s) =>
            {

                var k = s.Definitions[nameof(EntityDefinition)]
                         .Properties[nameof(EntityDefinition.Kind)];

                if (layers != null)
                {
                    foreach (var item in layers.GetList())
                        if (item.Enabled)
                            k.Enumeration.Add(item.Name);

                    k = s.Definitions[nameof(PropertyDefinition)]
                        .Properties[nameof(PropertyDefinition.Enums)];

                    k.UniqueItems = true;
                }

            }
            );

            GenerateSchema(targetDir, typeof(LayersDefinition),
            (s) =>
            {

            }
            );


        }

        private void GenerateSchema(DirectoryInfo dir, Type type, Action<JsonSchema> act = null)
        {
            string id = this._parent.Config.GetUri(type.Name);
            string filename = System.IO.Path.Combine(dir.FullName, type.Name + extension);
            var schema = SchemaHelper.GenerateSchemaForClass(type, type.Name);

            schema.Id = id;

            if (act != null)
                act(schema);

            File.WriteAllText(filename, schema.ToJson());
        }

        public void GenerateDefinitions(DirectoryInfo targetDir = null)
        {

            if (targetDir == null)
                targetDir = new DirectoryInfo(System.IO.Path.Combine(this._parent.Files.Folder.FullName, "Schemas"));

            if (!targetDir.Exists)
                targetDir.Create();

            SchemaGenerator generator = new SchemaGenerator(this._parent.Config)
            {

            };

            foreach (var item in this._parent.Get<EntityDefinition>())
                generator.Add(item);

            foreach (var item in this._parent.Get<RelationshipDefinition>())
                generator.Add(item);

            generator.GenerateTo(targetDir);

        }

        public bool GenerateDefinition(IBase item, DirectoryInfo targetDir = null)
        {

            if (targetDir == null)
                targetDir = new DirectoryInfo(System.IO.Path.Combine(this._parent.Files.Folder.FullName, "Schemas"));

            if (!targetDir.Exists)
                targetDir.Create();

            SchemaGenerator generator = new SchemaGenerator(this._parent.Config)
            {

            };

            bool result = false;
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

            if (result)
                generator.GenerateTo(targetDir);

            return result;
        }

        private ModelRepository _parent;
        private string extension = ".schema.json";

    }

}