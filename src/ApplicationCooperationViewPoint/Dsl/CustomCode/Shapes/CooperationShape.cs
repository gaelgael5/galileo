using System.Collections;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Collections.Generic;

using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling;

namespace Bb.ApplicationCooperationViewPoint
{
    public partial class CooperationShape
    {



        // Constructors were not generated for this class because it had HasCustomConstructor
        // set to true. Please provide the constructors below in a partial class.
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store">Store where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public CooperationShape(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
            : this(store != null ? store.DefaultPartitionForClass(DomainClassId) : null, propertyAssignments)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="partition">Partition where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public CooperationShape(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
            : base(partition, propertyAssignments)
        {
        }

        /// <summary>
        /// Gets the child compartment shape and verifies whether it can resize its parent compartment shape.
        /// </summary>
        /// <value></value>
        /// <returns>true if a child compartment shape can resize its parent compartment shape; otherwise, false.
        /// </returns>
        public override bool AllowsChildrenToResizeParent
        {
            get
            {
                // Overridden to return false so that the visual layout issue is solved, where a shape with a connector that "has custom source"
                // resizes to make the source shape wider until it reaches the edge of the referenced shape.
                // See http://social.msdn.microsoft.com/Forums/en-US/vsx/thread/4cc74f9e-1949-43ba-8407-934f6664167d.
                // It is applied to the base class here so that all inheriting compartment shapes automatically have this fix applied.
                return true;
            }
        }

        public override ResizeDirection AllowsChildrenToShrinkParent => base.AllowsChildrenToShrinkParent;

        public override void DrawResizeFeedback(DiagramPaintEventArgs e, RectangleD bounds)
        {
            base.DrawResizeFeedback(e, bounds);
        }

        protected override void InitializeShapeFields(IList<ShapeField> shapeFields)
        {
            base.InitializeShapeFields(shapeFields);
            global::Bb.ApplicationCooperationViewPoint.CooperationShape.DecoratorsInitialized += OnDecoratorsInitialized;
        }

        //public override void OnClick(DiagramPointEventArgs e)
        //{

        //    base.OnClick(e);

        //    //if (this.ModelElement is ModelElement me)
        //    //{
        //    //    using (Transaction t = this.Store.TransactionManager.BeginTransaction("automated model"))
        //    //    {
        //    //        ModelElementBase.ShowMenuPropertyHandler.Instance.NotifyValueChange(me);
        //    //        //t.Commit();
        //    //    }
        //    //}

        //}

        protected override void InitializeDecorators(IList<ShapeField> shapeFields, IList<Decorator> decorators)
        {

            base.InitializeDecorators(shapeFields, decorators);

            ShapeField field;

            double y = 0.25;
            ShowMenu menu = new ShowMenu("ShowMenu");
            //ShowMenu menu = (ShowMenu)DslDiagrams::ShapeElement.FindShapeField(shapeFields, "ShowMenu");
            Decorator decoratorMenu = new ShapeDecorator(menu, ShapeDecoratorPosition.OuterTopRight, new PointD(0, y));
            decorators.Add(decoratorMenu);

            //y += 0.23;
            ////field = DslDiagrams::ShapeElement.FindShapeField(shapeFields, "SelectWithChildren");
            //field = new SelectParentChildren("SelectWithChildren");
            ////shapeFields.Add(field);
            //Decorator decorator = new ShapeDecorator(field, ShapeDecoratorPosition.OuterTopRight, new PointD(0, y));
            //decorators.Add(decorator);
            //menu.AddSubMenu(decorator);

        }


        public static void OnDecoratorsInitialized(object sender, global::System.EventArgs e)
        {
            DslDiagrams::ShapeElement shape = (DslDiagrams::ShapeElement)sender;
            DslDiagrams::AssociatedPropertyInfo propertyInfo;
            propertyInfo = new DslDiagrams::AssociatedPropertyInfo(global::Bb.ApplicationCooperationViewPoint.ModelElement.ShowMenuDomainPropertyId);
            DslDiagrams::ShapeElement.FindDecorator(shape.Decorators, "ShowMenu").AssociateVisibilityWith(shape.Store, propertyInfo);
        }


        public override bool ShouldAutoPlaceChildShapes => true;

       

        private static ArrayList customOutlineDashPattern;


    }


}

