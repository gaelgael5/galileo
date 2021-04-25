using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Bb.ApplicationCooperationViewPoint
{
    public class ShowMenu : ToggleButtonField
    {

        public ShowMenu(string name)
            : base(name)
        {
            this._items = new List<Decorator>();

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
                return "Show menu";
            }
            return "";
        }

        public override string GetAccessibleDescription(ShapeElement parentShape)
        {
            return "Show menu";
        }

        public override string GetAccessibleName(ShapeElement parentShape)
        {
            object obj2 = this.GetValue(parentShape);
            if (!((obj2 != null) && ((bool)obj2)))
            {
                return "Menu";
            }
            return "Menu";
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
            return Bb.ApplicationCooperationViewPoint.CustomCode.Resources.ResourceImages.arrowbottom;
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

        

        }

        internal void AddSubMenu(Decorator decorator)
        {
            this._items.Add(decorator);
        }

        private List<Decorator> _items;

    }


}

