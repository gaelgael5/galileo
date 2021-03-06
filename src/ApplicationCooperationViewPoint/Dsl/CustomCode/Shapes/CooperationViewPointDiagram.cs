using System.Collections;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;
using System.Collections.Generic;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace Bb.ApplicationCooperationViewPoint
{
    public partial class CooperationViewPointDiagram
    {

        // Constructors were not generated for this class because it had HasCustomConstructor
        // set to true. Please provide the constructors below in a partial class.
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store">Store where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public CooperationViewPointDiagram(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
            : this(store != null ? store.DefaultPartitionForClass(DomainClassId) : null, propertyAssignments)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="partition">Partition where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public CooperationViewPointDiagram(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
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
                return false;
            }
        }

        protected override void InitializeShapeFields(IList<ShapeField> shapeFields)
        {

            base.InitializeShapeFields(shapeFields);

            //global::Bb.ApplicationCooperationViewPoint.CooperationShape.DecoratorsInitialized += CooperationShape.OnDecoratorsInitialized;
            //global::Bb.ApplicationCooperationViewPoint.CooperationSubShape.DecoratorsInitialized += CooperationSubShape.OnDecoratorsInitialized;
            //global::Bb.ApplicationCooperationViewPoint.ConceptSubElementShape.DecoratorsInitialized += ConceptSubElement.OnDecoratorsInitialized;
            //global::Bb.ApplicationCooperationViewPoint.ConceptShape.DecoratorsInitialized += ConceptShape.OnDecoratorsInitialized;
            //global::Bb.ApplicationCooperationViewPoint.RelationshipShape.DecoratorsInitialized += RelationshipShape.OnDecoratorsInitialized;

        }

    }

}
