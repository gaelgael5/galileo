using Bb.Galileo.Files.Datas;
using Bb.Galileo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bb.Galileo.Files
{


    internal class ModelIndex
    {

        public ModelIndex(ModelRepository parent)
        {
            this._parent = parent;
            _items = new Dictionary<string, ReferentialBase>();
        }

        internal bool TryGetValue(string identifier, out ReferentialBase item)
        {

            if (identifier.Length == 32)
                if (_items.TryGetValue("i:" + identifier, out item))
                    return true;

            if (identifier.StartsWith("i:") || identifier.StartsWith("n:"))
            {
                if (_items.TryGetValue(identifier, out item))
                    return true;
            }

            if (_items.TryGetValue("n:" + identifier, out item))
                return true;

            return false;
        }

        public ReferentialBase this[string name]
        {
            get
            {
                if (this.TryGetValue(name, out ReferentialBase value))
                    return value;
                return null;
            }
        }


        internal bool ContainsKey(string name)
        {

            if (name.StartsWith("i:"))
                return _items.ContainsKey(name);

            else if (name.StartsWith("n:"))
                return _items.ContainsKey(name);

            if (_items.ContainsKey("i:" + name))
                return true;

            if (_items.ContainsKey("n:" + name))
                return true;

            return false;

        }

        internal void Set(ReferentialBase item)
        {

            var n = "n:" + item.Name;
            var i = "i:" + item.Id;

            if (!_items.ContainsKey(n))
                _items.Add(n, item);
            else
                _items[n] = item;

            if (!_items.ContainsKey(i))
                _items.Add(i, item);
            else
                _items[i] = item;

        }

        public IEnumerable<ReferentialBase> Values { get => this._items.Values; }

        private readonly ModelRepository _parent;
        private Dictionary<string, ReferentialBase> _items;

        private volatile object _lock = new object();

    }


}
