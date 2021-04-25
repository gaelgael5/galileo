using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell.Interop;
using System.Runtime.InteropServices;


namespace devtm.Documentation.HierarchyModel
{

    public class HierarchyItemPair
    {
        // Methods
        public HierarchyItemPair(IVsHierarchy hierarchy, uint itemid)
        {
            this.Hierarchy = hierarchy;
            this.ItemID = itemid;
        }

        public override bool Equals(object obj)
        {
            HierarchyItemPair pair = obj as HierarchyItemPair;
            if (pair == null)
            {
                return false;
            }
            return (ComUtilities.IsSameComObject(this.Hierarchy, pair.Hierarchy) && (this.ItemID == pair.ItemID));
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

        public static bool operator ==(HierarchyItemPair first, HierarchyItemPair second)
        {
            return object.Equals(first, second);
        }

        public static bool operator !=(HierarchyItemPair first, HierarchyItemPair second)
        {
            return !object.Equals(first, second);
        }

        // Properties
        public IVsHierarchy Hierarchy { get; private set; }

        public uint ItemID { get; private set; }
    }



}
