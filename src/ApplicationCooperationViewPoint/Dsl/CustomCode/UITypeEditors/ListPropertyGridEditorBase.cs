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

                ListBox control = new ListBox();

                var io = List();
                if (io == null)
                    return value;

                control.DataSource = io;
                control.DisplayMember = this.DisplayMember;
                control.ValueMember = this.ValueMember;

                if (!String.IsNullOrEmpty((string)value))
                    foreach (object item in io)
                        if (Evaluate(item, v))
                        {
                            control.SelectedItem = item;
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
                catch (Exception)
                {


                }


            }

            return value;
        }

        protected abstract string GetValue(object selectedItem);


        private Store ResolveStore(object instance)
        {

            if (instance is PresentationElement e)
                return e.Store;

            if (instance is CooperationViewPointDiagram d)
                return d.Store;

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


        protected Action Close;
        protected abstract List<object> List();

    }


}
