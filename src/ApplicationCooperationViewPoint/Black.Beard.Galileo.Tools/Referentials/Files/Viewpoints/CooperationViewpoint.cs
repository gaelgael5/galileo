using Bb.Galileo.Files.Schemas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Galileo.Files.Viewpoints
{

    public class CooperationViewpoint : CooperationBase
    {

        public CooperationViewpoint()
        {
            Concepts = new List<CooperationConcept>();
            Elements = new List<CooperationRootElement>();
            References = new List<CooperationRelationship>();
        }


        public List<CooperationConcept> Concepts { get; set; }

        public List<CooperationRootElement> Elements { get; set; }

        public List<CooperationRelationship> References { get; set; }

        [JsonIgnore]
        public FileModel File { get; internal set; }

        public ViewpointModel GetViewpointModel()
        {

            var models = File.Parent.Models;

            var result = new ViewpointModel()
            {
                Type = typeof(CooperationViewpoint).Name,
                Name = this.Name,
            };

            foreach (var item in this.Concepts)
                result.AddChildren(item.GetViewpointItem(this.File));

            foreach (var item in this.Elements)
                result.AddChildren(item.GetViewpointItem(this.File));

            foreach (var item in this.References)
            {

                RelationshipDefinition rel = models.GetRelationshipDefinition(item.Name);
                if (rel == null)
                {
                    models.Diagnostic.Append(new DiagnositcMessage()
                    {
                        File = File.FullPath,
                        Severity = SeverityEnum.Error,
                        Text = $"Entity definition {item.Name} can't be resolved"
                    });
                }
                else
                {
                    foreach (var r in result.Children)
                        r.SetReference(this.File, rel);
                }

            }

            return result;

        }

    }

}
