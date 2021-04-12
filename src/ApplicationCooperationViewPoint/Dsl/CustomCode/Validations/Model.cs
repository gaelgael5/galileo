using Microsoft.VisualStudio.Modeling.Validation;
using DslModeling = global::Microsoft.VisualStudio.Modeling;

namespace Bb.ApplicationCooperationViewPoint
{
    /// <summary>
    /// DomainClass Model
    /// The root in which all other elements are embedded. Appears as a diagram.
    /// </summary>

    [ValidationState(ValidationState.Enabled)]
    public partial class Model
    {

        [ValidationMethod(ValidationCategories.Open | ValidationCategories.Save)]
        private void ValidateAttributeNameAsValidIdentifier(ValidationContext context)
        {



        }


    }


}
