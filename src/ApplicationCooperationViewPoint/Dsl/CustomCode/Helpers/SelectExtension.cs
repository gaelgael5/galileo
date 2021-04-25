using System;
using System.Linq;
using System.Collections.Generic;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;
using DslValidation = global::Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace Bb
{

    public static class SelectExtension
    {

        public static void Select(IEnumerable<DslModeling::ModelElement> items)
        {

            List<NodeShape> shapes = new List<NodeShape>(items.Count());
            foreach (var item in items)
            {
                NodeShape shape = (NodeShape)PresentationViewsSubject.GetPresentation(item).FirstOrDefault();
                shapes.Add(shape);
            }

            Select(shapes);

        }

        public static void Select(IEnumerable<NodeShape> items)
        {

            try
            {
                var first = items.First();
                Diagram diagram = first.Diagram;

                if (diagram != null)
                {
                    DiagramView diagramView = diagram.ActiveDiagramView;
                    if ((diagramView != null))
                    {

                        foreach (var item in items)
                        {
                            if (item.CanFocus)
                                diagramView.Selection.Add(new DiagramItem(item));
                        }

                        diagramView.DiagramClientView.EnsureVisible(first.AbsoluteBoundingBox, first.EnsureVisiblePreference);
                        diagramView.Selection.Add(diagramView.Selection.FocusedItem);


                    }

                }

            }
            catch (Exception)
            {
            }

        }

    }


}
