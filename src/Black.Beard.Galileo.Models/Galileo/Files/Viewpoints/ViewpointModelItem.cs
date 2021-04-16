using Bb.Galileo.Files.Schemas;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Viewpoints
{
    public class ViewpointModelItem
    {


        public ViewpointModelItem()
        {
            this._children = new List<ViewpointModelItem>();

        }


        public IEnumerable<ViewpointModelItem> Children { get => _children; }


        public IEnumerable<ReferenceItem> References { get => _references; }


        public EntityDefinition Definition { get; internal set; }


        public RelationshipDefinition Relationship { get; internal set; }


        internal void AddChildren(ViewpointModelItem viewpointModelItem)
        {
            this._children.Add(viewpointModelItem);
        }


        internal int SetReference(FileModel file, RelationshipDefinition rel)
        {

            int result = 0;

            if (Definition != null)
            {

                if (rel.Origin.Name == Definition.Name)
                {
                    this._references.Add(new ReferenceItem() { SourceDefinition = Definition, Target = rel.GetTargetDefinition() });
                    result++;
                }

                else if (rel.Target.Name == Definition.Name)
                {
                    this._references.Add(new ReferenceItem() { SourceDefinition = Definition, Target = rel.GetOriginDefinition() });
                    result++;
                }

                foreach (ViewpointModelItem v in this._children)
                    result += v.SetReference(file, rel);

            }

            return result;

        }


        private List<ReferenceItem> _references = new List<ReferenceItem>(10);
        private readonly List<ViewpointModelItem> _children;

    }


}
