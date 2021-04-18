using Bb.Galileo.Viewpoints.Cooperations;
using devtm.AutoMapper;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            //var keys = _memory ?? new HashSet<string>();
            var referential = ReferentialResolver.Instance.GetReferential(this.Store);

            var viewpointConfig = referential.GetCooperationViewpoint(this.ViewpointType);


            using (var form = new Bb.Galileo.Viewpoints.Cooperations.SelectReferential(referential))
            {

                form.SetViewpoint(viewpointConfig);
                //form.SetSelectedKeys(keys);

                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    //Dictionary<string, CooperationCheckedItem> k2 = form.GetSelectedKeys();
                    //CreateItem(k2);

                }

            }


        }

        private void CreateItem(Dictionary<string, CooperationCheckedItem> k2)
        {

            Dictionary<string, DslModeling::ModelElement> _items = new Dictionary<string, DslModeling.ModelElement>();
            using (Transaction t = this.Store.TransactionManager.BeginTransaction("automated model"))
            {
                foreach (var item in k2.Values.OrderBy(c => c.Level))
                {
                    if (item.Level == 0)
                    {
                        if (!_items.ContainsKey(item.Item.Name))
                        {
                            Concept c = new Concept(this.Partition)
                            {
                                ReferenceSource = item.Item.GetReference().ToString(),
                                Name = item.Item.Label,
                            };
                            this.Concept.Add(c);
                        }
                    }
                }

                t.Commit();

            }

        }

    }


}
