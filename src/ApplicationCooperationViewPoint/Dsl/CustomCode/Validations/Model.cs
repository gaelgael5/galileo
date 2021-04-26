using Microsoft.VisualStudio.Modeling.Validation;
using System.Linq;
using System;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using Bb.Galileo.Files;
using System.Diagnostics;
using Bb.Galileo.Files.Datas;
using Microsoft.VisualStudio.Modeling;

namespace Bb.ApplicationCooperationViewPoint
{
    /// <summary>
    /// DomainClass Model
    /// The root in which all other elements are embedded. Appears as a diagram.
    /// </summary>

    [ValidationState(ValidationState.Enabled)]
    public partial class Model
    {


        public enum CodeErrorEnum
        {
            InternalError,
            MissingConfiguration,
            BadConfiguration,
        }

        [ValidationMethod(ValidationCategories.Open | ValidationCategories.Save)]
        private void ValidateAttributeNameAsValidIdentifier(ValidationContext context)
        {

            bool changed = false;

            if (string.IsNullOrEmpty(this.ViewpointType))
                context.LogError($"Please specify the property '{nameof(Model.ViewpointType)}' in viewpoint type in the propertygrid", CodeErrorEnum.MissingConfiguration.ToString(), this);

            ModelRepository referential = null;

            try
            {
                referential = ReferentialResolver.Instance.GetReferential(this.Store);
                if (referential == null)
                {
                    context.LogError($"failed to access referential", CodeErrorEnum.InternalError.ToString(), this);
                    return;
                }
            }
            catch (Exception ex)
            {

                Trace.WriteLine(ex.ToString());

                if (referential == null)
                    context.LogError($"failed to access referential.", CodeErrorEnum.InternalError.ToString(), this);
                return;
            }

            var viewpointConfig = referential.GetCooperationViewpoint(this.ViewpointType);

            if (viewpointConfig == null)
                context.LogError($"the viewpoint type '{this.ViewpointType}' can't be found. Please fixup the property '{nameof(Model.ViewpointType)}'", CodeErrorEnum.BadConfiguration.ToString(), this);

            using (Transaction t = this.Store.TransactionManager.BeginTransaction("automated model"))
            {

                foreach (Concept concept in this.Concepts)
                {

                    if (EvaluateReference(context, referential, concept))
                        changed = true;

                    if (!string.IsNullOrEmpty(concept.Type))
                    {

                        var configConcept = viewpointConfig.Concepts.FirstOrDefault(c => c.Name == concept.Type);
                        if (configConcept == null)
                            context.LogError($"failed to resolve '{concept.Name}' in the configuration viewpoint '{this.ViewpointType}'", CodeErrorEnum.BadConfiguration.ToString(), concept);
                        else
                            if (ValidateConcept(concept, configConcept, context, referential))
                            changed = true;

                    }

                }

                foreach (var modelElement in this.Elements)
                {

                    if (EvaluateReference(context, referential, modelElement))
                        changed = true;

                    if (!string.IsNullOrEmpty(modelElement.Type))
                    {
                        var configModelElement = viewpointConfig.Elements.FirstOrDefault(c => c.Name == modelElement.Type);
                        if (configModelElement == null)
                            context.LogError($"failed to resolve '{modelElement.Name}' in the configuration viewpoint '{this.ViewpointType}'", CodeErrorEnum.BadConfiguration.ToString(), modelElement);
                        else
                            if (ValidateModelElement(modelElement, configModelElement, context, referential))
                            changed = true;
                    }

                }

                if (changed)
                    t.Commit();

            }
        }


        private bool ValidateModelElement(ModelElement modelElement, Galileo.Files.Viewpoints.CooperationRootElement configModelElement, ValidationContext context, Galileo.Files.ModelRepository referential)
        {

            bool changed = false;

            foreach (SubElement subElement in modelElement.Children)
                if (EvaluateReference(context, referential, subElement))
                    changed = true;

            return changed;

        }

        private bool ValidateConcept(Concept concept, Galileo.Files.Viewpoints.CooperationConcept configConcept, ValidationContext context, Galileo.Files.ModelRepository referential)
        {

            bool changed = false;

            foreach (ConceptElement conceptElement in concept.Children)
            {

                if (EvaluateReference(context, referential, conceptElement))
                    changed = true;

                if (!string.IsNullOrEmpty(conceptElement.Type))
                {

                    var configConceptElement = conceptElement.Children.FirstOrDefault(c => c.Type == conceptElement.Type);
                    if (configConceptElement == null)
                        context.LogError($"failed to resolve '{concept.Name}' in the configuration viewpoint '{this.ViewpointType}'", CodeErrorEnum.BadConfiguration.ToString(), conceptElement);

                    else
                        if (ValidateConceptElement(conceptElement, configConceptElement, context, referential))
                        changed = true;

                }

            }

            return changed;

        }

        private bool ValidateConceptElement(ConceptElement conceptElement, ConceptSubElement configConceptElement, ValidationContext context, Galileo.Files.ModelRepository referential)
        {

            var changed = false;

            foreach (ConceptSubElement conceptSubElement in conceptElement.Children)
                if (EvaluateReference(context, referential, conceptSubElement))
                    changed = true;

            return changed;

        }






