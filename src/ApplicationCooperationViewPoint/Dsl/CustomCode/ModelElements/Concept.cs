using Bb.Galileo.Files;
using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Schemas;
using System.Linq;
using DslModeling = global::Microsoft.VisualStudio.Modeling;

namespace Bb.ApplicationCooperationViewPoint
{

    /// <summary>
    /// DomainClass Concept
    /// Description de Bb.ApplicationCooperationViewPoint.Concept
    /// </summary>
    public partial class Concept : ConceptBase
	{

        #region Constructors
        // Constructors were not generated for this class because it had HasCustomConstructor
        // set to true. Please provide the constructors below in a partial class.
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store">Store where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public Concept(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
            : this(store != null ? store.DefaultPartitionForClass(DomainClassId) : null, propertyAssignments)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="partition">Partition where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public Concept(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
            : base(partition, propertyAssignments)
        {
        }
        #endregion

        internal ResolveQuery GetQuery()
        {
            return new ResolveQuery(this.ReferenceSource);
        }

        internal EntityDefinition GetDefinition(ModelRepository rep)
        {
            if (!string.IsNullOrEmpty(this.ReferenceSource))
            {
                var item = new ResolveQuery(this.ReferenceSource);
                item.Kind = Galileo.ElementEnum.EntityDefinition;
                var items = item.GetReferentials(rep);
                return items.OfType<EntityDefinition>().FirstOrDefault();
            }
            return null;
        }

        internal ReferentialEntity GetEntity(ModelRepository rep)
        {
            if (!string.IsNullOrEmpty(this.ReferenceSource))
            {
                var item = new ResolveQuery(this.ReferenceSource);
                var items = item.GetReferentials(rep);
                return items.OfType<ReferentialEntity>().FirstOrDefault();
            }

            return null;
        }

    }


}
