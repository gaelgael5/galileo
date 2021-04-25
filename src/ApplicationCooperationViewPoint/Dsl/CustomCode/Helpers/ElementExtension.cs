using System.Linq;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace Bb
{
    public static class ElementExtension
    {

        public static NodeShape ToShape(this DslModeling::ModelElement item)
        {
            NodeShape shape = (NodeShape)PresentationViewsSubject.GetPresentation(item).FirstOrDefault();
            return shape;
        }

        public static Bb.ApplicationCooperationViewPoint.Model GetDiagram(this DslModeling::ModelElement item)
        {

            if (item is Bb.ApplicationCooperationViewPoint.ModelElement me)
                return me.Model;

            else if (item is Bb.ApplicationCooperationViewPoint.Concept co)
                return co.Model;

            else if (item is Bb.ApplicationCooperationViewPoint.SubElement se)
                return se.Parent.Model;
            
            else if (item is Bb.ApplicationCooperationViewPoint.ConceptElement ce)
                return ce.Parent.Model;

            else if (item is Bb.ApplicationCooperationViewPoint.ConceptSubElement cs)
                return cs.Parent.Parent.Model;

            else if (item is Bb.ApplicationCooperationViewPoint.Relationship re)
                return re.Model;

            return null;

        }


    }


}
