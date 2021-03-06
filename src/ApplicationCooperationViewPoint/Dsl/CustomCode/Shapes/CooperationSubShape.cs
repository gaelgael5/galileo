using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace Bb.ApplicationCooperationViewPoint
{

    public partial class CooperationSubShape
    {


        // Constructors were not generated for this class because it had HasCustomConstructor
        // set to true. Please provide the constructors below in a partial class.
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store">Store where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public CooperationSubShape(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
            : this(store != null ? store.DefaultPartitionForClass(DomainClassId) : null, propertyAssignments)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="partition">Partition where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public CooperationSubShape(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
            : base(partition, propertyAssignments)
        {
        }

 

    }

}

