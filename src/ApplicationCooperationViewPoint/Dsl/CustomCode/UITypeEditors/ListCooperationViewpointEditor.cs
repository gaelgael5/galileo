using Bb.Galileo.Files.Viewpoints;
using System.Collections.Generic;

namespace Bb.ApplicationCooperationViewPoint
{


    // [System.ComponentModel.Editor(typeof(ListeCooperationViewpointEditor),typeof(System.Drawing.Design.UITypeEditor))]
    public class ListCooperationViewpointEditor : ListPropertyGridEditorBase
    {

        public ListCooperationViewpointEditor()
        {
           
        }

        protected override List<ProjectionItem> List()
        {

            List<ProjectionItem> l = new List<ProjectionItem>();

            var items = this.Referential.GetCooperationViewpoints();
            foreach (var item in items)
                l.Add(new ProjectionItem() { Key = item.Name, Name = item.Name, Tag = item });

            return l;

        }
      
    }


}
