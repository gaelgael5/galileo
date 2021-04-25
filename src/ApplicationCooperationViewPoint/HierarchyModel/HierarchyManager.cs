using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell.Interop;

namespace devtm.Documentation.HierarchyModel
{

    public class HierarchyManager
    {
        // Fields
        private static HierarchyManager _current;
        private Dictionary<HierarchyItemPair, HierarchyItemIdentity> _hierarchyIdentities = new Dictionary<HierarchyItemPair, HierarchyItemIdentity>();
        private Dictionary<HierarchyIdentity, WeakReference> _weakHierarchies = new Dictionary<HierarchyIdentity, WeakReference>();
        private Dictionary<HierarchyItemIdentity, WeakReference> _weakItems = new Dictionary<HierarchyItemIdentity, WeakReference>();

        // Methods
        internal void AddHierarchyItemIdentity(HierarchyItemIdentity identity)
        {
            this._hierarchyIdentities[identity.HierarchyInfo] = identity;
            if (identity.IsNestedItem)
            {
                this._hierarchyIdentities[identity.NestedInfo] = identity;
            }
        }

        public void Clear()
        {
            this._weakHierarchies.Clear();
            this._weakItems.Clear();
            this._hierarchyIdentities.Clear();
        }

        //public void ClearHierarchyEvents(IVsHierarchy hierarchy)
        //{
        //    HierarchyIdentity identity = new HierarchyIdentity(hierarchy);
        //    HierarchyEventSink sink = this.TryGetHierarchyEventSink(identity);
        //    if (sink != null)
        //    {
        //        sink.Dispose();
        //        this._weakHierarchies.Remove(identity);
        //    }
        //}

        public void ClearItem(HierarchyItemIdentity identity)
        {
            this._weakItems.Remove(identity);
            this._hierarchyIdentities.Remove(identity.HierarchyInfo);
            if (identity.IsNestedItem)
            {
                this._hierarchyIdentities.Remove(identity.NestedInfo);
            }
        }

        //public HierarchyEventSink GetHierarchyEvents(IVsHierarchy vsHierarchy)
        //{
        //    HierarchyEventSink target = this.TryGetHierarchyEventSink(new HierarchyIdentity(vsHierarchy));
        //    if (target == null)
        //    {
        //        target = new HierarchyEventSink(this, vsHierarchy);
        //        this._weakHierarchies.Add(new HierarchyIdentity(vsHierarchy), new WeakReference(target));
        //    }
        //    return target;
        //}

        //public HierarchyListItem GetHierarchyItem(HierarchyItemIdentity identity)
        //{
        //    HierarchyListItem target = this.TryGetHierarchyItem(identity);
        //    if (target == null)
        //    {
        //        target = new HierarchyListItem(this, identity);
        //        this.GetHierarchyEvents(identity.Hierarchy);
        //        if (identity.IsNestedItem)
        //        {
        //            this.GetHierarchyEvents(identity.NestedHierarchy);
        //        }
        //        this._weakItems.Add(identity, new WeakReference(target));
        //    }
        //    return target;
        //}

        //public HierarchyListItem GetHierarchyItem(IVsHierarchy vsHierarchy, uint itemid)
        //{
        //    return this.GetHierarchyItem(HierarchyItemIdentity.Create(vsHierarchy, itemid));
        //}

        //private void RemoveIdentity(HierarchyItemIdentity identity)
        //{
        //    this._weakItems.Remove(identity);
        //    this._hierarchyIdentities.Remove(identity.HierarchyInfo);
        //    if (identity.IsNestedItem)
        //    {
        //        this._hierarchyIdentities.Remove(identity.NestedInfo);
        //    }
        //}

        //private HierarchyEventSink TryGetHierarchyEventSink(HierarchyIdentity identity)
        //{
        //    WeakReference reference;
        //    if (this._weakHierarchies.TryGetValue(identity, out reference))
        //    {
        //        if (reference.IsAlive)
        //        {
        //            return (reference.Target as HierarchyEventSink);
        //        }
        //        this._weakHierarchies.Remove(identity);
        //    }
        //    return null;
        //}

        //public HierarchyListItem TryGetHierarchyItem(HierarchyItemIdentity identity)
        //{
        //    WeakReference reference;
        //    if (this._weakItems.TryGetValue(identity, out reference))
        //    {
        //        if (reference.IsAlive)
        //        {
        //            return (reference.Target as HierarchyListItem);
        //        }
        //        this._weakItems.Remove(identity);
        //    }
        //    return null;
        //}

        //public HierarchyListItem TryGetHierarchyItem(IVsHierarchy hierarchy, uint itemid)
        //{
        //    return this.TryGetHierarchyItem(HierarchyItemIdentity.Create(hierarchy, itemid));
        //}

        public HierarchyItemIdentity TryGetHierarchyItemIdentity(IVsHierarchy hierarchy, uint itemid)
        {
            HierarchyItemIdentity identity;
            HierarchyItemPair key = new HierarchyItemPair(hierarchy, itemid);
            this._hierarchyIdentities.TryGetValue(key, out identity);
            return identity;
        }

        // Properties
        public static HierarchyManager Current
        {
            get
            {
                return (_current = _current ?? new HierarchyManager());
            }
        }

        //public IEnumerable<HierarchyListItem> Items
        //{
        //    get
        //    {
        //        return (from item in this._weakItems.Values
        //                where item.IsAlive
        //                select (HierarchyListItem)item.Target);
        //    }
        //}

    }



}
