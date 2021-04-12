using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;


namespace Bb.ApplicationCooperationViewPoint.CustomCode.UITypeEditors
{


    // [System.ComponentModel.Editor(typeof(ListeTypeReturnGridEditor),typeof(System.Drawing.Design.UITypeEditor))]
    public class ListeTypeReturnGridEditor : ListPropertyGridEditorBase
    {

        protected override List<Type> Liste(HashSet<String> namespaces)
        {

            //List<Type> l = ContainerTypes.GetFromPrimitive().ToList();
            //l.AddRange(ContainerTypes.GetFromReference(Store, namespaces, ContainerTypes.IsTypeReturn).ToList());

            //foreach (var item in ContainerTypes.GetFromSourceCsharp(Store, namespaces, ContainerTypes.IsTypeReturn))
            //    l.Add(item);

            //List<Type> l2 = new List<Type>();
            //foreach (var item in l.ToLookup(c => c.Name))
            //    l2.Add(item.Last());

            //l2 = l2.OrderBy(c => c.Name).ToList();

            //return l2;

            return null;

        }


    }


    public abstract class ListPropertyGridEditorBase : UITypeEditor
    {

        protected object TypeReturn;


        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {

                //Store = (context.Instance as AttributeProperty).Store;

                //TypeReturn = (context.Instance as AttributeProperty).Type;

                //HashSet<string> namespaces = (context.Instance as AttributeProperty).ConfigurationElement.Namespaces;
                //ListBox control = new ListBox();

                //var io = Liste(namespaces);
                //if (io == null)
                //    return value;

                //List<Type> l = new List<Type>();
                //l.Add(ContainerTypes.GetNoneType());
                //l.AddRange(io);

                //control.DataSource = l;
                //control.DisplayMember = "Name";

                //if (!String.IsNullOrEmpty((string)value))
                //    foreach (Type item in l)
                //        if (item.FullName == value)
                //        {
                //            control.SelectedItem = item;
                //            break;
                //        }

                //Close = () => edSvc.CloseDropDown();
                //control.Click += control_Click;
                //edSvc.DropDownControl(control);
                //control.Click -= control_Click;



                //value = string.Empty;

                //try
                //{
                //    value = (control.SelectedItem as Type).FullName;

                //}
                //catch (Exception)
                //{


                //}


            }

            return value;
        }

        //void control_Click(object sender, EventArgs e)
        //{

        //    if (sender != null)
        //        if ((sender as ListBox).SelectedItem != null)
        //            Close();

        //}


        //public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        //{
        //    return true;
        //}

        //public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        //{
        //    return UITypeEditorEditStyle.DropDown;
        //}

        protected DslModeling.Store Store;
        protected Action Close;
        protected abstract List<Type> Liste(HashSet<String> namespaces);




    }

}
