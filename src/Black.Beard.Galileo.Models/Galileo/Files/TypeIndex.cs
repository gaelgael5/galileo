﻿using System.Collections.Generic;

namespace Bb.Galileo.Files
{
    internal class TypeIndex
    {

        public TypeIndex(ModelRepository parent)
        {
            this._parent = parent;
            _items = new Dictionary<string, ModelIndex>();
        }

        public ModelIndex Get(string typename)
        {
            if (!_items.TryGetValue(typename, out ModelIndex result))
                _items.Add(typename, (result = new ModelIndex(this._parent)));

            return result;
        }

        public IEnumerable<ModelIndex> Values { get => _items.Values; }

        private readonly ModelRepository _parent;
        private readonly Dictionary<string, ModelIndex> _items;

    }


}
