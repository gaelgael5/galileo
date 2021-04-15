using Bb.Galileo;
using devtm.AutoMapper;
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

            var srv = this.Store.GetService(typeof(ReferentialResolver));

            var projectDte = this.Store.GetProjectForStore();
            var project = new VisualStudio.ParsingSolution.Project(projectDte);
            var directoryPath = new FileInfo(project.FullName);
            var models = new Bb.Galileo.Files.ModelRepository(directoryPath.Directory.FullName, new Diag());

            using (var form = new Bb.Galileo.Viewpoints.Cooperations.SelectReferential())
            {

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
