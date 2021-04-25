using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell.Interop;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio;

namespace devtm.Documentation.HierarchyModel
{

    public class HierarchyItemIdentity
    {
        // Fields
        private HierarchyItemPair _hierarchyInfo;
        private bool _isNestedInfoValid;
        private bool _isNestedItem;
        private HierarchyItemPair _nestedInfo;

        // Methods
        private HierarchyItemIdentity(IVsHierarchy hierarchy, uint itemid)
        {
            IVsHierarchy hierarchy2;
            uint num;
            if (((itemid == 0xfffffffe) && HierarchyUtilities.TryGetHierarchyProperty<IVsHierarchy>(hierarchy, itemid, -2032, out hierarchy2)) && HierarchyUtilities.TryGetHierarchyProperty<uint>(hierarchy, itemid, -2033, var => Unbox.AsUInt32(var), out num))
            {
                this._isNestedInfoValid = true;
                this._isNestedItem = true;
                this._hierarchyInfo = new HierarchyItemPair(hierarchy2, num);
                this._nestedInfo = new HierarchyItemPair(hierarchy, itemid);
            }
            else
            {
                this._hierarchyInfo = new HierarchyItemPair(hierarchy, itemid);
            }
        }

        public static HierarchyItemIdentity Create(IVsHierarchy hierarchy, uint itemid)
        {
            HierarchyItemIdentity identity = HierarchyManager.Current.TryGetHierarchyItemIdentity(hierarchy, itemid);
            if (identity == null)
            {
                identity = new HierarchyItemIdentity(hierarchy, itemid);
                HierarchyManager.Current.AddHierarchyItemIdentity(identity);
            }
            return identity;
        }

        private void EnsureNestedInfo()
        {
            if (!this._isNestedInfoValid)
            {
                this._isNestedItem = this.MaybeMapToNested(out this._nestedInfo);
                this._isNestedInfoValid = true;
            }
        }

        public override bool Equals(object obj)
        {
            HierarchyItemIdentity identity = obj as HierarchyItemIdentity;
            if (identity == null)
            {
                return false;
            }
            return (this.HierarchyInfo == identity.HierarchyInfo);
        }

        public override int GetHashCode()
        {
            int num;
            IntPtr pUnk = ComUtilities.QueryInterfaceIUnknown(this.Hierarchy);
            try
            {
                num = pUnk.ToInt32() ^ ((int)this.ItemID);
            }
            finally
            {
                if (pUnk != IntPtr.Zero)
                {
                    Marshal.Release(pUnk);
                }
            }
            return num;
        }

        private bool MaybeMapToNested(out HierarchyItemPair nestedInfo)
        {
            uint num;
            IntPtr ptr;
            Guid gUID = typeof(IVsHierarchy).GUID;
            if (ErrorHandler.Succeeded(this.Hierarchy.GetNestedHierarchy(this.ItemID, ref gUID, out ptr, out num)) && (ptr != IntPtr.Zero))
            {
                IVsHierarchy objectForIUnknown;
                using (SafeIUnknown unknown = new SafeIUnknown(ptr))
                {
                    objectForIUnknown = Marshal.GetObjectForIUnknown(unknown.Value) as IVsHierarchy;
                }
                nestedInfo = new HierarchyItemPair(objectForIUnknown, num);
                return true;
            }
            nestedInfo = this._hierarchyInfo;
            return false;
        }

        public static bool operator ==(HierarchyItemIdentity first, HierarchyItemIdentity second)
        {
            return object.Equals(first, second);
        }

        public static bool operator !=(HierarchyItemIdentity first, HierarchyItemIdentity second)
        {
            return !object.Equals(first, second);
        }

        // Properties
        public IVsHierarchy Hierarchy
        {
            get
            {
                return this._hierarchyInfo.Hierarchy;
            }
        }

        public HierarchyItemPair HierarchyInfo
        {
            get
            {
                return this._hierarchyInfo;
            }
        }

        public bool IsNestedItem
        {
            get
            {
                this.EnsureNestedInfo();
                return this._isNestedItem;
            }
        }

        public bool IsRoot
        {
            get
            {
                return (this.NestedItemID == 0xfffffffe);
            }
        }

        public bool IsSolutionNode
        {
            get
            {
                return ((this.Hierarchy is IVsSolution) && (this.ItemID == 0xfffffffe));
            }
        }

        public uint ItemID
        {
            get
            {
                return this._hierarchyInfo.ItemID;
            }
        }

        public IVsHierarchy NestedHierarchy
        {
            get
            {
                this.EnsureNestedInfo();
                return this._nestedInfo.Hierarchy;
            }
        }

        public HierarchyItemPair NestedInfo
        {
            get
            {
                this.EnsureNestedInfo();
                return this._nestedInfo;
            }
        }

        public uint NestedItemID
        {
            get
            {
                this.EnsureNestedInfo();
                return this._nestedInfo.ItemID;
            }
        }
    }



}