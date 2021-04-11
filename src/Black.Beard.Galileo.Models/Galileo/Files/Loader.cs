using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Files.Viewpoints;
using Bb.Galileo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bb.Galileo.Files
{
    internal class Loader
    {

        public Loader(ModelRepository parent)
        {
            this._parent = parent;
        }

        public Transactionfile Remove(JObject payload, FileModel file)
        {

            var result = new Transactionfile()
            {
                File = file,
                Trace = FileTracingEnum.Deleted,
            };

            var item1 =  this._parent.CollectFile<ReferentialBase>(file).ToList();
            if (item1.Count > 0)
            {
                foreach (var item in item1)
                {
                    _parent.RemoveReferential(item);
                    result.Deleted.Add(item);
                }
            }

            var item2 = this._parent.CollectFile<ModelDefinition>(file).ToList();
            if (item2.Count > 0)
            {
                foreach (var item in item2)
                {
                    _parent.RemoveDefinition(item);
                    result.Deleted.Add(item);
                }
            }

            var item3 = this._parent.CollectFile<CooperationViewpoint>(file).ToList();
            if (item3.Count > 0)
            {
                foreach (var item in item3)
                {
                    _parent.RemoveCooperationViewpoint(item);
                    result.Deleted.Add(item);
                }
            }

            var item4 = this._parent.CollectFile<LayersDefinition>(file).ToList();
            if (item4.Count > 0)
            {
                foreach (var item in item4)
                {
                    _parent.RemoveLayers(item);
                    result.Deleted.Add(item);
                }
            }

            return result;

        }

        public Transactionfile Add(JObject payload, FileModel file)
        {

            Transactionfile result = null;

            file.Schema = payload.GetSchemaReference();

            switch (file.Schema.Kind)
            {

                case KindSchemaEnum.SchemaDefinitions:
                    result = LoadMetaDefinitions(payload, file);
                    break;

                case KindSchemaEnum.SchemaLayerDefinitions:
                    result = LoadLayerDefinitions(payload, file);
                    break;

                case KindSchemaEnum.Entity:
                    result = LoadEntity(payload, file);
                    break;
                case KindSchemaEnum.Relationship:
                    result = LoadRelationship(payload, file);
                    break;

                case KindSchemaEnum.CooperationViewpoint:
                    result = LoadCooperationViewpoint(payload, file);
                    break;

                case KindSchemaEnum.Undefined:
                case KindSchemaEnum.Definition:
                case KindSchemaEnum.Schema:
                default:
                    this._parent.Diagnostic.Append(new DiagnositcMessage() { Severity = SeverityEnum.Warning, File = file.FullPath, Text = "Failed to resolve the schema" });
                    break;
            }

            result.File = file;

            return result;

        }


        private Transactionfile LoadRelationship(JObject payload, FileModel file)
        {

            Transactionfile result = new Transactionfile();

            JsonSerializer serializer = new JsonSerializer();
            var reader = new JTokenReader(payload);
            var converter = new ConvertEntities<ReferentialRelationship>(this._parent, this._parent.Diagnostic, file);
            serializer.Converters.Add(converter);
            var model = (Entities<ReferentialRelationship>)serializer.Deserialize(reader, typeof(Entities<ReferentialRelationship>));

            if (model.HasChangedOnLoading)
            {
                model.Save(file.FullPath, Formatting.Indented, converter);
            }

            List<ReferentialRelationship> _removed = new List<ReferentialRelationship>();

            var items = this._parent.CollectFile<ReferentialRelationship>(file).ToList();

            foreach (var item in model)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.Name))
                    {
                        this._parent.Add(item);
                        var  p = items.RemoveWhere(c => c.Name == item.Name);
                        if (p.Count == 0)
                            result.Added.Add(item);
                        else
                            result.Updated.Add(item);
                    }
                }
                catch (System.Exception e)
                {
                    _parent.Diagnostic.Append(
                        new DiagnositcMessage()
                        {
                            Severity = SeverityEnum.Error,
                            File = file.FullPath,
                            Exception = e,
                            Text = $"Failed to adding relationship {item.Name}"
                        }
                    );

                }
            }

            foreach (var item in items)
            {
                this._parent.RemoveReferential(item);
                result.Deleted.Add(item);
            }

            return result;

        }

        private Transactionfile LoadEntity(JObject payload, FileModel file)
        {

            Transactionfile result = new Transactionfile();

            JsonSerializer serializer = new JsonSerializer();
            var reader = new JTokenReader(payload);
            var converter = new ConvertEntities<ReferentialEntity>(this._parent, this._parent.Diagnostic, file);
            serializer.Converters.Add(converter);
            var model = (Entities<ReferentialEntity>)serializer.Deserialize(reader, typeof(Entities<ReferentialEntity>));

            if (model.HasChangedOnLoading)
            {
                model.Save(file.FullPath, Formatting.Indented, converter);
            }

            var items = this._parent.CollectFile<ReferentialEntity>(file).ToList();

            foreach (var item in model)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.Name))
                    {
                        this._parent.Add(item);
                        var p = items.RemoveWhere(c => c.Name == item.Name);
                        if (p.Count == 0)
                            result.Added.Add(item);
                        else
                            result.Updated.Add(item);
                    }

                }
                catch (System.Exception e)
                {

                    _parent.Diagnostic.Append(
                        new DiagnositcMessage()
                        {
                            Severity = SeverityEnum.Error,
                            File = file.FullPath,
                            Exception = e,
                            Text = $"Failed to adding entity {item.Name}"
                        }
                    );

                }
            }

            foreach (var item in items)
            {
                this._parent.RemoveReferential(item);
                result.Deleted.Add(item);
            }

            return result;

        }

        private Transactionfile LoadCooperationViewpoint(JObject payload, FileModel file)
        {

            Transactionfile result = new Transactionfile();

            JsonSerializer serializer = new JsonSerializer();
            var reader = new JTokenReader(payload);

            var model = (CooperationViewpoint)serializer.Deserialize(reader, typeof(CooperationViewpoint));
            model.File = file;

            var items = this._parent.CollectFile<CooperationViewpoint>(file).ToList();

            try
            {
                if (!string.IsNullOrEmpty(model.Name))
                {
                    this._parent.Add(model);
                    var p = items.RemoveWhere(c => c.Name == model.Name);
                    if (p.Count == 0)
                        result.Added.Add(model);
                    else
                        result.Updated.Add(model);
                }

            }
            catch (System.Exception e)
            {
                _parent.Diagnostic.Append(
                        new DiagnositcMessage()
                        {
                            Severity = SeverityEnum.Error,
                            File = file.FullPath,
                            Exception = e,
                            Text = $"Failed to adding cooperation viewpoint {model.Name}"
                        }
                    );

            }

            foreach (var item in items)
            {
                this._parent.RemoveCooperationViewpoint(item);
                result.Deleted.Add(item);
            }

            return result;

        }

        private Transactionfile LoadMetaDefinitions(JObject payload, FileModel file)
        {

            Transactionfile result = new Transactionfile();

            JsonSerializer serializer = new JsonSerializer();

            var model = (MetaDefinitions)serializer.Deserialize(new JTokenReader(payload), typeof(MetaDefinitions));

            var item1 = this._parent.CollectFile<EntityDefinition>(file).ToList();

            foreach (EntityDefinition entity in model.Entities)
            {
                entity.File = file;
                entity.Properties.Insert(0, Property(nameof(ReferentialEntity.Description), "entity's Description", "Specify the description for this entity", string.Empty));
                entity.Properties.Insert(0, Property(nameof(ReferentialEntity.Label), "Label of the entity", "Specify the label for this entity", string.Empty, true));
                entity.Properties.Insert(0, Property(nameof(ReferentialBase.Name), "entity'name", "Specify the functional name of the entity", this._parent.Config.RestrictionNamePattern, true));
                entity.Properties.Insert(0, Property(nameof(ReferentialBase.Id), "entity'key", "Specify the key of the entity", string.Empty, true));

                try
                {
                    this._parent.Add(entity);
                    var p = item1.RemoveWhere(c => c.Name == entity.Name);
                    if (p.Count == 0)
                        result.Added.Add(entity);
                    else
                        result.Updated.Add(entity);
                }
                catch (System.Exception e1)
                {

                    _parent.Diagnostic.Append(
                        new DiagnositcMessage()
                        {
                            Severity = SeverityEnum.Error,
                            File = file.FullPath,
                            Exception = e1,
                            Text = $"Failed to adding entity definition {model.Name}"
                        }
                    );

                }

            }

            foreach (var item in item1)
            {
                this._parent.RemoveDefinition(item);
                result.Deleted.Add(item);
            }

            var item2 = this._parent.CollectFile<RelationshipDefinition>(file).ToList();

            foreach (RelationshipDefinition relationship in model.Relationships)
            {
                relationship.File = file;
                relationship.Properties.Insert(0, Property(nameof(ReferentialRelationship.Description), "relationship's Description", "Specify the description for this relationship", string.Empty));
                relationship.Properties.Insert(0, Property(nameof(ReferentialRelationship.Label), "Label of the relationship", "Specify the label for this relationship", string.Empty, true));
                relationship.Properties.Insert(0, Property(nameof(ReferentialBase.Name), "entity'key", "Specify the functional name of the entity", this._parent.Config.RestrictionNamePattern, true));
                relationship.Properties.Insert(0, Property(nameof(ReferentialBase.Id), "entity'key", "Specify the key of the entity", string.Empty, true));

                relationship.Origin.Properties.Insert(0, Property(nameof(ModelDefinition.Name), "reference of the entity", "Specify the origin reference.", this._parent.Config.RestrictionNamePattern, true));
                relationship.Target.Properties.Insert(0, Property(nameof(ModelDefinition.Name), "reference of the entity", "Specify the target reference", this._parent.Config.RestrictionNamePattern, true));

                try
                {
                    this._parent.Add(relationship);
                    var p = item2.RemoveWhere(c => c.Name == relationship.Name);
                    if (p.Count == 0)
                        result.Added.Add(relationship);
                    else
                        result.Updated.Add(relationship);

                }
                catch (System.Exception e)
                {
                    _parent.Diagnostic.Append(
                            new DiagnositcMessage()
                            {
                                Severity = SeverityEnum.Error,
                                File = file.FullPath,
                                Exception = e,
                                Text = $"Failed to adding relationship definition {relationship.Name}"
                            }
                        );

                }

            }

            foreach (var item in item2)
            {
                this._parent.RemoveDefinition(item);
                result.Deleted.Add(item);
            }

            return result;

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

        private Transactionfile LoadLayerDefinitions(JObject payload, FileModel file)
        {

            Transactionfile result = new Transactionfile();

            JsonSerializer serializer = new JsonSerializer()
            {
            };

            var model = (LayersDefinition)serializer.Deserialize(new JTokenReader(payload), typeof(LayersDefinition));
            model.File = file;
            model.Name = nameof(LayersDefinition);

            var items = this._parent.CollectFile<LayersDefinition>(file).ToList();

            foreach (var layer in model.Layers)
                foreach (var element in layer.Elements)
                    foreach (var concept in element.Concepts)
                    {
                        concept.LayerName = layer.Name;
                        concept.Element = element.Name;
                    }

            try
            {
                _parent.Add(model);
                var p = items.RemoveWhere(c => c.Name == model.Name);
                if (p.Count == 0)
                    result.Added.Add(model);
                else
                    result.Updated.Add(model);
            }
            catch (System.Exception e)
            {
                _parent.Diagnostic.Append(
                        new DiagnositcMessage()
                        {
                            Severity = SeverityEnum.Error,
                            File = file.FullPath,
                            Exception = e,
                            Text = $"Failed to adding layers definition {model.Name}"
                        }
                    );
            }

            foreach (var item in items)
            {
                this._parent.RemoveLayers(item);
                result.Deleted.Add(item);
            }

            return result;

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



    public class Transactionfile
    {

        public Transactionfile()
        {
            Added = new List<IBase>();
            Updated = new List<IBase>();
            Deleted = new List<IBase>();
        }

        public FileModel File { get; set; }

        public List<IBase> Added { get; set; }

        public List<IBase> Updated { get; set; }

        public List<IBase> Deleted { get; set; }
        public FileTracingEnum Trace { get; internal set; }
    }

}
