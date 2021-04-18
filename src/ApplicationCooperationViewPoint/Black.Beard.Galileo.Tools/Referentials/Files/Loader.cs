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

            if (file.Schema != null)
                switch (file.Schema.Kind)
                {
                    case KindSchemaEnum.Undefined:
                        break;

                    case KindSchemaEnum.Entity:
                    case KindSchemaEnum.Relationship:
                        var item1 = this._parent.CollectContentOfFile<ReferentialBase>(file).ToList();
                        if (item1.Count > 0)
                            foreach (var item in item1)
                            {
                                _parent.RemoveReferential(item);
                                result.Deleted.Add(item);
                            }
                        break;

                    case KindSchemaEnum.Definition:
                        var item2 = this._parent.CollectContentOfFile<ModelDefinition>(file).ToList();
                        if (item2.Count > 0)
                            foreach (var item in item2)
                            {
                                _parent.RemoveDefinition(item);
                                result.Deleted.Add(item);
                            }
                        break;

                    case KindSchemaEnum.Schema:
                        break;

                    case KindSchemaEnum.SchemaDefinitions:
                        break;

                    case KindSchemaEnum.CooperationViewpoint:
                        var item3 = this._parent.CollectContentOfFile<CooperationViewpoint>(file).ToList();
                        if (item3.Count > 0)
                            foreach (var item in item3)
                            {
                                _parent.RemoveCooperationViewpoint(item);
                                result.Deleted.Add(item);
                            }
                        break;

                    case KindSchemaEnum.SchemaLayerDefinitions:
                        var item4 = this._parent.CollectContentOfFile<LayersDefinition>(file).ToList();
                        if (item4.Count > 0)
                            foreach (var item in item4)
                            {
                                _parent.RemoveLayers(item);
                                result.Deleted.Add(item);
                            }
                        break;

                    default:
                        break;

                }


            return result;

        }

        public Transactionfile Add(JObject payload, FileModel file)
        {

            Transactionfile result = null;

            file.Schema = payload.GetSchemaReference(file, this._parent.Config.GetRoot());

            if (file.Schema == null)
            {

                return null;

            }

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

                case KindSchemaEnum.Schema:
                    result = LoadSchema(payload, file);
                    break;

                case KindSchemaEnum.SchemaEntity:
                case KindSchemaEnum.SchemaLink:
                    result = LoadObjectSchema(payload, file);
                    break;

                case KindSchemaEnum.Undefined:
                case KindSchemaEnum.Definition:
                default:
                    this._parent.Diagnostic.Append(new DiagnositcMessage() { Severity = SeverityEnum.Warning, File = file.FullPath, Text = "Failed to resolve the schema" });
                    break;
            }

            if (result != null)
            {

                result.File = file;

                switch (file.Schema.Kind)
                {

                    case KindSchemaEnum.CooperationViewpoint:
                    case KindSchemaEnum.Entity:
                    case KindSchemaEnum.Relationship:
                    case KindSchemaEnum.Definition:
                    case KindSchemaEnum.SchemaLayerDefinitions:
                    case KindSchemaEnum.SchemaDefinitions:
                        _parent.SchemaValidator.Evaluate(result.File, payload);
                        break;

                    case KindSchemaEnum.Schema:
                        break;

                    case KindSchemaEnum.SchemaEntity:
                    case KindSchemaEnum.SchemaLink:
                        _parent.Files.EvaluateSchema(result);
                        break;

                    case KindSchemaEnum.Undefined:
                    default:
                        break;
                }

                foreach (var item in result.Added)
                    if (item is IEvaluate e)
                        e.Evaluate();

                foreach (var item in result.Updated)
                    if (item is IEvaluate e)
                        e.Evaluate();

            }

            return result;

        }

        private Transactionfile LoadSchema(JObject payload, FileModel file)
        {
            Transactionfile result = new Transactionfile();
            return result;
        }

        private Transactionfile LoadObjectSchema(JObject payload, FileModel file)
        {

            Transactionfile result = new Transactionfile();

            ObjectBaseSchema item = null;
            if (file.Schema.Kind == KindSchemaEnum.SchemaEntity)
            {
                item = new EntitySchema()
                {
                    Name = file.Schema.Type,
                    File = file,
                };
            }
            else if (file.Schema.Kind == KindSchemaEnum.SchemaLink)
            {
                item = new LinkSchema()
                {
                    Name = file.Schema.Type,
                    File = file,
                };
            }

            if (item != null)
            {
                if (!string.IsNullOrEmpty(item.Name))
                {

                    if (this._parent.AddSchema(item))
                        result.Added.Add(item);
                    else
                        result.Updated.Add(item);
                }

            }
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

            if (converter.Exception != null)
                throw converter.Exception;

            if (model.HasChangedOnLoading)
                model.Save(file.FullPath, Formatting.Indented, converter);

            List<ReferentialRelationship> _removed = new List<ReferentialRelationship>();

            var items = this._parent.CollectContentOfFile<ReferentialRelationship>(file).ToList();

            foreach (var relationship in model)
            {

                // Add restriction on the name of origin et target
                relationship.Origin.Restrictions.Add($"{relationship.Origin.Name}TypeReferentialRestriction");
                relationship.Target.Restrictions.Add($"{relationship.Target.Name}TypeReferentialRestriction");

                try
                {
                    if (!string.IsNullOrEmpty(relationship.Name))
                    {
                        this._parent.Add(relationship);
                        var p = items.RemoveWhere(c => c.Name == relationship.Name);
                        if (p.Count == 0)
                            result.Added.Add(relationship);
                        else
                            result.Updated.Add(relationship);
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
                            Text = $"Failed to adding relationship {relationship.Name}"
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

            JsonSerializer serializer = new JsonSerializer()
            {

            };
            var reader = new JTokenReader(payload);
            var converter = new ConvertEntities<ReferentialEntity>(this._parent, this._parent.Diagnostic, file);
            serializer.Converters.Add(converter);
            var model = (Entities<ReferentialEntity>)serializer.Deserialize(reader, typeof(Entities<ReferentialEntity>));

            if (converter.Exception != null)
                throw converter.Exception;

            if (model.HasChangedOnLoading)
                model.Save(file.FullPath, Formatting.Indented, converter);

            var items = this._parent.CollectContentOfFile<ReferentialEntity>(file).ToList();

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

            var items = this._parent.CollectContentOfFile<CooperationViewpoint>(file).ToList();

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

            var itemRestrictions = this._parent.CollectContentOfFile<RestrictionDefinition>(file).Where(c => !c.AutoGenerated).ToList();
            foreach (var restriction in model.Restrictions)
            {

                restriction.File = file;

                try
                {
                    this._parent.Add(restriction);
                    var p = itemRestrictions.RemoveWhere(c => c.Name == restriction.Name);
                    if (p.Count == 0)
                        result.Added.Add(restriction);
                    else
                        result.Updated.Add(restriction);
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
            foreach (var item in itemRestrictions)
            {
                if (!item.AutoGenerated)
                {
                    this._parent.RemoveDefinition(item);
                    result.Deleted.Add(item);
                }
            }

            var itemEntities = this._parent.CollectContentOfFile<EntityDefinition>(file).ToList();
            foreach (EntityDefinition entity in model.Entities)
            {

                entity.File = file;
                entity.Properties.Insert(0, Property(nameof(ReferentialEntity.Description), "entity's Description", "Specify the description for this entity", string.Empty));
                entity.Properties.Insert(0, Property(nameof(ReferentialEntity.Label), "Label of the entity", "Specify the label for this entity", string.Empty, true));
                entity.Properties.Insert(0, Property(nameof(ReferentialBase.Name), "entity'name", "Specify the functional name of the entity", this._parent.Config.RestrictionNamePattern, true));
                entity.Properties.Insert(0, Property(nameof(ReferentialBase.Id), "entity'key", "Specify the key of the entity", string.Empty, true));

                EnsureRestrictionExists(file, entity, "e");     // create a restriction on the type for use in relation referential

                try
                {
                    this._parent.Add(entity);
                    var p = itemEntities.RemoveWhere(c => c.Name == entity.Name);
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

            foreach (var item in itemEntities)
            {
                this._parent.RemoveDefinition(item);
                result.Deleted.Add(item);
            }

            var itemLinks = this._parent.CollectContentOfFile<RelationshipDefinition>(file).ToList();

            foreach (RelationshipDefinition relationship in model.Relationships)
            {

                relationship.File = file;
                relationship.Properties.Insert(0, Property(nameof(ReferentialRelationship.Description), "relationship's Description", "Specify the description for this relationship", string.Empty));
                relationship.Properties.Insert(0, Property(nameof(ReferentialRelationship.Label), "Label of the relationship", "Specify the label for this relationship", string.Empty, true));
                relationship.Properties.Insert(0, Property(nameof(ReferentialBase.Name), "entity'key", "Specify the functional name of the entity", this._parent.Config.RestrictionNamePattern, true));
                relationship.Properties.Insert(0, Property(nameof(ReferentialBase.Id), "entity'key", "Specify the key of the entity", string.Empty, true));

                relationship.Origin.Properties.Insert(0, Property(nameof(ModelDefinition.Name), "reference of the entity", "Specify the origin reference.", this._parent.Config.RestrictionNamePattern, true));
                relationship.Target.Properties.Insert(0, Property(nameof(ModelDefinition.Name), "reference of the entity", "Specify the target reference", this._parent.Config.RestrictionNamePattern, true));

                // Add restriction on the name of origin et target
                RestrictionDefinition r = EnsureRestrictionExists(file);
                relationship.Origin.Restrictions.Add(r.Name);
                relationship.Target.Restrictions.Add(r.Name);

                try
                {
                    this._parent.Add(relationship);
                    var p = itemLinks.RemoveWhere(c => c.Name == relationship.Name);
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

            foreach (var item in itemLinks)
            {
                this._parent.RemoveDefinition(item);
                result.Deleted.Add(item);
            }

            return result;

        }

        private RestrictionDefinition EnsureRestrictionExists(FileModel file, IBase entity, string prefix)
        {

            string _prefix = string.Empty;
            if (prefix == "de")
                _prefix = "Definition";

            else if (prefix == "de")
                _prefix = "Referential";

            var name = $"{entity.Name}Type{_prefix}Restriction";

            var r = file.Parent.Models.GetRestrictionDefinition(name);

            if (r == null)
            {

                string error = "Failed to evaluate '!ibase.name' with the restriction !name -> '!rule'";

                var r2 = new RestrictionDefinition()
                {
                    File = file,
                    Description = $"Autogenerated restriction on the {entity.Name} entities",
                    Name = name,
                    ErrorMessage = error,
                    AutoGenerated = true,
                    Kind = RestrictionKindEnum.TypeRestriction,
                    Value = $"{prefix}:{entity.Name}",
                }.SetRule();

                this._parent.Add(r2);

                return r2;

            }

            return r;

        }

        private RestrictionDefinition EnsureRestrictionExists(FileModel file)
        {

            var name = $"EntityTypeRestriction";

            var r = file.Parent.Models.GetRestrictionDefinition(name);

            if (r == null)
            {

                string error = "Failed to evaluate '!ibase.name' with the restriction !name -> '!rule'";

                var r2 = new RestrictionDefinition()
                {
                    File = file,
                    Description = $"Autogenerated restriction validate the specified name is an Entity name",
                    Name = name,
                    ErrorMessage = error,
                    AutoGenerated = true,
                    Kind = RestrictionKindEnum.TypeRestriction,
                    Value = $"de:*",
                }.SetRule();

                this._parent.Add(r2);

                return r2;

            }

            return r;

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

            var items = this._parent.CollectContentOfFile<LayersDefinition>(file).ToList();

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
            var Filename = new FileInfo(System.IO.Path.Combine(folder.FullName, "galileo.config.json"));
            if (Filename.Exists)
                this._parent.Config = ContentHelper.LoadContentFromFile(Filename.FullName)
                    .Deserialize<ConfigModel>();

            else
            {
                this._parent.Config = new ConfigModel()
                {
                    Si = "MyCompany",
                    Targets = new TargetDefinition()
                    {
                        Name = "Current",
                    }
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
        public bool FailedToLoad { get; internal set; }
    }

}
