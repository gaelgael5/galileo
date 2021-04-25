using DslShell = global::Microsoft.VisualStudio.Modeling.Shell;
using VSShell = global::Microsoft.VisualStudio.Shell;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;
using DslValidation = global::Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.Modeling;

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
               Constants.CreateRootMap1);
            commands.Add(menuCommand);

            menuCommand = new DslShell::DynamicStatusMenuCommand(
              new global::System.EventHandler(GenerateFromStatus),
              new global::System.EventHandler(SelectWithChildren),
               Constants.CreateRootMap2);
            commands.Add(menuCommand);

            menuCommand = new DslShell::DynamicStatusMenuCommand(
                new global::System.EventHandler(GenerateFromStatus),
                new global::System.EventHandler(CenterChildren),
                Constants.CreateRootMap3);
            commands.Add(menuCommand);

            menuCommand = new DslShell::DynamicStatusMenuCommand(
                new global::System.EventHandler(GenerateFromStatus),
                new global::System.EventHandler(CenterItems),
                Constants.AlignToCenter);
            commands.Add(menuCommand);

            menuCommand = new DslShell::DynamicStatusMenuCommand(
                new global::System.EventHandler(GenerateFromStatus),
                new global::System.EventHandler(AlignLeftItems),
                Constants.AlignToLeft);
            commands.Add(menuCommand);

            menuCommand = new DslShell::DynamicStatusMenuCommand(
               new global::System.EventHandler(GenerateFromStatus),
               new global::System.EventHandler(AlignRightItems),
               Constants.AlignToRight);
            commands.Add(menuCommand);

            menuCommand = new DslShell::DynamicStatusMenuCommand(
               new global::System.EventHandler(GenerateFromStatus),
               new global::System.EventHandler(AlignTopItems),
               Constants.AlignToTop);
            commands.Add(menuCommand);

            menuCommand = new DslShell::DynamicStatusMenuCommand(
               new global::System.EventHandler(GenerateFromStatus),
               new global::System.EventHandler(AlignMiddleItems),
               Constants.AlignToMiddle);
            commands.Add(menuCommand);

            menuCommand = new DslShell::DynamicStatusMenuCommand(
               new global::System.EventHandler(GenerateFromStatus),
               new global::System.EventHandler(AlignBottomItems),
               Constants.AlignToBottom);
            commands.Add(menuCommand);

            menuCommand = new DslShell::DynamicStatusMenuCommand(
               new global::System.EventHandler(GenerateFromStatus),
               new global::System.EventHandler(DistributeVerticalItems),
               Constants.DistributeVertical);
            commands.Add(menuCommand);

            menuCommand = new DslShell::DynamicStatusMenuCommand(
               new global::System.EventHandler(GenerateFromStatus),
               new global::System.EventHandler(DistributeHorizontalItems),
               Constants.DistributeHorizontal);
            commands.Add(menuCommand);

            return commands;

        }


        /// <summary>
        /// Handler for validating the model.
        /// </summary>
        internal virtual void GenerateFromStatus(object sender, System.EventArgs e)
        {

            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)
                this.CurrentApplicationCooperationViewPointDocData.ValidationController
                    .Validate(this.CurrentApplicationCooperationViewPointDocData.GetAllElementsForValidation(), DslValidation::ValidationCategories.Menu);

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

        internal virtual void SelectWithChildren(object sender, System.EventArgs e)
        {
            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)
            {
                List<NodeShape> _toSelect = new List<NodeShape>(100);
                foreach (object selectedObject in this.CurrentSelection)
                {

                    _toSelect.Add(selectedObject as NodeShape);

                    // Build list of elements embedded beneath the selected root.
                    DslModeling::ModelElement element = GetValidationTarget1(selectedObject);
                    if (element != null)
                    {

                        if (element is ModelElement me)
                            foreach (var item in me.Children)
                                _toSelect.Add(item.ToShape());

                        else if (element is Concept c)
                            foreach (var item in c.Children)
                                _toSelect.Add(item.ToShape());

                        else if (element is ConceptElement ce)
                            foreach (var item in ce.Children)
                                _toSelect.Add(item.ToShape());
                    }
                }

                if (_toSelect.Count > 1)
                    SelectExtension.Select(_toSelect);

            }
        }

        internal virtual void CenterChildren(object sender, System.EventArgs e)
        {

            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)
            {

                DslModeling::ModelElement element = null;

                List<NodeShape> _toSelect = new List<NodeShape>(100);
                foreach (object selectedObject in this.CurrentSelection)
                {

                    element = GetValidationTarget1(selectedObject);
                    if (element != null)
                    {

                        if (element is ModelElement me)
                            foreach (var item in me.Children)
                                _toSelect.Add(item.ToShape());

                        else if (element is Concept c)
                            foreach (var item in c.Children)
                                _toSelect.Add(item.ToShape());

                        else if (element is ConceptElement ce)
                            foreach (var item in ce.Children)
                                _toSelect.Add(item.ToShape());

                    }

                }

                if (_toSelect.Count > 1)
                {
                    using (Transaction t = element.Store.TransactionManager.BeginTransaction("Replace shapes"))
                    {
                        ShapeExtension.CenterOnItem(element.ToShape(), _toSelect);
                        t.Commit();
                    }
                }

            }

        }

        internal virtual void CenterItems(object sender, System.EventArgs e)
        {

            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)
            {

                List<NodeShape> _toSelect = new List<NodeShape>(100);
                foreach (object selectedObject in this.CurrentSelection)
                    if (selectedObject is NodeShape s)
                        _toSelect.Add(s);

                if (_toSelect.Count > 1)
                {
                    using (Transaction t = _toSelect[0].Store.TransactionManager.BeginTransaction("center shapes"))
                    {
                        ShapeExtension.Center(_toSelect);
                        t.Commit();
                    }
                }
            }

        }

        internal virtual void AlignLeftItems(object sender, System.EventArgs e)
        {

            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)
            {
                List<NodeShape> _toSelect = new List<NodeShape>(100);
                foreach (object selectedObject in this.CurrentSelection)
                    if (selectedObject is NodeShape s)
                        _toSelect.Add(s);

                if (_toSelect.Count > 1)
                {
                    using (Transaction t = _toSelect[0].Store.TransactionManager.BeginTransaction("center shapes"))
                    {
                        ShapeExtension.AlignLeft(_toSelect);
                        t.Commit();
                    }
                }

            }

        }

        internal virtual void AlignRightItems(object sender, System.EventArgs e)
        {

            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)
            {
                List<NodeShape> _toSelect = new List<NodeShape>(100);
                foreach (object selectedObject in this.CurrentSelection)
                    if (selectedObject is NodeShape s)
                        _toSelect.Add(s);

                if (_toSelect.Count > 1)
                {
                    using (Transaction t = _toSelect[0].Store.TransactionManager.BeginTransaction("center shapes"))
                    {
                        ShapeExtension.AlignRight(_toSelect);
                        t.Commit();
                    }
                }

            }

        }

        internal virtual void AlignTopItems(object sender, System.EventArgs e)
        {

            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)
            {
                List<NodeShape> _toSelect = new List<NodeShape>(100);
                foreach (object selectedObject in this.CurrentSelection)
                    if (selectedObject is NodeShape s)
                        _toSelect.Add(s);

                if (_toSelect.Count > 1)
                {
                    using (Transaction t = _toSelect[0].Store.TransactionManager.BeginTransaction("center shapes"))
                    {
                        ShapeExtension.AlignTop(_toSelect);
                        t.Commit();
                    }
                }

            }

        }

        internal virtual void AlignMiddleItems(object sender, System.EventArgs e)
        {

            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)
            {
                List<NodeShape> _toSelect = new List<NodeShape>(100);
                foreach (object selectedObject in this.CurrentSelection)
                    if (selectedObject is NodeShape s)
                        _toSelect.Add(s);

                if (_toSelect.Count > 1)
                {
                    using (Transaction t = _toSelect[0].Store.TransactionManager.BeginTransaction("center shapes"))
                    {
                        ShapeExtension.AlignMiddle(_toSelect);
                        t.Commit();
                    }
                }

            }

        }


        internal virtual void AlignBottomItems(object sender, System.EventArgs e)
        {

            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)
            {
                List<NodeShape> _toSelect = new List<NodeShape>(100);
                foreach (object selectedObject in this.CurrentSelection)
                    if (selectedObject is NodeShape s)
                        _toSelect.Add(s);

                if (_toSelect.Count > 1)
                {
                    using (Transaction t = _toSelect[0].Store.TransactionManager.BeginTransaction("center shapes"))
                    {
                        ShapeExtension.AlignBottom(_toSelect);
                        t.Commit();
                    }
                }

            }

        }

        internal virtual void DistributeHorizontalItems(object sender, System.EventArgs e)
        {

            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)
            {
                List<NodeShape> _toSelect = new List<NodeShape>(100);
                foreach (object selectedObject in this.CurrentSelection)
                    if (selectedObject is NodeShape s)
                        _toSelect.Add(s);

                if (_toSelect.Count > 1)
                {
                    using (Transaction t = _toSelect[0].Store.TransactionManager.BeginTransaction("center shapes"))
                    {
                        ShapeExtension.DistributeHorizontaly(_toSelect);
                        t.Commit();
                    }
                }

            }

        }

        internal virtual void DistributeVerticalItems(object sender, System.EventArgs e)
        {

            if (this.CurrentApplicationCooperationViewPointDocData != null && this.CurrentApplicationCooperationViewPointDocData.Store != null)
            {
                List<NodeShape> _toSelect = new List<NodeShape>(100);
                foreach (object selectedObject in this.CurrentSelection)
                    if (selectedObject is NodeShape s)
                        _toSelect.Add(s);

                if (_toSelect.Count > 1)
                {
                    using (Transaction t = _toSelect[0].Store.TransactionManager.BeginTransaction("center shapes"))
                    {
                        ShapeExtension.DistributeVerticaly(_toSelect);
                        t.Commit();
                    }
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