        private bool EvaluateReference(ValidationContext context, ModelRepository referential, Concept element)
        {

            bool changed = false;

            var definition = element.GetDefinition(referential);
            if (definition == null)
                context.LogError($"failed to resolve definition '{element.Type}'", CodeErrorEnum.BadConfiguration.ToString(), element);

            else if (string.IsNullOrEmpty(element.ReferenceSource))
                context.LogError($"missing reference in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), element);

            else
            {
                var item = element.GetEntity(referential);
                if (item == null)
                {
                    if (!string.IsNullOrEmpty(element.Type))
                    {
                        element.Type = string.Empty;
                        changed = true;
                    }
                    context.LogError($"Reference '{element.ReferenceSource}' can't be resolved in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), element);
                }

                else
                {
                    if (item is ReferentialEntity e)
                    {
                        if (e.TypeEntity != element.Type)
                        {
                            element.Type = e.TypeEntity;
                            changed = true;
                        }
                        if (e.Name != element.Name)
                        {
                            element.Name = e.Name;
                            changed = true;
                        }
                    }
                }

            }

            return changed;
        }

        private bool EvaluateReference(ValidationContext context, ModelRepository referential, ConceptElement element)
        {

            bool changed = false;

            var definition = element.GetDefinition(referential);
            if (definition == null)
                context.LogError($"failed to resolve definition '{element.Type}'", CodeErrorEnum.BadConfiguration.ToString(), element);

            else if (string.IsNullOrEmpty(element.ReferenceSource))
                context.LogError($"missing reference in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), element);

            else
            {
                var item = element.GetEntity(referential);
                if (item == null)
                {
                    if (!string.IsNullOrEmpty(element.Type))
                    {
                        element.Type = string.Empty;
                        changed = true;
                    }
                    context.LogError($"Reference '{element.ReferenceSource}' can't be resolved in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), element);
                }

                else
                {
                    if (item is ReferentialEntity e)
                    {
                        if (e.TypeEntity != element.Type)
                        {
                            element.Type = e.TypeEntity;
                            changed = true;
                        }
                        if (e.Name != element.Name)
                        {
                            element.Name = e.Name;
                            changed = true;
                        }
                    }
                }

            }

            return changed;
        }

        private bool EvaluateReference(ValidationContext context, ModelRepository referential, ConceptSubElement element)
        {

            bool changed = false;

            var definition = element.GetDefinition(referential);
            if (definition == null)
                context.LogError($"failed to resolve definition '{element.Type}'", CodeErrorEnum.BadConfiguration.ToString(), element);

            else if (string.IsNullOrEmpty(element.ReferenceSource))
                context.LogError($"missing reference in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), element);

            else
            {
                var item = element.GetEntity(referential);
                if (item == null)
                {
                    if (!string.IsNullOrEmpty(element.Type))
                    {
                        element.Type = string.Empty;
                        changed = true;
                    }
                    context.LogError($"Reference '{element.ReferenceSource}' can't be resolved in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), element);
                }

                else
                {
                    if (item is ReferentialEntity e)
                    {
                        if (e.TypeEntity != element.Type)
                        {
                            element.Type = e.TypeEntity;
                            changed = true;
                        }
                        if (e.Name != element.Name)
                        {
                            element.Name = e.Name;
                            changed = true;
                        }
                    }
                }

            }

            return changed;
        }

        private bool EvaluateReference(ValidationContext context, ModelRepository referential, ModelElement element)
        {

            bool changed = false;

            var definition = element.GetDefinition(referential);
            if (definition == null)
                context.LogError($"failed to resolve definition '{element.Type}'", CodeErrorEnum.BadConfiguration.ToString(), element);

            else if (string.IsNullOrEmpty(element.ReferenceSource))
                context.LogError($"missing reference in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), element);

            else
            {
                var item = element.GetEntity(referential);
                if (item == null)
                {
                    if (!string.IsNullOrEmpty(element.Type))
                    {
                        element.Type = string.Empty;
                        changed = true;
                    }
                    context.LogError($"Reference '{element.ReferenceSource}' can't be resolved in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), element);
                }

                else
                {
                    if (item is ReferentialEntity e)
                    {
                        if (e.TypeEntity != element.Type)
                        {
                            element.Type = e.TypeEntity;
                            changed = true;
                        }
                        if (e.Name != element.Name)
                        {
                            element.Name = e.Name;
                            changed = true;
                        }
                    }
                }

            }

            return changed;
        }

        private bool EvaluateReference(ValidationContext context, ModelRepository referential, SubElement element)
        {

            bool changed = false;

            var definition = element.GetDefinition(referential);
            if (definition == null)
                context.LogError($"failed to resolve definition '{element.Type}'", CodeErrorEnum.BadConfiguration.ToString(), element);

            else if (string.IsNullOrEmpty(element.ReferenceSource))
                context.LogError($"missing reference in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), element);

            else
            {
                var item = element.GetEntity(referential);
                if (item == null)
                {
                    if (!string.IsNullOrEmpty(element.Type))
                    {
                        element.Type = string.Empty;
                        changed = true;
                    }
                    context.LogError($"Reference '{element.ReferenceSource}' can't be resolved in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), element);
                }

                else
                {
                    if (item is ReferentialEntity e)
                    {
                        if (e.TypeEntity != element.Type)
                        {
                            element.Type = e.TypeEntity;
                            changed = true;
                        }
                        if (e.Name != element.Name)
                        {
                            element.Name = e.Name;
                            changed = true;
                        }
                    }
                }

            }

            return changed;
        }
    
    }


}
