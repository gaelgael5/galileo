using Bb.Galileo.Files.Viewpoints;
using System.Collections.Generic;


namespace Bb.ApplicationCooperationViewPoint
{

    // [System.ComponentModel.Editor(typeof(ListeCooperationViewpointEditor),typeof(System.Drawing.Design.UITypeEditor))]
    public class ListeCooperationViewpointEditor : ListPropertyGridEditorBase
    {

        public ListeCooperationViewpointEditor()
        {
            this.ValueMember = "Name";
            this.DisplayMember = "Name";
        }

        protected override List<object> List()
        {

            List<object> l = new List<object>();

            var items = this.Referential.GetCooperationViewpoints();
            foreach (var item in items)
                l.Add(item);

            return l;

        }

        protected override bool Evaluate(object value1, string value2)
        {
            
            if (value1 is CooperationViewpoint v)
                return v.Name == value2;

            return false;

        }

        protected override string GetValue(object selectedItem)
        {
            if (selectedItem is CooperationViewpoint s)
                return s.Name;

            return string.Empty;
        }

    }


}
