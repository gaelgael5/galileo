using DslShell = global::Microsoft.VisualStudio.Modeling.Shell;
using VSShell = global::Microsoft.VisualStudio.Shell;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;
using DslValidation = global::Microsoft.VisualStudio.Modeling.Validation;

namespace Bb.ApplicationCooperationViewPoint
{

    /// <summary>
    /// Double-derived class to allow easier code customization.
    /// </summary>
    internal partial class ApplicationCooperationViewPointCommandSet 
	{

        protected override System.Collections.Generic.IList<System.ComponentModel.Design.MenuCommand> GetMenuCommands()
        {

            // Get the standard commands
            global::System.Collections.Generic.IList<global::System.ComponentModel.Design.MenuCommand> commands = base.GetMenuCommands();


            global::System.ComponentModel.Design.MenuCommand menuCommand;

            // Add command handler for the "view explorer" command in the top-level menu.
            // We use a ContextBoundMenuCommand because the visibility of this command is
            // based on whether or not the command context of our DSL editor is active. 

            menuCommand = new DslShell::DynamicStatusMenuCommand(
              new global::System.EventHandler(GenerateFromStatus),
              new global::System.EventHandler(GenerateFrom),
               Constants.CreateRootMap);
            commands.Add(menuCommand);
            
            //global::System.ComponentModel.Design.MenuCommand menuCommand2
            //= new DslShell::DynamicStatusMenuCommand(
            //    new global::System.EventHandler(ShowDetailsStatus),
            //    new global::System.EventHandler(ShowDetails),
            //    Constants.ShowDetails);
            //commands.Add(menuCommand2);


            return commands;

        }


        /// <summary>
        /// Handler for validating the model.
        /// </summary>
        internal virtual void GenerateFromStatus(object sender, System.EventArgs e)
        {
            
            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)
                this.CurrentApplicationCooperationViewPointDocData.ValidationController.Validate(this.CurrentApplicationCooperationViewPointDocData.GetAllElementsForValidation(), DslValidation::ValidationCategories.Menu);

        }

        internal virtual void GenerateFrom(object sender, System.EventArgs e)
        {


            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)

                foreach (object selectedObject in this.CurrentSelection)
                {
                    // Build list of elements embedded beneath the selected root.
                    DslModeling::ModelElement element = GetValidationTarget1(selectedObject);
                    if (element != null && element is Model)
                    {
                        (element as Model).CreateMapper();
                        break;
                    }
                }
        }

        /// <summary>
		/// Helper method to retrieve the target root element for validation from the selected object.
		/// </summary>
		protected static DslModeling::ModelElement GetValidationTarget1(object selectedObject)
        {
            DslModeling::ModelElement element = null;
            if (selectedObject is DslDiagrams::PresentationElement presentation)
            {
                if (presentation.Subject != null)
                {
                    element = presentation.Subject;
                }
            }
            else
            {
                element = selectedObject as DslModeling::ModelElement;
            }
            return element;
        }

    }

}

