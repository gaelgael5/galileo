using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using Bb.Galileo.Files;
using System.Linq;
using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Files.Datas;

namespace Bb.ApplicationCooperationViewPoint
{

    /// <summary>
	/// DomainClass SubElement
	/// Description de Bb.ApplicationCooperationViewPoint.SubElement
	/// </summary>
    public partial class SubElement : SubElementBase
    {
        #region Constructors
        // Constructors were not generated for this class because it had HasCustomConstructor
        // set to true. Please provide the constructors below in a partial class.
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store">Store where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public SubElement(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
            : this(store != null ? store.DefaultPartitionForClass(DomainClassId) : null, propertyAssignments)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="partition">Partition where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public SubElement(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
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
            var item = new ResolveQuery(this.ReferenceSource);
            item.Kind = Galileo.ElementEnum.EntityDefinition;
            var items = item.GetReferentials(rep);
            return items.OfType<EntityDefinition>().FirstOrDefault();
        }

        internal ReferentialEntity GetEntity(ModelRepository rep)
        {
            var item = new ResolveQuery(this.ReferenceSource);
            var items = item.GetReferentials(rep);
            return items.OfType<ReferentialEntity>().FirstOrDefault();
        }

    }


}

