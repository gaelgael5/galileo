﻿using Bb.Galileo.Files;
using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Files.Viewpoints;
using Bb.Galileo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bb.ApplicationCooperationViewPoint
{
    public class TreeviewCooperationViewpointEditor : ListPropertyGridEditorBase
    {

        public TreeviewCooperationViewpointEditor()
        {
            this.ValueMember = "Key";
            this.DisplayMember = "Name";
        }

        protected override List<object> List()
        {

            var configViewpoint = Referential.GetCooperationViewpoint(this.Diagram.ViewpointType);

            if (configViewpoint != null)
            {

                var item1 = this.ModelElement;

                var result = new List<ProjectionItem>(300);

                if (item1 is Bb.ApplicationCooperationViewPoint.ModelElement me)
                {
                    foreach (var e in configViewpoint.Elements)
                    {
                        var query = new ResolveQuery() { Kind = Galileo.ElementEnum.Entity, TypeName = e.Name };
                        var items = query.GetReferentials(this.Referential);
                        result.AddRange(GetReferentials(items));
                    }
                }
                else if (item1 is Concept co)
                {
                    foreach (var e in configViewpoint.Concepts)
                    {
                        var query = new ResolveQuery() { Kind = Galileo.ElementEnum.Entity, TypeName = e.Name };
                        var items = query.GetReferentials(this.Referential);
                        result.AddRange(GetReferentials(items));
                    }
                }
                else if (item1 is SubElement se)
                {
                    if (!string.IsNullOrEmpty(se.Parent.Type))
                    {
                        foreach (var element in configViewpoint.Elements.Where(c => c.Name == se.Parent.Type))
                        {
                            foreach (CooperationRelationship sElement in element.Children)
                            {
                                var itemDef = sElement.GetRelationshipDefinition(this.Referential);
                                var parentQuery = se.Parent.ReferenceSource.AsQuery();
                                var p = parentQuery.GetReferentials(this.Referential).OfType<ReferentialEntity>().FirstOrDefault();
                                var children = p.GetTargetEntities(itemDef).ToList();
                                result.AddRange(GetReferentials(children));
                            }
                        }
                    }
                }
                else if (item1 is ConceptElement ce)
                {
                    if (!string.IsNullOrEmpty(ce.Parent.Type))
                    {
                        foreach (var element in configViewpoint.Elements.Where(c => c.Name == ce.Parent.Type))
                        {
                            foreach (CooperationRelationship sElement in element.Children)
                            {
                                var itemDef = sElement.GetRelationshipDefinition(this.Referential);
                                var parentQuery = ce.Parent.ReferenceSource.AsQuery();
                                var p = parentQuery.GetReferentials(this.Referential).OfType<ReferentialEntity>().FirstOrDefault();
                                var children = p.GetTargetEntities(itemDef).ToList();
                                result.AddRange(GetReferentials(children));
                            }
                        }
                    }
                }
                else if (item1 is ConceptSubElement cs)
                {

                }
                else if (item1 is Relationship re)
                {

                }

                return result.OrderBy(c => c.Name).Cast<object>().ToList();

            }

            throw new System.NotImplementedException();
        }



        protected override bool Evaluate(object value1, string value2)
        {
            if (value1 is ProjectionItem v)
                return object.Equals(v.Key, value2);
            return false;
        }

        protected override string GetValue(object selectedItem)
        {

            if (selectedItem is ProjectionItem v)
                return v.Key;

            return string.Empty;
        }

        private class ProjectionItem
        {

            public string Key { get; set; }

            public string Name { get; set; }

            public object Tag { get; set; }

        }


        private IEnumerable<ProjectionItem> GetReferentials(IEnumerable<IBase> items)
        {
            foreach (ReferentialEntity entity in items)
                yield return new ProjectionItem()
                {
                    Key = entity.GetReference().ToString(),
                    Name = entity.Name,
                    Tag = entity,
                };
        }


    }


}
