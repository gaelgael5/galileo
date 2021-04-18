using Bb.Galileo.Files.Schemas;
using Newtonsoft.Json;

namespace Bb.Galileo.Files.Viewpoints
{

    public class CooperationRelationship : CooperationBase
    {

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
                        Text = $"Entity definition {parentName} can't be resolved"
                    });

                }
            }
            else
            {
                models.Diagnostic.Append(new DiagnositcMessage()
                {
                    File = file.FullPath,
                    Severity = SeverityEnum.Error,
                    Text = $"relationship definition {this.Name} can't be resolved"
                });
            }

            return new ViewpointModelItem()
            {
                Kind = ViewpointItem.Relation,
                Relationship = relationship,
                Definition = definition
            };

        }


    }

}
