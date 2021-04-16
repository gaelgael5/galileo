using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Galileo.Files.Viewpoints
{


    public class ViewpointModel
    {

        public ViewpointModel()
        {
            this._children = new List<ViewpointModelItem>();

        }

        internal void AddChildren(ViewpointModelItem viewpointModelItem)
        {
            this._children.Add(viewpointModelItem);
        }

        public IEnumerable<ViewpointModelItem> Children { get => _children; }
        public string Type { get; internal set; }

        public string Name { get; internal set; }


        private readonly List<ViewpointModelItem> _children;

    }


}
