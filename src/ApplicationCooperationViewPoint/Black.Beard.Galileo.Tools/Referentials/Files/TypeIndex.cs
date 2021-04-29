using System.Collections.Generic;

namespace Bb.Galileo.Files
{
    internal class TypesIndex
    {

        public TypesIndex(ModelRepository parent)
        {
            this._parent = parent;
            _items = new Dictionary<string, TargetListIndex>();
        }

        public TargetListIndex GetByTypename(string typename)
        {
            if (!_items.TryGetValue(typename, out TargetListIndex result))
                _items.Add(typename, (result = new TargetListIndex(this._parent, typename)));

            return result;
        }

        public TargetListIndex GetIfExistsByTypename(string typename)
        {
            _items.TryGetValue(typename, out TargetListIndex result);
            return result;
        }

        public IEnumerable<TargetListIndex> Values { get => _items.Values; }

        private readonly ModelRepository _parent;
        private readonly Dictionary<string, TargetListIndex> _items;

    }

    internal class TargetListIndex
    {

        public TargetListIndex(ModelRepository parent, string typeEntity)
        {
            this._parent = parent;
            this.TypeEntity = typeEntity;
            _items = new Dictionary<string, ModelIndex>();
        }

        public ModelIndex Get(string targetName)
        {
            if (!_items.TryGetValue(targetName, out ModelIndex result))
                _items.Add(targetName, (result = new ModelIndex(this._parent, targetName)));

            return result;
        }

        public IEnumerable<ModelIndex> Values { get => _items.Values; }

        private readonly ModelRepository _parent;

        public string TypeEntity { get; }

        private readonly Dictionary<string, ModelIndex> _items;

    }

}
