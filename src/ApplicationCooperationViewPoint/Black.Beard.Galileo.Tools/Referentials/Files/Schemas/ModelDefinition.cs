using Bb.Galileo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bb.Galileo.Files.Schemas

{
    public class ModelDefinition : IBase, IEvaluate
    {

        public ModelDefinition()
        {
            Restrictions = new List<string>();
        }

        [Description("functional key")]
        [JsonRequired]
        public string Name { get; set; }

        [Description("Property's label for display in the propertygrid")]
        public string Label { get; set; }

        [Description("Description of the current item")]
        public string Description { get; set; }

        [JsonIgnore]
        public FileModel File { get; internal set; }

        public List<string> Restrictions { get; set; }

        public virtual void Evaluate()
        {

            File.Parent.Models.EvaluateRestrictions(this, this.File, Restrictions);

        }


        public ResolveQuery GetReference() => new ResolveQuery(this);

        public override string ToString()
        {
            return Name.ToString();
        }

    }

}
