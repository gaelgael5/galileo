using Microsoft.VisualStudio.Modeling.Validation;
using System.Linq;
using System;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using Bb.Galileo.Files;
using System.Diagnostics;

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

            foreach (var concept in this.Concepts)
            {

                if (string.IsNullOrEmpty(concept.Type))
                    context.LogError($"failed to resolve definition of '{concept.ReferenceSource}' in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), concept);

                var configConcept = viewpointConfig.Concepts.FirstOrDefault(c => c.Name == concept.Type);
                if (configConcept == null)
                    context.LogError($"failed to resolve '{concept.Name}' in the configuration viewpoint '{this.ViewpointType}'", CodeErrorEnum.BadConfiguration.ToString(), concept);
                else
                    ValidateConcept(concept, configConcept, context, referential);
            }

            foreach (var modelElement in this.Elements)
            {

                if (string.IsNullOrEmpty(modelElement.Type))
                    context.LogError($"failed to resolve definition of '{modelElement.ReferenceSource}' in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), modelElement);
                
                else
                {
                    var configModelElement = viewpointConfig.Elements.FirstOrDefault(c => c.Name == modelElement.Type);
                    if (configModelElement == null)
                        context.LogError($"failed to resolve '{modelElement.Name}' in the configuration viewpoint '{this.ViewpointType}'", CodeErrorEnum.BadConfiguration.ToString(), modelElement);
                    else
                        ValidateModelElement(modelElement, configModelElement, context, referential);
                }
            }

        }

        private void ValidateModelElement(ModelElement modelElement, Galileo.Files.Viewpoints.CooperationRootElement configModelElement, ValidationContext context, Galileo.Files.ModelRepository referential)
        {

            var definition = modelElement.GetDefinition(referential);
            if (definition == null)
                context.LogError($"failed to resolve definition '{modelElement.Type}'", CodeErrorEnum.BadConfiguration.ToString(), modelElement);

            if (string.IsNullOrEmpty(modelElement.ReferenceSource))
                context.LogError($"missing entity reference '{modelElement.ReferenceSource}'", CodeErrorEnum.MissingConfiguration.ToString(), modelElement);

            else
            {
                var entity = modelElement.GetEntity(referential);
                if (entity == null)
                    context.LogError($"failed to resolve entity '{modelElement.ReferenceSource}'", CodeErrorEnum.BadConfiguration.ToString(), modelElement);
            }

            foreach (var subElement in modelElement.Children)
            {

                if (string.IsNullOrEmpty(subElement.Type))
                    context.LogError($"failed to resolve definition of '{modelElement.ReferenceSource}' in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), modelElement);

                else
                {

                    var configConceptSubElement = modelElement.Children.FirstOrDefault(c => c.Type == subElement.Type);
                    if (configConceptSubElement == null)
                        context.LogError($"failed to resolve '{subElement.Name}' in the configuration viewpoint '{this.ViewpointType}'", CodeErrorEnum.BadConfiguration.ToString(), subElement);

                    else
                    {

                        definition = subElement.GetDefinition(referential);
                        if (definition == null)
                            context.LogError($"failed to resolve definition '{subElement.Type}'", CodeErrorEnum.BadConfiguration.ToString(), subElement);

                        if (string.IsNullOrEmpty(subElement.ReferenceSource))
                            context.LogError($"missing entity reference '{subElement.ReferenceSource}'", CodeErrorEnum.MissingConfiguration.ToString(), subElement);

                        else
                        {
                            var entity = subElement.GetEntity(referential);
                            if (entity == null)
                                context.LogError($"failed to resolve entity '{subElement.ReferenceSource}'", CodeErrorEnum.BadConfiguration.ToString(), subElement);
                        }

                    }
                }

            }
        }

        private void ValidateConcept(Concept concept, Galileo.Files.Viewpoints.CooperationConcept configConcept, ValidationContext context, Galileo.Files.ModelRepository referential)
        {

            var definition = concept.GetDefinition(referential);
            if (definition == null)
                context.LogError($"failed to resolve definition '{concept.GetQuery().TypeName}'", CodeErrorEnum.BadConfiguration.ToString(), concept);

            if (string.IsNullOrEmpty(concept.ReferenceSource))
                context.LogError($"missing entity reference '{concept.ReferenceSource}'", CodeErrorEnum.MissingConfiguration.ToString(), concept);

            else
            {
                var entity = concept.GetEntity(referential);
                if (entity == null)
                    context.LogError($"failed to resolve entity '{concept.ReferenceSource}'", CodeErrorEnum.BadConfiguration.ToString(), concept);
            }

            foreach (var conceptElement in concept.Children)
            {

                if (string.IsNullOrEmpty(conceptElement.Type))
                    context.LogError($"failed to resolve definition of '{conceptElement.ReferenceSource}' in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), conceptElement);

                else
                {

                    var configConceptElement = conceptElement.Children.FirstOrDefault(c => c.Type == conceptElement.Type);
                    if (configConceptElement == null)
                        context.LogError($"failed to resolve '{concept.Name}' in the configuration viewpoint '{this.ViewpointType}'", CodeErrorEnum.BadConfiguration.ToString(), conceptElement);

                    else
                        ValidateConceptElement(conceptElement, configConceptElement, context, referential);

                }
            }
        }

        private void ValidateConceptElement(ConceptElement conceptElement, ConceptSubElement configConceptElement, ValidationContext context, Galileo.Files.ModelRepository referential)
        {

            var definition = conceptElement.GetDefinition(referential);
            if (definition == null)
                context.LogError($"failed to resolve definition '{conceptElement.Type}'", CodeErrorEnum.BadConfiguration.ToString(), conceptElement);

            if (string.IsNullOrEmpty(conceptElement.ReferenceSource))
                context.LogError($"missing entity reference '{conceptElement.ReferenceSource}'", CodeErrorEnum.MissingConfiguration.ToString(), conceptElement);

            else
            {
                var entity = conceptElement.GetEntity(referential);
                if (entity == null)
                    context.LogError($"failed to resolve entity '{conceptElement.ReferenceSource}'", CodeErrorEnum.BadConfiguration.ToString(), conceptElement);
            }

            foreach (var conceptSubElement in conceptElement.Children)
            {

                if (string.IsNullOrEmpty(conceptSubElement.Type))
                    context.LogError($"failed to resolve definition of '{conceptSubElement.ReferenceSource}' in the configuration viewpoint '{this.Name}'", CodeErrorEnum.BadConfiguration.ToString(), conceptSubElement);

                else
                {

                    var configConceptSubElement = conceptElement.Children.FirstOrDefault(c => c.Type == conceptSubElement.Type);
                    if (configConceptSubElement == null)
                        context.LogError($"failed to resolve '{configConceptSubElement.Name}' in the configuration viewpoint '{this.ViewpointType}'", CodeErrorEnum.BadConfiguration.ToString(), conceptSubElement);

                    else
                    {

                        definition = conceptSubElement.GetDefinition(referential);
                        if (definition == null)
                            context.LogError($"failed to resolve definition '{conceptSubElement.Type}'", CodeErrorEnum.BadConfiguration.ToString(), conceptSubElement);

                        if (string.IsNullOrEmpty(conceptSubElement.ReferenceSource))
                            context.LogError($"missing entity reference '{conceptSubElement.ReferenceSource}'", CodeErrorEnum.MissingConfiguration.ToString(), conceptSubElement);

                        else
                        {
                            var entity = conceptSubElement.GetEntity(referential);
                            if (entity == null)
                                context.LogError($"failed to resolve entity '{conceptSubElement.ReferenceSource}'", CodeErrorEnum.BadConfiguration.ToString(), conceptSubElement);
                        }

                    }

                }

            }
        }
    }


}
