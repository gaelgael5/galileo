using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;
using Bb.Galileo.Files;
using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Files.Datas;
using System.Linq;
using System;
using Microsoft.VisualStudio.Modeling.Diagrams;

namespace Bb.ApplicationCooperationViewPoint
{
    public partial class ModelElement
    {

        internal ResolveQuery GetQuery()
        {
            return new ResolveQuery(this.ReferenceSource);
        }

        internal EntityDefinition GetDefinition(ModelRepository rep)
        {
            var item = new ResolveQuery(this.ReferenceSource);
            item.Kind = Galileo.ElementEnum.EntityDefinition;
            var items = item.GetReferentials(rep);
            return items.OfType<EntityDefinition>().FirstOrDefault();
        }

        internal ReferentialEntity GetEntity(ModelRepository rep)
        {
            var item = new ResolveQuery(this.ReferenceSource);
            var items = item.GetReferentials(rep);
            return items.OfType<ReferentialEntity>().FirstOrDefault();
        }

    }
}