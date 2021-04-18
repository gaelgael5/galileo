using Bb.Galileo.Files.Schemas;
using System;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Viewpoints
{

    public class CooperationConcept : CooperationBase
    {

        public CooperationConcept()
        {
            Children = new List<CooperationElement>();
        }

        public List<CooperationElement> Children { get; set; }

        internal ViewpointModelItem GetViewpointItem(FileModel file)
        {

            var result = new ViewpointModelItem()
            {
                Kind = ViewpointItem.Concept,
            };

            var models = file.Parent.Models;
            var definition = models.GetEntityDefinition(this.Name);

            if (definition == null)
            {

                file.Parent.Diagnostic.Append(new DiagnositcMessage()
                {
                    File = file.FullPath,
                    Severity = SeverityEnum.Error,
                    Text = $"Entity definition {this.Name} can't be resolved"
                });

            }
            else
            {
                result.Definition = definition;

                foreach (CooperationElement item in Children)
                    result.AddChildren(item.GetViewpointItem(file, result));

            }

            return result;

        }

    }

    public enum ViewpointItem
    {
        Concept,
        Element,
        Relation,
        ElementRoot,
    }

}

