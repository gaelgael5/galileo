using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell.Interop;
using System.Runtime.InteropServices;

namespace devtm.Documentation.HierarchyModel
{

    internal class HierarchyIdentity
    {
        // Methods
        public HierarchyIdentity(IVsHierarchy hierarchy)
        {
            if (hierarchy == null)
            {
                throw new ArgumentNullException("hierarchy");
            }
            this.Hierarchy = hierarchy;
        }

        public override bool Equals(object obj)
        {
            HierarchyIdentity identity = obj as HierarchyIdentity;
            if (identity == null)
            {
                return false;
            }
            return ComUtilities.IsSameComObject(this.Hierarchy, identity.Hierarchy);
        }

        public override int GetHashCode()
        {
            int num;
            IntPtr pUnk = ComUtilities.QueryInterfaceIUnknown(this.Hierarchy);
            try
            {
                num = pUnk.ToInt32();
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

        public static bool operator ==(HierarchyIdentity first, HierarchyIdentity second)
        {
            if (object.ReferenceEquals(first, second))
            {
                return true;
            }
            if (object.ReferenceEquals(first, null))
            {
                return false;
            }
            return first.Equals(second);
        }

        public static bool operator !=(HierarchyIdentity first, HierarchyIdentity second)
        {
            return !(first == second);
        }

        // Properties
        public IVsHierarchy Hierarchy { get; private set; }

    }




}
