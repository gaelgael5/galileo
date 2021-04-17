using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Files.Viewpoints;
using Bb.Galileo.Models;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Bb.Galileo.Files
{

    public class ModelRepository : IDisposable
    {

        public ModelRepository(string folder, IDiagnostic diagnostic)
        {
            this._loader = new Loader(this);
            this.SchemaManager = new SchemaManager(this);
            this.SchemaValidator = new SchemaValidator(this);
            this._loader.LoadConfig(new DirectoryInfo(folder));
            this.Files = new FileRepository(folder, diagnostic, this);
            this.Diagnostic = diagnostic;
            this._definitions = new Dictionary<Type, Dictionary<string, IBase>>();
            this._entities = new Dictionary<Type, TypesIndex>();
        }

        public void Initialize()
        {

            this.Files.StartListener();

            //foreach (var item in this.GetCooperationViewpoints())
            //{

            //}

        }

        public SchemaManager SchemaManager { get; }

        public SchemaValidator SchemaValidator { get; }

        public FileRepository Files { get; }

        public IDiagnostic Diagnostic { get; }

        public ConfigModel Config { get; internal set; }

        public Action<ModelRepository, Transactionfile> ItemFileHasChanged { get; set; }

        #region Schemas

        internal bool AddSchema(ObjectBaseSchema item)
        {

            var type = item.GetType();
            if (!_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
            {
                dic = new Dictionary<string, IBase>();
                _definitions.Add(type, dic);
            }

            if (dic.ContainsKey(item.Name))
            {
                var oldDefinition = dic[item.Name];
                dic[item.Name] = item;
                //PropagateFileChanged(item);
                return false;
            }

            dic.Add(item.Name, item);
            //PropagateFileChanged(item);

            return true;

        }

        internal void RefreshSchema(FileInfo file)
        {

        }


        #endregion

        #region Layers

        internal void Add(LayersDefinition item)
        {

            var type = item.GetType();
            if (!_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
            {
                dic = new Dictionary<string, IBase>();
                _definitions.Add(type, dic);
            }

            if (dic.ContainsKey(item.Name))
            {
                var oldDefinition = dic[item.Name];
                dic[item.Name] = item;
                //PropagateFileChanged(item);
                return;
            }

            dic.Add(item.Name, item);
            //PropagateFileChanged(item);

        }

        internal Transactionfile RemoveFile(FileModel item)
        {

            JObject payload = null;
            Transactionfile transaction = null;
            try
            {
                payload = item.Load();
                transaction = _loader.Remove(payload, item);
            }
            catch (System.IO.IOException e1)
            {
                Diagnostic.Append(new DiagnositcMessage() { Severity = SeverityEnum.Error, File = item.FullPath, Text = e1.Message, Exception = e1 });
            }
            catch (Exception e2)
            {
                Diagnostic.Append(new DiagnositcMessage() { Severity = SeverityEnum.Error, File = item.FullPath, Text = e2.Message, Exception = e2 });
            }

            if (transaction != null)
                if (ItemFileHasChanged != null)
                    lock (_lockFile)
                        ItemFileHasChanged(this, transaction);

            return transaction;

        }

        internal void EvaluateRestrictions(IBase item, FileModel file, IEnumerable<string> restrictions)
        {

            foreach (var restrictionName in restrictions)
            {
                var restriction = this.GetRestrictionDefinition(restrictionName);
                if (restriction == null)
                {

                }
                else
                    restriction.Rule.Evaluate((IBase)item, file);
            }

        }

        internal void RemoveLayers(LayersDefinition item)
        {

            var type = item.GetType();
            if (!_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
                _definitions.Add(type, (dic = new Dictionary<string, IBase>()));

            if (dic.ContainsKey(item.Name))
                dic.Remove(item.Name);

        }

        public LayersDefinition GetLayers()
        {
            if (_definitions.TryGetValue(typeof(LayersDefinition), out Dictionary<string, IBase> dic))
                if (dic.TryGetValue(nameof(LayersDefinition), out IBase item))
                    return (LayersDefinition)item;
            return null;
        }

        #endregion Layers

        #region CooperationVeiwpoint

        internal void Add(CooperationViewpoint item)
        {
            var type = typeof(CooperationViewpoint);
            if (!_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
            {
                dic = new Dictionary<string, IBase>();
                _definitions.Add(type, dic);
            }

            if (dic.ContainsKey(item.Name))
            {
                var oldDefinition = dic[item.Name];
                dic[item.Name] = item;
                //PropagateFileChanged(item);
                return;
            }

            dic.Add(item.Name, item);
            //PropagateFileChanged(item);

        }

        internal JsonSchema GetSchema(FileModel file)
        {
            if (File.Exists(file.Schema.FilePath))
            {
                JsonSchema schema = JsonSchema.FromJsonAsync(file.Schema.FilePath.LoadContentFromFile()).Result;
                return schema;
            }

            return null;
        }

        internal void RemoveCooperationViewpoint(CooperationViewpoint item)
        {
            var type = typeof(CooperationViewpoint);
            if (!_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
            {
                dic = new Dictionary<string, IBase>();
                _definitions.Add(type, dic);
            }

            if (dic.ContainsKey(item.Name))
                dic.Remove(item.Name);

        }

        public CooperationViewpoint GetCooperationViewpoint(string entityName)
        {
            if (_definitions.TryGetValue(typeof(CooperationViewpoint), out Dictionary<string, IBase> dic))
                if (dic.TryGetValue(entityName, out IBase item))
                    return (CooperationViewpoint)item;
            return null;
        }

        public IEnumerable<CooperationViewpoint> GetCooperationViewpoints()
        {
            if (_definitions.TryGetValue(typeof(CooperationViewpoint), out Dictionary<string, IBase> dic))
                foreach (var item in dic.Values)
                    yield return (CooperationViewpoint)item;
        }

        #endregion

        #region Definitions

        internal void Add(ModelDefinition item)
        {

            var type = item.GetType();
            if (!_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
            {
                dic = new Dictionary<string, IBase>();
                _definitions.Add(type, dic);
            }

            if (dic.ContainsKey(item.Name))
            {
                var oldDefinition = dic[item.Name];
                dic[item.Name] = item;
                //PropagateFileChanged(item);
                return;
            }

            dic.Add(item.Name, item);
            //PropagateFileChanged(item);

        }

        internal void RemoveDefinition(ModelDefinition item)
        {
            var type = item.GetType();
            if (!_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
                _definitions.Add(type, (dic = new Dictionary<string, IBase>()));
            if (dic.ContainsKey(item.Name))
                dic.Remove(item.Name);
        }

        public EntityDefinition GetEntityDefinition(string entityName)
        {
            if (_definitions.TryGetValue(typeof(EntityDefinition), out Dictionary<string, IBase> dic))
                if (dic.TryGetValue(entityName, out IBase item))
                    return (EntityDefinition)item;
            return null;
        }

        public IEnumerable<EntityDefinition> GetEntityDefinitions()
        {
            if (_definitions.TryGetValue(typeof(EntityDefinition), out Dictionary<string, IBase> dic))
                foreach (var item in dic.Values)
                    yield return (EntityDefinition)item;
        }

        public RelationshipDefinition GetRelationshipDefinition(string relationshipName)
        {
            if (_definitions.TryGetValue(typeof(RelationshipDefinition), out Dictionary<string, IBase> dic))
                if (dic.TryGetValue(relationshipName, out IBase item))
                    return (RelationshipDefinition)item;
            return null;
        }

        public IEnumerable<RelationshipDefinition> GetRelationshipDefinitions()
        {
            if (_definitions.TryGetValue(typeof(RelationshipDefinition), out Dictionary<string, IBase> dic))
                foreach (var item in dic.Values)
                    yield return (RelationshipDefinition)item;
        }

        #endregion Definitions

        #region Restrictions

        internal void Add(RestrictionDefinition item)
        {

            var type = item.GetType();
            if (!_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
            {
                dic = new Dictionary<string, IBase>();
                _definitions.Add(type, dic);
            }

            if (dic.ContainsKey(item.Name))
            {
                var oldDefinition = dic[item.Name];
                dic[item.Name] = item;
                //PropagateFileChanged(item);
                return;
            }

            dic.Add(item.Name, item);
            //PropagateFileChanged(item);

        }

        internal void RemoveDefinition(RestrictionDefinition item)
        {
            var type = item.GetType();
            if (!_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
                _definitions.Add(type, (dic = new Dictionary<string, IBase>()));
            if (dic.ContainsKey(item.Name))
                dic.Remove(item.Name);
        }

        public RestrictionDefinition GetRestrictionDefinition(string restrictionName)
        {
            if (_definitions.TryGetValue(typeof(RestrictionDefinition), out Dictionary<string, IBase> dic))
                if (dic.TryGetValue(restrictionName, out IBase item))
                    return (RestrictionDefinition)item;
            return null;
        }

        public IEnumerable<RestrictionDefinition> GetRestrictionDefinitions()
        {
            if (_definitions.TryGetValue(typeof(RestrictionDefinition), out Dictionary<string, IBase> dic))
                foreach (var item in dic.Values)
                    yield return (RestrictionDefinition)item;
        }

        #endregion Restrictions

        #region referentials

        internal void Add(ReferentialBase item)
        {

            var type = item.GetType();
            if (!_entities.TryGetValue(type, out TypesIndex types))
            {
                types = new TypesIndex(this);
                _entities.Add(type, types);
            }

            TargetListIndex typeEntity = types.GetByTypename(item.TypeEntity);
            ModelIndex dic2 = typeEntity.Get(item.TargetName);

            if (dic2.ContainsKey(item.Name))
            {

                var olditem = dic2[item.Name];

                if (olditem is INotifyPropertyChanged n1)
                    n1.PropertyChanged -= N_PropertyChanged;

                dic2.Set(item);
                //PropagateFileChanged(item);
                return;

            }

            dic2.Set(item);
            //PropagateFileChanged(item);

            if (item is INotifyPropertyChanged n2)
                n2.PropertyChanged -= N_PropertyChanged;

        }

        internal void RemoveReferential(ReferentialBase item)
        {
            var type = item.GetType();
            if (_entities.TryGetValue(type, out TypesIndex types))
            {
                var dic1 = types.GetByTypename(item.TypeEntity);
                var dic2 = dic1.Get(item.TargetName);
                if (dic2.ContainsKey(item.Name))
                    dic2.Remove(item);
            }
        }


        public ReferentialEntity GetEntity(string typeEntity, string target, string identifier)
        {
            if (_entities.TryGetValue(typeof(ReferentialEntity), out TypesIndex dic))
                if (dic.GetByTypename(typeEntity).Get(target).TryGetValue(identifier, out ReferentialBase item))
                    return (ReferentialEntity)item;
            return null;
        }

        public IEnumerable<ReferentialEntity> GetEntities(string typeEntity, string target)
        {
            if (_entities.TryGetValue(typeof(ReferentialEntity), out TypesIndex dic))
            {
                HashSet<string> _h = new HashSet<string>();
                var i = dic.GetByTypename(typeEntity).Get(target);
                foreach (var item in i.Values)
                    if (_h.Add(item.Id))
                        yield return (ReferentialEntity)item;
            }
        }

        public ReferentialRelationship GetRelationship(string typeEntity, string target, string identifier)
        {
            if (_entities.TryGetValue(typeof(ReferentialRelationship), out TypesIndex dic))
            {
                HashSet<string> _h = new HashSet<string>();
                if (dic.GetByTypename(typeEntity).Get(target).TryGetValue(identifier, out ReferentialBase item))
                    if (_h.Add(item.Id))
                        return (ReferentialRelationship)item;
            }
            return default(ReferentialRelationship);
        }

        public IEnumerable<ReferentialRelationship> GetRelationships(string typeEntity, string target)
        {
            if (_entities.TryGetValue(typeof(ReferentialRelationship), out TypesIndex dic))
            {
                HashSet<string> _h = new HashSet<string>();
                var i = dic.GetByTypename(typeEntity).Get(target);
                foreach (var item in i.Values)
                    if (_h.Add(item.Id))
                        yield return (ReferentialRelationship)item;
            }

        }

        public IEnumerable<ReferentialBase> GetReferentials(Type type)
        {

            HashSet<string> _h = new HashSet<string>();

            if (_entities.TryGetValue(type, out TypesIndex dic))
                foreach (TargetListIndex target in dic.Values)
                    foreach (ModelIndex model in target.Values)
                        foreach (var item in model.Values)
                            if (_h.Add(item.Id))
                                yield return item;

        }

        public IEnumerable<ReferentialBase> GetReferentials(Type type, string typeEntity, string target)
        {
            if (_entities.TryGetValue(type, out TypesIndex dic))
            {
                HashSet<string> _h = new HashSet<string>();
                var _targets = dic.GetByTypename(typeEntity);
                var model = _targets.Get(typeEntity);
                foreach (var item in model.Values)
                    if (_h.Add(item.Id))
                        yield return (ReferentialBase)item;
            }
        }

        public IEnumerable<ReferentialBase> GetReferentials(Type type, string typeEntity)
        {
            if (_entities.TryGetValue(type, out TypesIndex dic))
            {

                HashSet<string> _h = new HashSet<string>();
                var _targets = dic.GetByTypename(typeEntity);
                foreach (var model in _targets.Values)
                    foreach (var item in model.Values)
                        if (_h.Add(item.Id))
                            yield return item;

            }
        }

        #endregion referentials

        internal IEnumerable<T> CollectContentOfFile<T>(FileModel file)
            where T : IBase
        {

            Type type = typeof(T);

            if (typeof(ReferentialBase).IsAssignableFrom(type))
            {
                HashSet<string> _h = new HashSet<string>();
                if (_entities.TryGetValue(type, out TypesIndex types))
                    foreach (TargetListIndex target in types.Values)
                        foreach (ModelIndex index in target.Values)
                            foreach (var item in index.Values)
                                if (_h.Add(item.Id))
                                    if (item is T r && item.File.FullPath == file.FullPath)
                                        yield return r;
            }

            else if (typeof(ModelDefinition).IsAssignableFrom(type))
            {
                foreach (var definition in _definitions)
                    foreach (ModelDefinition item in definition.Value.Values.OfType<ModelDefinition>())
                        if (item is T d && item.File.FullPath == file.FullPath)
                            yield return d;
            }

            else if (type == typeof(CooperationViewpoint))
            {
                foreach (var definition in _definitions)
                    if (definition.Value != null)
                        foreach (CooperationViewpoint item in definition.Value.Values.OfType<CooperationViewpoint>())
                            if (item.File.FullPath == file.FullPath)
                                yield return (T)(object)item;
            }

            else if (type == typeof(LayersDefinition))
            {
                foreach (var definition in _definitions)
                    if (definition.Value != null)
                        foreach (LayersDefinition item in definition.Value.Values.OfType<LayersDefinition>())
                            if (item.File.FullPath == file.FullPath)
                                yield return (T)(object)item;
            }

            else if (type == typeof(RestrictionDefinition))
            {
                foreach (var definition in _definitions)
                    if (definition.Value != null)
                        foreach (RestrictionDefinition item in definition.Value.Values.OfType<RestrictionDefinition>())
                            if (item.File.FullPath == file.FullPath)
                                yield return (T)(object)item;
            }

            else
            {

            }
        }

        public IEnumerable<T> Get<T>()
         where T : IBase
        {

            Type type = typeof(T);
            QueryFilter query = new QueryFilter();

            foreach (var item in Get(type, query))
                yield return (T)item;

        }

        public IEnumerable<T> Get<T>(QueryFilter query)
         where T : IBase
        {

            Type type = typeof(T);

            foreach (var item in Get(type, query))
                yield return (T)item;
        }

        public IEnumerable<IBase> Get(Type type, QueryFilter query)
        {

            HashSet<string> _h = new HashSet<string>();

            if (typeof(ModelDefinition).IsAssignableFrom(type))
                if (_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
                    foreach (var item in dic.Values)
                        if (query.EvaluateName(item))
                            yield return item;

            if (typeof(ReferentialBase).IsAssignableFrom(type))
                if (_entities.TryGetValue(type, out TypesIndex types))
                    foreach (TargetListIndex target in query.GetTypeNames(types))
                        foreach (ModelIndex model in target.Values)
                            foreach (ReferentialBase item in model.Values)
                                if (query.EvaluateName(item))
                                    if (_h.Add(item.Id))
                                        yield return item;
        }

        public IEnumerable<IBase> Get()
        {

            foreach (var dic1 in _definitions.Values)
                foreach (var item in dic1.Values)
                    yield return item;

            HashSet<string> _h = new HashSet<string>();
            foreach (var dic2 in _entities)
                foreach (TargetListIndex target in dic2.Value.Values)
                    foreach (var model in target.Values)
                        foreach (var item in model.Values)
                            if (_h.Add(item.Id))
                                yield return item;

        }

        public void Dispose()
        {
            this.Files.StopListener();
        }



        internal Transactionfile Add(FileModel item, FileTracingEnum trace)
        {

            if (!item.Exist)
                return null;

            JObject payload = null;
            Transactionfile transaction = null;
            try
            {
                payload = item.Load();
                transaction = _loader.Add(payload, item);
            }
            catch (System.IO.IOException e1)
            {

                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();

                Diagnostic.Append(new DiagnositcMessage() { Severity = SeverityEnum.Error, File = item.FullPath, Text = e1.Message, Exception = e1 });
            }
            catch (Exception e2)
            {

                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();

                Diagnostic.Append(new DiagnositcMessage() { Severity = SeverityEnum.Error, File = item.FullPath, Text = e2.Message, Exception = e2 });
            }

            if (transaction != null)
                if (ItemFileHasChanged != null)
                    lock (_lockFile)
                    {
                        transaction.Trace = trace;
                        ItemFileHasChanged(this, transaction);
                    }

            return transaction;

        }

        private void N_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }



        private Dictionary<Type, Dictionary<string, IBase>> _definitions;
        private Dictionary<Type, TypesIndex> _entities;

        private volatile object _lockFile = new object();
        private readonly Loader _loader;
    }


}