using Bb.Galileo.Viewpoints.Cooperations;
using devtm.AutoMapper;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DslModeling = global::Microsoft.VisualStudio.Modeling;

namespace Bb.ApplicationCooperationViewPoint
{



    /// <summary>
    /// DomainClass Model
    /// The root in which all other elements are embedded. Appears as a diagram.
    /// </summary>

    public partial class Model
    {


        #region Constructors
        // Constructors were not generated for this class because it had HasCustomConstructor
        // set to true. Please provide the constructors below in a partial class.
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store">Store where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public Model(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
            : this(store != null ? store.DefaultPartitionForClass(DomainClassId) : null, propertyAssignments)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="partition">Partition where new element is to be created.</param>
        /// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
        public Model(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
            : base(partition, propertyAssignments)
        {
        }
        #endregion

        internal void CreateMapper()
        {


            if (string.IsNullOrEmpty(this.ViewpointType))
                throw new Exception("Please select a viewpoint type in the propertygrid");

            var referential = ReferentialResolver.Instance.GetReferential(this.Store);
            if (referential == null)
                throw new Exception("referential can't be loded");

            var viewpointConfig = referential.GetCooperationViewpoint(this.ViewpointType);

            if (viewpointConfig == null)
                throw new Exception($"the viewpoint type '{this.ViewpointType}' can't be found. please select a valid viewpoint type");



            using (var form = new Bb.Galileo.Viewpoints.Cooperations.SelectReferential(referential))
            {

                form.SetViewpoint(viewpointConfig);
                //form.SetSelectedKeys(keys);

                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    ViewpointProjectionEntities k2 = form.GetSelectedKeys();
                    CreateItem(k2);

                }

            }


        }

        private void CreateItem(ViewpointProjectionEntities k2)
        {

            Dictionary<string, DslModeling::ModelElement> _items = new Dictionary<string, DslModeling.ModelElement>();
            using (Transaction t = this.Store.TransactionManager.BeginTransaction("automated model"))
            {
                foreach (var item in k2.Entities)
                {

                    var k = item.Entity.GetReference().ToString();

                    if (!_items.ContainsKey(k))
                    {

                        if (item.Kind == Galileo.Files.Viewpoints.ViewpointItem.Concept)
                        {

                            Concept concept = new Concept(this.Partition)
                            {
                                ReferenceSource = k,
                                Name = item.Entity.Label ?? item.Entity.Name,
                                Type = item.Entity.TypeEntity,

                            };
                            this.Concepts.Add(concept);

                            foreach (var parent in item.Children)
                            {

                                var l = parent.Entity.GetReference().ToString();
                                ConceptElement s = new ConceptElement(this.Partition)
                                {
                                    ReferenceSource = l,
                                    Name = parent.Entity.Label ?? parent.Entity.Name,
                                    Type = parent.Entity.TypeEntity,
                                };
                                concept.Children.Add(s);

                                foreach (var subParent in parent.Children)
                                {
                                    var m = subParent.Entity.GetReference().ToString();
                                    ConceptSubElement s2 = new ConceptSubElement(this.Partition)
                                    {
                                        ReferenceSource = l,
                                        Name = subParent.Entity.Label ?? subParent.Entity.Name,
                                        Type = subParent.Entity.TypeEntity,

                                    };
                                    s.Children.Add(s2);
                                }

                            }


                        }
                        else
                        {

                            ModelElement parent = new ModelElement(this.Partition)
                            {
                                ReferenceSource = k,
                                Name = item.Entity.Label ?? item.Entity.Name,
                                Type = item.Entity.TypeEntity,

                            };
                            this.Elements.Add(parent);

                            foreach (var subParent in item.Children)
                            {
                                var l = subParent.Entity.GetReference().ToString();
                                SubElement s = new SubElement(this.Partition)
                                {
                                    ReferenceSource = l,
                                    Name = subParent.Entity.Label ?? subParent.Entity.Name,
                                    Type = subParent.Entity.TypeEntity,
                                };
                                parent.Children.Add(s);
                            }

                        }

                    }
                }

                t.Commit();

            }

        }

       
    }

}