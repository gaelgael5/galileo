using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Drawing;
using System.Windows.Forms;

namespace Bb.ApplicationCooperationViewPoint
{
    public class SelectParentChildren : ToggleButtonField
    {


        public SelectParentChildren(string name)
            : base(name)
        {

        }


        public override void AccessibleDoDefaultAction(ShapeElement parentShape)
        {
            NodeShape shape = parentShape as NodeShape;
            if (shape != null)
            {
                Execute(shape);
            }
        }

        public override string GetAccessibleDefaultActionDescription(ShapeElement parentShape)
        {
            object obj2 = this.GetValue(parentShape);
            if (!((obj2 != null) && ((bool)obj2)))
            {
                return "";
            }
            return "";
        }

        public override string GetAccessibleDescription(ShapeElement parentShape)
        {
            return "";
        }

        public override string GetAccessibleName(ShapeElement parentShape)
        {
            object obj2 = this.GetValue(parentShape);
            if (!((obj2 != null) && ((bool)obj2)))
            {
                return "";
            }
            return "";
        }

        public override AccessibleRole GetAccessibleRole(ShapeElement parentShape)
        {
            return AccessibleRole.PushButton;
        }

        public override AccessibleStates GetAccessibleState(ShapeElement parentShape, DiagramClientView diagramClientView)
        {
            return base.GetAccessibleState(parentShape, diagramClientView);
        }

        protected override Image GetButtonImage(ShapeElement parentShape)
        {
            return Bb.ApplicationCooperationViewPoint.CustomCode.Resources.ResourceImages.hierachy;
        }

        public override MouseAction GetPotentialMouseAction(MouseButtons mouseButtons, PointD point, DiagramHitTestInfo hitTestInfo)
        {
            DiagramItem hitDiagramItem = hitTestInfo.HitDiagramItem;
            Diagram diagram = (hitDiagramItem != null) ? hitDiagramItem.Diagram : null;
            if (diagram == null)
            {
                return null;
            }
            return diagram.SelectAction;
        }

        public override void OnMouseUp(DiagramMouseEventArgs e)
        {
            //base.OnMouseUp(e);
            if (!e.Handled)
            {
                //bool flag = DiagramClientView.IsModifierKeyDown(Keys.Control);
                //bool flag2 = DiagramClientView.IsModifierKeyDown(Keys.Shift);
                if (e.Button == MouseButtons.Left)
                {
                    var shape = e.DiagramHitTestInfo.HitDiagramItem.Shape as NodeShape;
                    Execute(shape);
                    e.Handled = true;
                }
            }
        }


        private void Execute(NodeShape shape)
        {

            Microsoft.VisualStudio.Modeling.Diagrams.ShapeElement aa = shape as Microsoft.VisualStudio.Modeling.Diagrams.ShapeElement;
            //shape.ParentShape
            NodeShape pp = aa.ParentShape as NodeShape;

            if (pp.ModelElement is Concept)
            {


            }
            else if (pp.ModelElement is ConceptElement ce)
            {


            }
            else if (pp.ModelElement is ConceptSubElement cs)
            {


            }
            else if (pp.ModelElement is ModelElement me)
            {


            }
            else if (pp.ModelElement is SubElement se)
            {


            }

        }


    }


}

