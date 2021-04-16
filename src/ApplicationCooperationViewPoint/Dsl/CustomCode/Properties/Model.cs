using Bb.Galileo;
using devtm.AutoMapper;
using Microsoft.VisualStudio.Shell;
using System.IO;
using DslModeling = global::Microsoft.VisualStudio.Modeling;

namespace Bb.ApplicationCooperationViewPoint
{

    /// <summary>
    /// DomainClass Model
    /// The root in which all other elements are embedded. Appears as a diagram.
    /// </summary>

    public partial class Model 
    {

        internal void CreateMapper()
        {

            var referential = ReferentialResolver.Instance.GetReferential(this.Store);

            var viewpointConfig = referential.GetCooperationViewpoint(this.ViewpointType);


            using (var form = new Bb.Galileo.Viewpoints.Cooperations.SelectReferential())
            {

                form.SetViewpoint(viewpointConfig);

                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                }

            }
          

        }

    }

    public class Diag : IDiagnostic
    {
        public void Append(DiagnositcMessage message)
        {

        }

    }


}
