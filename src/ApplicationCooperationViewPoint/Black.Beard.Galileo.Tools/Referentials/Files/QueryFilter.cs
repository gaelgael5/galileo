using Bb.Galileo.Files.Datas;
using Bb.Galileo.Models;
using System.Collections.Generic;

namespace Bb.Galileo.Files
{
    public class QueryFilter
    {

        public QueryFilter()
        {
            this._names = new HashSet<string>();
            this._typeNames = new HashSet<string>();
        }

        internal bool EvaluateName(IBase item)
        {
            if (_names.Count == 0 || _names.Contains("n:" + item.Name) || _names.Contains(item.Name))
                return true;

            if (item is ReferentialBase r && _names.Contains("i:" + r.Id))
                return true;
            
            return false;

        }

        internal IEnumerable<TargetListIndex> GetTypeNames(TypesIndex item)
        {
            if (this._typeNames.Count == 0)
            {
                foreach (var item2 in item.Values)
                    yield return item2;
            }
            else
            {

                foreach (var type in this._typeNames)
                {
                    var t = item.GetIfExistsByTypename(type);
                    if (t != null)
                        yield return t;
                }

            }
        }


        public void AddName(string name)
        {
            this._names.Add(name);
        }

        internal void AddTypeName(string typeName)
        {
            this._typeNames.Add(typeName);
        }

        private HashSet<string> _names;
        private HashSet<string> _typeNames;


    }


}
