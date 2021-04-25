using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;

namespace devtm.Documentation.HierarchyModel
{

    public static class ComUtilities
    {
        // Methods
        public static bool IsSameComObject(object object1, object object2)
        {
            bool flag = false;
            IntPtr zero = IntPtr.Zero;
            IntPtr objB = IntPtr.Zero;
            try
            {
                if ((object1 != null) && (object2 != null))
                {
                    zero = QueryInterfaceIUnknown(object1);
                    objB = QueryInterfaceIUnknown(object2);
                    flag = object.Equals(zero, objB);
                }
            }
            finally
            {
                if (zero != IntPtr.Zero)
                {
                    Marshal.Release(zero);
                }
                if (objB != IntPtr.Zero)
                {
                    Marshal.Release(objB);
                }
            }
            return flag;
        }

        public static IntPtr QueryInterfaceIUnknown(object objectToQuery)
        {
            IntPtr ptr2;
            bool flag = false;
            IntPtr zero = IntPtr.Zero;
            try
            {
                if (objectToQuery is IntPtr)
                {
                    zero = (IntPtr)objectToQuery;
                }
                else
                {
                    zero = Marshal.GetIUnknownForObject(objectToQuery);
                    flag = true;
                }
                Guid iid = VSConstants.IID_IUnknown;
                ErrorHandler.ThrowOnFailure(Marshal.QueryInterface(zero, ref iid, out ptr2));
            }
            finally
            {
                if (flag && (zero != IntPtr.Zero))
                {
                    Marshal.Release(zero);
                }
            }
            return ptr2;
        }
    }

}
