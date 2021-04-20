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


            if (string.IsNullOrEmpty(this.ViewpointType))
                throw new Exception("Please select a viewpoint type in the propertygrid");

            var referential = ReferentialResolver.Instance.GetReferential(this.Store);

            var viewpointConfig = referential.GetCooperationViewpoint(this.ViewpointType);

            if (viewpointConfig == null)
                throw new Exception($"the viewpoint type '{this.ViewpointType}' can't be found. please select a valid viewpoint type");



            using (var form = new Bb.Galileo.Viewpoints.Cooperations.SelectReferential(referential))
            {

                form.SetViewpoint(viewpointConfig);
                //form.SetSelectedKeys(keys);

                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    ViewpointProjectionEntities k2 = form.GetSelectedKeys();
                    CreateItem(k2);

                }

            }


        }

        private void CreateItem(ViewpointProjectionEntities k2)
        {

            Dictionary<string, DslModeling::ModelElement> _items = new Dictionary<string, DslModeling.ModelElement>();
            using (Transaction t = this.Store.TransactionManager.BeginTransaction("automated model"))
            {
                foreach (var item in k2.Entities)
                {

                    var k = item.Entity.GetReference().ToString();

                    if (!_items.ContainsKey(k))
                    {

                        if (item.Kind == Galileo.Files.Viewpoints.ViewpointItem.Concept)
                        {
                            Concept c = new Concept(this.Partition)
                            {
                                ReferenceSource = k,
                                Name = item.Entity.Label ?? item.Entity.Name,
                            };
                            this.Concept.Add(c);

                            foreach (var item2 in item.Children)
                            {
                                var l = item2.Entity.GetReference().ToString();
                                ConceptElement s = new ConceptElement(this.Partition)
                                {
                                    ReferenceSource = l,
                                    Name = item2.Entity.Label ?? item2.Entity.Name,
                                };
                                c.ConceptElement.Add(s);

                                foreach (var item3 in item2.Children)
                                {
                                    var m = item3.Entity.GetReference().ToString();
                                    ConceptSubElement s2 = new ConceptSubElement(this.Partition)
                                    {
                                        ReferenceSource = l,
                                        Name = item2.Entity.Label ?? item2.Entity.Name,
                                    };
                                    s.ConceptSubElement.Add(s2);
                                }

                            }

                        }
                        else
                        {
                            ModelElement c = new ModelElement(this.Partition)
                            {
                                ReferenceSource = k,
                                Name = item.Entity.Label ?? item.Entity.Name,
                            };
                            this.Elements.Add(c);

                            foreach (var item2 in item.Children)
                            {
                                var l = item2.Entity.GetReference().ToString();
                                SubElement s = new SubElement(this.Partition)
                                {
                                    ReferenceSource = l,
                                    Name = item2.Entity.Label ?? item2.Entity.Name,
                                };
                                c.Parent.Add(s);
                            }


                        }

                    }
                }

                t.Commit();

            }

        }

    }


}
