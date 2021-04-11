using Bb.Galileo.Files;
using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Files.Viewpoints;
using Bb.Galileo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Bb.Galileo.Files
{

    public class ModelRepository : IDisposable
    {


        public ModelRepository(string folder, IDiagnostic diagnostic)
        {
            this._loader = new Loader(this);
            this.SchemaManager = new SchemaManager(this);
            this._loader.LoadConfig(new DirectoryInfo(folder));
            this.Files = new FileRepository(folder, diagnostic, this);
            this.Diagnostic = diagnostic;
            this._definitions = new Dictionary<Type, Dictionary<string, IBase>>();
            this._entities = new Dictionary<Type, TypeIndex>();
        }

        public void Initialize()
        {
            this.Files.StartListener();
        }

        public SchemaManager SchemaManager { get; }

        public FileRepository Files { get; }

        public IDiagnostic Diagnostic { get; }

        public ConfigModel Config { get; internal set; }

        public Action<ModelRepository, Transactionfile> ItemFileHasChanged { get; set; }


        #region Layers

        internal void Add(LayersDefinition item)
        {

            var type = item.GetType();
            if (!_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
                _definitions.Add(type, (dic = new Dictionary<string, IBase>()));

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
                _definitions.Add(type, (dic = new Dictionary<string, IBase>()));

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

        internal void RemoveCooperationViewpoint(CooperationViewpoint item)
        {
            var type = typeof(CooperationViewpoint);
            if (!_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
                _definitions.Add(type, (dic = new Dictionary<string, IBase>()));

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
                _definitions.Add(type, (dic = new Dictionary<string, IBase>()));

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

        #region referentials

        internal void Add(ReferentialBase item)
        {

            var type = item.GetType();
            if (!_entities.TryGetValue(type, out TypeIndex types))
            {
                types = new TypeIndex(this);
                _entities.Add(type, types);
            }

            var dic = types.Get(item.TypeEntity);

            if (dic.ContainsKey(item.Name))
            {

                var olditem = dic[item.Name];

                if (olditem is INotifyPropertyChanged n1)
                    n1.PropertyChanged -= N_PropertyChanged;

                dic.Set(item);
                //PropagateFileChanged(item);
                return;

            }

            dic.Set(item);
            //PropagateFileChanged(item);

            if (item is INotifyPropertyChanged n2)
                n2.PropertyChanged -= N_PropertyChanged;

        }

        internal void RemoveReferential(ReferentialBase item)
        {
            var type = item.GetType();
            if (_entities.TryGetValue(type, out TypeIndex types))
            {
                var dic = types.Get(item.TypeEntity);
                if (dic.ContainsKey(item.Name))
                    dic.Remove(item);
            }
        }


        public ReferentialEntity GetEntity(string typeEntity, string identifier)
        {
            if (_entities.TryGetValue(typeof(ReferentialEntity), out TypeIndex dic))
                if (dic.Get(typeEntity).TryGetValue(identifier, out ReferentialBase item))
                    return (ReferentialEntity)item;
            return null;
        }

        public ReferentialRelationship GetRelationship(string typeEntity, string identifier)
        {
            if (_entities.TryGetValue(typeof(ReferentialRelationship), out TypeIndex dic))
                if (dic.Get(typeEntity).TryGetValue(identifier, out ReferentialBase item))
                    return (ReferentialRelationship)item;
            return default(ReferentialRelationship);
        }

        public IEnumerable<ReferentialBase> GetReferentials(Type type)
        {
            if (_entities.TryGetValue(type, out TypeIndex dic))
                foreach (ModelIndex model in dic.Values)
                    foreach (var item in model.Values)
                        yield return (ReferentialBase)item;
        }

        public IEnumerable<ReferentialBase> GetReferentials(Type type, string typeEntity)
        {
            if (_entities.TryGetValue(type, out TypeIndex dic))
            {
                var model = dic.Get(typeEntity);
                foreach (var item in model.Values)
                    yield return (ReferentialBase)item;
            }
        }

        #endregion referentials

        internal IEnumerable<T> CollectFile<T>(FileModel file)
            where T : IBase
        {

            Type type = typeof(T);

            if (typeof(ReferentialBase).IsAssignableFrom(type))
            {
                if (_entities.TryGetValue(type, out TypeIndex types))
                    foreach (ModelIndex index in types.Values)
                        foreach (var item in index.Values)
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
                    foreach (CooperationViewpoint item in definition.Value.Values.OfType<CooperationViewpoint>())
                        if (item.File.FullPath == file.FullPath)
                            yield return (T)(object)item;
            }
            else if (type == typeof(LayersDefinition))
            {
                foreach (var definition in _definitions)
                    foreach (LayersDefinition item in definition.Value.Values.OfType<LayersDefinition>())
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

            if (typeof(ModelDefinition).IsAssignableFrom(type))
                if (_definitions.TryGetValue(type, out Dictionary<string, IBase> dic))
                    foreach (var item in dic.Values)
                        if (item is T i)
                            yield return i;

            if (typeof(ReferentialBase).IsAssignableFrom(type))
                if (_entities.TryGetValue(type, out TypeIndex dic))
                    foreach (ModelIndex model in dic.Values)
                        foreach (object item in model.Values)
                            if (item is T i)
                                yield return (T)item;

        }


        public IEnumerable<IBase> Get()
        {

            foreach (var dic1 in _definitions.Values)
                foreach (var item in dic1.Values)
                    yield return item;

            foreach (var dic2 in _entities)
                foreach (ModelIndex model in dic2.Value.Values)
                    foreach (var item in model.Values)
                        yield return item;

        }

        public void Dispose()
        {
            this.Files.StopListener();
        }



        internal Transactionfile Add(FileModel item, FileTracingEnum trace)
        {

            JObject payload = null;
            Transactionfile transaction = null;
            try
            {
                payload = item.Load();
                transaction = _loader.Add(payload, item);
                transaction.Trace = trace;
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

        private void N_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }



        private Dictionary<Type, Dictionary<string, IBase>> _definitions;
        private Dictionary<Type, TypeIndex> _entities;

        private volatile object _lockFile = new object();
        private readonly Loader _loader;
    }


}


/*
 
         internal void RemoveFile(FileModel file, Type type)
        {

            if (typeof(ReferentialBase).IsAssignableFrom(type))
            {
                if (_entities.TryGetValue(type, out TypeIndex types))
                    foreach (ModelIndex index in types.Values)
                    {
                        List<ReferentialBase> _items = new List<ReferentialBase>();
                        foreach (var item in index.Values)
                            if (item.File.FullPath == file.FullPath)
                                _items.Add(item);

                        foreach (var item in _items)
                            index.Remove(item);

                    }
            }

            else if (typeof(ModelDefinition).IsAssignableFrom(type))
            {
                foreach (var definition in _definitions)
                {
                    List<ModelDefinition> _items = new List<ModelDefinition>();
                    foreach (ModelDefinition item in definition.Value.Values.OfType<ModelDefinition>())
                        if (item.File.FullPath == file.FullPath)
                            _items.Add(item);

                    foreach (var item in _items)
                        definition.Value.Remove(item.Name);

                }
            }

            else if (type == typeof(CooperationViewpoint))
            {
                foreach (var definition in _definitions)
                {
                    List<CooperationViewpoint> _items = new List<CooperationViewpoint>();
                    foreach (CooperationViewpoint item in definition.Value.Values.OfType<CooperationViewpoint>())
                        if (item.File.FullPath == file.FullPath)
                            _items.Add(item);

                    foreach (var item in _items)
                        definition.Value.Remove(item.Name);

                }
            }
            else if (type == typeof(LayersDefinition))
            {
                foreach (var definition in _definitions)
                {
                    List<LayersDefinition> _items = new List<LayersDefinition>();
                    foreach (LayersDefinition item in definition.Value.Values.OfType<LayersDefinition>())
                        if (item.File.FullPath == file.FullPath)
                            _items.Add(item);

                    foreach (var item in _items)
                        definition.Value.Remove(item.Name);

                }
            }
            else
            {

            }
        }

 */