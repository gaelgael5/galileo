using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace devtm.Documentation.HierarchyModel
{

    public static class Unbox
    {
        // Methods
        public static uint AsUInt32(object var)
        {
            if (var is short)
            {
                return (uint)((short)var);
            }
            if (var is int)
            {
                return (uint)((int)var);
            }
            if (var is long)
            {
                return (uint)((long)var);
            }
            if (var is ushort)
            {
                return (ushort)var;
            }
            if (var is uint)
            {
                return (uint)var;
            }
            if (var is ulong)
            {
                return (uint)((ulong)var);
            }
            if (var is IntPtr)
            {
                IntPtr ptr = (IntPtr)var;
                return (uint)ptr.ToInt32();
            }
            if (var is Enum)
            {
                return Convert.ToUInt32(var);
            }
            return 0;
        }
    }



}
