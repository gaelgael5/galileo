using Bb.Galileo.Files.Schemas;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Viewpoints
{
    public class CooperationRootElement : CooperationBase
    {

        public CooperationRootElement()
        {
            Children = new List<CooperationRelationship>();
        }


        public List<CooperationRelationship> Children { get; set; }


        public ViewpointModelItem GetViewpointItem(FileModel file)
        {

            var result = new ViewpointModelItem()
            {
                Kind = ViewpointItem.ElementRoot,
            };

            ModelRepository models = file.Parent.Models;
            var definition = models.GetEntityDefinition(this.Name);

            if (definition == null)
            {
                models.Diagnostic.Append(new DiagnositcMessage()
                {
                    File = file.FullPath,
                    Severity = SeverityEnum.Error,
                    Text = $"Entity definition {this.Name} can't be resolved"
                });
            }
            else
            {
                result.Definition = definition;
                foreach (var item in this.Children)
                    result.AddChildren(item.GetViewpointItem(file, result));
            }

            return result;

        }

    }


}

