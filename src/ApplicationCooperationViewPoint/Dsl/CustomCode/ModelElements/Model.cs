using Bb.Galileo.Files.Datas;
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

            using (Transaction t = this.Store.TransactionManager.BeginTransaction("automated model"))
            {

                foreach (var item in k2.Entities)
                {
                    if (item.Kind == Galileo.Files.Viewpoints.ViewpointItem.Concept)
                    {
                        Concept concept = GetConcept(item);
                        foreach (var parent in item.Children)
                        {
                            ConceptElement s = GetConceptElement(concept, parent);
                            foreach (var subParent in parent.Children)
                                GetConceptSubElement(s, subParent);
                        }
                    }
                    else
                    {
                        ModelElement parent = GetModelElement(item);
                        foreach (var subParent in item.Children)
                            GetSubElement(parent, subParent);
                    }

                }

                //    t.Commit();
                //}
                //using (Transaction t = this.Store.TransactionManager.BeginTransaction("automated model"))
                //{

                GenerateRelationships(k2);

                t.Commit();

            }



        }

        private void GenerateRelationships(ViewpointProjectionEntities k2)
        {
            foreach (ViewpointProjectionEntity item in k2.Entities)
                GenerateRelationships(item);
        }

        private void GenerateRelationships(ViewpointProjectionEntity item)
        {

            var left = GetModel(item.Entity);

            foreach (ViewpointProjectionRelationship rel1 in item.Relationships)
            {
                var sh = new Relationship(this.Partition);
                this.Relationships.Add(sh);
                sh.ReferenceSource = rel1.Relationship.GetReference().ToString();

                sh.Name = rel1.Relationship.Name;
                sh.Label = rel1.Relationship.Description;
                
                SetLeftRelationship(sh, left);
                SetRightRelationship(sh, GetModel(rel1.TargetEntity));
            }

            foreach (ViewpointProjectionEntity item2 in item.Children)
                GenerateRelationships(item2);

        }

        private static void SetLeftRelationship(Relationship sh, DslModeling.ModelElement element)
        {

            if (element is Concept co)
                sh.LeftConcept = co;

            else if (element is ConceptElement ce)
                sh.LeftConceptElement = ce;

            else if (element is ConceptSubElement cse)
                sh.LeftConceptSubElement = cse;

            else if (element is ModelElement me)
                sh.LeftModelElement = me;

            else if (element is SubElement se)
                sh.LeftSubElement = se;

        }

        private static void SetRightRelationship(Relationship sh, DslModeling.ModelElement element)
        {

            if (element is Concept co)
                sh.RightConcept = co;

            else if (element is ConceptElement ce)
                sh.RightConceptElement = ce;

            else if (element is ConceptSubElement cse)
                sh.RightConceptSubElement = cse;

            else if (element is ModelElement me)
                sh.RightModelElement = me;

            else if (element is SubElement se)
                sh.RightSubElement = se;

        }

        private DslModeling::ModelElement GetModel(ReferentialEntity toFind)
        {

            var key = toFind.GetReference().ToString();

            foreach (Concept concept in this.Concepts)
            {

                if (concept.ReferenceSource == key)
                    return concept;

                foreach (ConceptElement element in concept.Children)
                {

                    if (element.ReferenceSource == key)
                        return element;

                    foreach (ConceptSubElement cse in element.Children)
                        if (cse.ReferenceSource == key)
                            return cse;

                }

            }

            foreach (ModelElement element in this.Elements)
            {

                if (element.ReferenceSource == key)
                    return element;

                foreach (SubElement ce in element.Children)
                    if (ce.ReferenceSource == key)
                        return ce;

            }

            return null;

        }

        private SubElement GetSubElement(ModelElement parent, ViewpointProjectionEntity config)
        {

            var key = config.Entity.GetReference().ToString();
            SubElement element = parent.Children.FirstOrDefault(c => c.ReferenceSource == key);

            if (element == null)
            {
                element = new SubElement(this.Partition)
                {
                    ReferenceSource = key,
                    Name = config.Entity.Label ?? config.Entity.Name,
                    Type = config.Entity.TypeEntity,
                };
                parent.Children.Add(element);
            }

            return element;

        }

        private ModelElement GetModelElement(ViewpointProjectionEntity config)
        {

            var key = config.Entity.GetReference().ToString();
            var element = this.Elements.FirstOrDefault(c => c.ReferenceSource == key);

            if (element == null)
            {
                element = new ModelElement(this.Partition)
                {
                    ReferenceSource = key,
                    Name = config.Entity.Label ?? config.Entity.Name,
                    Type = config.Entity.TypeEntity,

                };
                this.Elements.Add(element);
            }

            return element;

        }

        private ConceptSubElement GetConceptSubElement(ConceptElement parent, ViewpointProjectionEntity config)
        {

            var key = config.Entity.GetReference().ToString();
            ConceptSubElement element = parent.Children.FirstOrDefault(c => c.ReferenceSource == key);

            if (element == null)
            {
                element = new ConceptSubElement(this.Partition)
                {
                    ReferenceSource = key,
                    Name = config.Entity.Label ?? config.Entity.Name,
                    Type = config.Entity.TypeEntity,

                };
                parent.Children.Add(element);
            }

            return element;

        }

        private ConceptElement GetConceptElement(Concept parent, ViewpointProjectionEntity config)
        {

            var key = config.Entity.GetReference().ToString();
            ConceptElement element = parent.Children.FirstOrDefault(c => c.ReferenceSource == key);

            if (element == null)
            {
                element = new ConceptElement(this.Partition)
                {
                    ReferenceSource = key,
                    Name = config.Entity.Label ?? config.Entity.Name,
                    Type = config.Entity.TypeEntity,
                };
                parent.Children.Add(element);
            }

            return element;

        }

        private Concept GetConcept(ViewpointProjectionEntity config)
        {

            var key = config.Entity.GetReference().ToString();

            var element = this.Concepts.FirstOrDefault(c => c.ReferenceSource == key);

            if (element == null)
            {
                element = new Concept(this.Partition)
                {
                    ReferenceSource = key,
                    Name = config.Entity.Label ?? config.Entity.Name,
                    Type = config.Entity.TypeEntity,
                };

                this.Concepts.Add(element);
            }

            return element;

        }

    }

}