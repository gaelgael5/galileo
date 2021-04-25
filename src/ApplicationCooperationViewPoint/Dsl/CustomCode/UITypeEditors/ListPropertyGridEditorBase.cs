using Bb.Galileo.Files;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using DslModeling = global::Microsoft.VisualStudio.Modeling;


namespace Bb.ApplicationCooperationViewPoint
{



    public abstract class ListPropertyGridEditorBase : UITypeEditor
    {


        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {

                var v = (string)value;

                Store = ResolveStore(context.Instance);
                this.Referential = ReferentialResolver.Instance.GetReferential(Store);
                if (this.Referential == null)
                    throw new Exception("referential can't be loded");

                ListBox control = new ListBox();

                var io = List();
                if (io == null)
                    return value;

                control.DataSource = io;
                control.DisplayMember = this.DisplayMember;
                control.ValueMember = this.ValueMember;
                control.SelectionMode = SelectionMode.One;

                if (!String.IsNullOrEmpty((string)value))
                    foreach (object item in io)
                        if (Evaluate(item, v))
                        {
                            control.Select();
                            control.SelectedItem = item;
                            control.SelectedValue = v;
                            break;
                        }

                Close = () => edSvc.CloseDropDown();
                control.Click += control_Click;
                edSvc.DropDownControl(control);
                control.Click -= control_Click;



                value = string.Empty;

                try
                {

                    value = GetValue(control.SelectedItem);
                }
                catch (Exception e)
                {


                }


            }

            return value;
        }

        protected abstract string GetValue(object selectedItem);


        private Store ResolveStore(object instance)
        {

            if (instance is PresentationElement e)
            {
                this.ModelElement = e.ModelElement;
                this.Diagram = this.ModelElement.GetDiagram();
                return e.Store;

            }

            if (instance is CooperationViewPointDiagram d)
            {
                this.ModelElement = d.ModelElement;
                this.Diagram = this.ModelElement.GetDiagram();
                return d.Store;
            }

            throw new NotImplementedException();

        }


        protected abstract bool Evaluate(object value1, string value2);


        void control_Click(object sender, EventArgs e)
        {

            if (sender != null)
                if ((sender as ListBox).SelectedItem != null)
                    Close();

        }


        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        protected DslModeling.Store Store;

        protected ModelRepository Referential { get; private set; }

        public string DisplayMember { get; protected set; }

        public string ValueMember { get; protected set; }
        public DslModeling.ModelElement ModelElement { get; private set; }
        public Model Diagram { get; private set; }

        protected Action Close;
        protected abstract List<object> List();

    }


}
