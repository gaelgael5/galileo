using Bb.Galileo;
using devtm.AutoMapper;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
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

        private HashSet<string> _memory;

        internal void CreateMapper()
        {

            var keys = _memory ?? new HashSet<string>();
            var referential = ReferentialResolver.Instance.GetReferential(this.Store);

            var viewpointConfig = referential.GetCooperationViewpoint(this.ViewpointType);


            using (var form = new Bb.Galileo.Viewpoints.Cooperations.SelectReferential(referential))
            {

                form.SetViewpoint(viewpointConfig);
                form.SetSelectedKeys(keys);

                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    var k2 = form.GetSelectedKeys();
                    _memory = k2;
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
