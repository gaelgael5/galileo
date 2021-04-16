using Bb.Galileo.Files.Schemas;
using System.Collections.Generic;

namespace Bb.Galileo.Files.Viewpoints
{


    public class CooperationElement : CooperationBase
    {

        public CooperationElement()
        {
            Children = new List<CooperationRelationship>();
        }


        public List<CooperationRelationship> Children { get; set; }

        public ViewpointModelItem GetViewpointItem(FileModel file, ViewpointModelItem parent)
        {

            string parentName = parent.Definition.Name;
            ModelRepository models = file.Parent.Models;

            EntityDefinition definition = null;
            var relationship = models.GetRelationshipDefinition(this.Name);
            if (relationship != null)
            {

                if (relationship.Origin.Name == parentName)
                    definition = relationship.GetTargetDefinition();

                else if (relationship.Target.Name == parentName)
                    definition = relationship.GetOriginDefinition();

                else
                {
                    models.Diagnostic.Append(new DiagnositcMessage()
                    {
                        File = file.FullPath,
                        Severity = SeverityEnum.Error,
                        Text = $"Entity definition {parentName} don't match with origin or target entity definition"
                    });

                }
            }
            else
            {
                models.Diagnostic.Append(new DiagnositcMessage()
                {
                    File = file.FullPath,
                    Severity = SeverityEnum.Error,
                    Text = $"relitionship definition {this.Name} can't be resolved"
                });
            }

            var result = new ViewpointModelItem()
            {
                Relationship = relationship,
                Definition = definition
            };

            if (definition != null)
                foreach (var item in this.Children)
                    result.AddChildren(item.GetViewpointItem(file, result));

            return result;

        }





    }


}

