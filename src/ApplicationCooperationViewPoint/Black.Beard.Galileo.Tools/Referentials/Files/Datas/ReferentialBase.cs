using Bb.Galileo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Bb.Galileo.Files.Datas
{
    public class ReferentialBase : INotifyPropertyChanged, IBase
    {

        public ReferentialBase(string type, FileModel file)
        {

            this.TypeEntity = type;
            this.Kind = GetType().Name;
            this.File = file;
            this._propertiesStorage = new Dictionary<string, object>();
        }


        public string Id
        {
            get => (string)this[nameof(Id)];
            set => this[nameof(Id)] = value;
        }

        // public ReferenceResolver<IBase> ReferenceSchema { get; internal set; }

        public string Name
        {
            get => (string)this[nameof(Name)];
            set => this[nameof(Name)] = value;
        }

        public string TypeEntity { get; }

        public string Kind { get; }

        public string Target { get; internal set; }

        public SchemaReference Schema { get; internal set; }


        [JsonIgnore]
        public FileModel File { get; }

        public bool Changed { get; private set; }

        public object this[string propertyName]
        {

            get
            {
                if (this._propertiesStorage.TryGetValue(propertyName, out object value))
                    return value;
                return null;
            }
            set
            {

                if (this._propertiesStorage.TryGetValue(propertyName, out object oldValue))
                {
                    if (object.Equals(oldValue, value))
                        return;
                    this._propertiesStorage[propertyName] = value;
                }
                else
                {
                    if (object.Equals(value, null))
                        return;
                    this._propertiesStorage.Add(propertyName, value);
                }

                this.Changed = true;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }
        }


        public IEnumerable<string> PropertyKeys()
        {

            var items = this._propertiesStorage.Keys.OrderBy(c => c).ToList();
            HashSet<string> _h = new HashSet<string>();

            if (_propertiesStorage.ContainsKey(nameof(ReferentialBase.Id)))
            {
                _h.Add(nameof(ReferentialBase.Id));
                yield return nameof(ReferentialBase.Id);
            }

            if (_propertiesStorage.ContainsKey(nameof(ReferentialBase.Name)))
            {
                _h.Add(nameof(ReferentialBase.Name));
                yield return nameof(ReferentialBase.Name);
            }

            if (Kind != "links")
            {

                if (_propertiesStorage.ContainsKey(nameof(ReferentialEntity.Label)))
                {
                    _h.Add(nameof(ReferentialEntity.Label));
                    yield return nameof(ReferentialEntity.Label);
                }

                if (_propertiesStorage.ContainsKey(nameof(ReferentialEntity.Description)))
                {
                    _h.Add(nameof(ReferentialEntity.Description));
                    yield return nameof(ReferentialEntity.Description);
                }

            }


            foreach (var item in items)
                if (_h.Add(item))
                    yield return item;

        }


        public override string ToString()
        {
            return Name.ToString();
        }

        internal void ResetChanges()
        {
            this.Changed = false;
        }


        private readonly Dictionary<string, object> _propertiesStorage;

        public event PropertyChangedEventHandler PropertyChanged;

    }


}
