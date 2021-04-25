using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;

namespace devtm.Documentation.HierarchyModel
{
   
    public static class HierarchyUtilities
{

    // Methods

    //public static int ExecHierParentChain(IVsUIHierarchy lpUIHCmd, IVsUIHierarchy lpUIHCurrent, uint itemidCurrent, ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
    //{
    //    IVsUIHierarchy hierarchy;
    //    uint num2;
    //    int num = -2147221248;
    //    if (!FindNestedNode(lpUIHCurrent, 0xfffffffe, out hierarchy, out num2))
    //    {
    //        Guid guid = pguidCmdGroup;
    //        return lpUIHCurrent.ExecCommand(itemidCurrent, ref guid, nCmdID, nCmdexecopt, pvaIn, pvaOut);
    //    }
    //    uint num3 = 0xfffffffd;
    //    if ((lpUIHCmd == lpUIHCurrent) && (itemidCurrent == 0xfffffffe))
    //    {
    //        num3 = num2;
    //    }
    //    num = ExecHierParentChain(lpUIHCmd, hierarchy, num3, ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
    //    if (((num != -2147467263) && (num != -2147352573)) && ((num != -2147221248) && (num != -2147221244)))
    //    {
    //        return num;
    //    }
    //    Guid guid2 = pguidCmdGroup;
    //    return lpUIHCurrent.ExecCommand(itemidCurrent, ref guid2, nCmdID, nCmdexecopt, pvaIn, pvaOut);
    //}

    //public static HierarchyListItem FindCommonAncestor(IEnumerable<HierarchyListItem> items)
    //{
    //    HierarchyListItem item = null;
    //    foreach (HierarchyListItem item2 in items)
    //    {
    //        if (item == null)
    //        {
    //            item = item2;
    //        }
    //        else
    //        {
    //            item = item.FindCommonAncestor<HierarchyListItem>(item2, h => h.Parent);
    //        }
    //    }
    //    return item;
    //}

    private static T FindCommonAncestor<T>(this T obj1, T obj2, Func<T, T> parentEvaluator) where T: class
    {
        if ((obj1 != null) && (obj2 != null))
        {
            HashSet<T> set = new HashSet<T>();
            while (obj1 != null)
            {
                set.Add(obj1);
                obj1 = parentEvaluator(obj1);
            }
            while (obj2 != null)
            {
                if (set.Contains(obj2))
                {
                    return obj2;
                }
                obj2 = parentEvaluator(obj2);
            }
        }
        return default(T);
    }

    //public static HierarchyListItem FindHierarchyItem(IVsWindowFrame windowFrame)
    //{
    //    object obj2;
    //    object obj3;
    //    if (ErrorHandler.Succeeded(windowFrame.GetProperty(-4005, out obj2)) && ErrorHandler.Succeeded(windowFrame.GetProperty(-4006, out obj3)))
    //    {
    //        IVsHierarchy vsHierarchy = obj2 as IVsHierarchy;
    //        uint itemid = (uint) ((int) obj3);
    //        if (vsHierarchy != null)
    //        {
    //            return HierarchyManager.Current.GetHierarchyItem(vsHierarchy, itemid);
    //        }
    //    }
    //    return null;
    //}

    //public static HierarchyListItem FindHierarchyItem(IVsHierarchy hierarchy, uint itemID, string fullpath, bool searchHiddenItems)
    //{
    //    return new HierarchySearchTree(searchHiddenItems).FindItem(HierarchyItemIdentity.Create(hierarchy, itemID), fullpath);
    //}

    //private static bool FindNestedNode(IVsUIHierarchy lpIVsUIHNested, uint itemidNested, out IVsUIHierarchy uih, out uint itemid)
    //{
    //    HierarchyIdentity identity = new HierarchyIdentity(lpIVsUIHNested);
    //    foreach (HierarchyItemIdentity identity2 in from item in HierarchyManager.Current.Items select item.HierarchyIdentity)
    //    {
    //        if (identity2.IsNestedItem)
    //        {
    //            HierarchyIdentity identity3 = new HierarchyIdentity(identity2.NestedHierarchy);
    //            if ((identity == identity3) && (itemidNested == identity2.NestedItemID))
    //            {
    //                uih = identity2.Hierarchy as IVsUIHierarchy;
    //                itemid = identity2.ItemID;
    //                return true;
    //            }
    //        }
    //    }
    //    uih = null;
    //    itemid = uint.MaxValue;
    //    return false;
    //}

    public static uint GetFirstChild(IVsHierarchy hierarchy, uint itemid, bool tryVisibleFirst)
    {
        long num;
        if (tryVisibleFirst && TryGetHierarchyProperty<long>(hierarchy, itemid, -2041, obj => Convert.ToInt64(obj), out num))
        {
            return (uint) num;
        }
        if (TryGetHierarchyProperty<long>(hierarchy, itemid, -1001, obj => Convert.ToInt64(obj), out num))
        {
            return (uint) num;
        }
        return uint.MaxValue;
    }

    public static T GetHierarchyProperty<T>(IVsHierarchy hierarchy, uint itemid, int propid)
    {
        return GetHierarchyProperty<T>(hierarchy, itemid, propid, obj => (T) obj);
    }

    public static T GetHierarchyProperty<T>(IVsHierarchy hierarchy, uint itemid, int propid, Func<object, T> converter)
    {
        object obj2;
        ErrorHandler.ThrowOnFailure(hierarchy.GetProperty(itemid, propid, out obj2));
        return converter(obj2);
    }

    public static uint GetNextSibling(IVsHierarchy hierarchy, uint itemid, bool tryVisibleFirst)
    {
        long num;
        if (tryVisibleFirst && TryGetHierarchyProperty<long>(hierarchy, itemid, -2042, obj => Convert.ToInt64(obj), out num))
        {
            return (uint) num;
        }
        if (TryGetHierarchyProperty<long>(hierarchy, itemid, -1002, obj => Convert.ToInt64(obj), out num))
        {
            return (uint) num;
        }
        return uint.MaxValue;
    }

    public static Project GetProject(HierarchyItemIdentity hierarchyItem)
    {
        object obj2;
        if ((hierarchyItem != null) && ErrorHandler.Succeeded(hierarchyItem.NestedHierarchy.GetProperty(0xfffffffe, -2027, out obj2)))
        {
            return (obj2 as Project);
        }
        return null;
    }

    //public static Project GetProject(HierarchyListItem listItem)
    //{
    //    if (listItem == null)
    //    {
    //        return null;
    //    }
    //    return GetProject(listItem.HierarchyIdentity);
    //}

    public static bool IsHiddenItem(IVsHierarchy hierarchy, uint itemid)
    {
        bool flag;
        return (TryGetHierarchyProperty<bool>(hierarchy, itemid, -2043, out flag) && flag);
    }

    public static bool IsMiscellaneousProject(Project project)
    {
        return ((project != null) && project.UniqueName.Equals("<MiscFiles>", StringComparison.Ordinal));
    }

    public static bool IsProject(HierarchyItemIdentity hierarchyIdentity)
    {
        return ((hierarchyIdentity.NestedHierarchy is IVsProject) && (hierarchyIdentity.NestedItemID == 0xfffffffe));
    }

    public static bool IsSolutionFolder(HierarchyItemIdentity hierarchyIdentity)
    {
        Project project = GetProject(hierarchyIdentity);
        return ((project != null) && (project.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}"));
    }

    public static bool IsStartupProject(HierarchyItemIdentity hierarchyIdentity)
    {
        IVsHierarchy hierarchy;
        if (!IsProject(hierarchyIdentity))
        {
            return false;
        }
        IVsSolutionBuildManager solutionBuildManager = GlobalServices.SolutionBuildManager;
        if (solutionBuildManager == null)
        {
            return false;
        }
        if ((solutionBuildManager.get_StartupProject(out hierarchy) < 0) || (hierarchy == null))
        {
            return false;
        }
        HierarchyItemIdentity identity = HierarchyItemIdentity.Create(hierarchy, 0xfffffffe);
        return hierarchyIdentity.Equals(identity);
    }

    //public static int QueryStatusHierParentChain(IVsUIHierarchy lpUIHCmd, IVsUIHierarchy lpUIHCurrent, uint itemidCurrent, ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
    //{
    //    IVsUIHierarchy hierarchy;
    //    uint num2;
    //    int num = -2147221248;
    //    if (!FindNestedNode(lpUIHCurrent, 0xfffffffe, out hierarchy, out num2))
    //    {
    //        Guid guid = pguidCmdGroup;
    //        return lpUIHCurrent.QueryStatusCommand(itemidCurrent, ref guid, cCmds, prgCmds, pCmdText);
    //    }
    //    uint num3 = 0xfffffffd;
    //    if ((lpUIHCmd == lpUIHCurrent) && (itemidCurrent == 0xfffffffe))
    //    {
    //        num3 = num2;
    //    }
    //    num = QueryStatusHierParentChain(lpUIHCmd, hierarchy, num3, ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
    //    if (((num != -2147467263) && (num != -2147352573)) && ((num != -2147221248) && (num != -2147221244)))
    //    {
    //        return num;
    //    }
    //    Guid guid2 = pguidCmdGroup;
    //    return lpUIHCurrent.QueryStatusCommand(itemidCurrent, ref guid2, cCmds, prgCmds, pCmdText);
    //}

    public static bool TryGetHierarchyProperty<T>(IVsHierarchy hierarchy, uint itemid, int propid, out T value)
    {
        object obj2;
        if (ErrorHandler.Succeeded(hierarchy.GetProperty(itemid, propid, out obj2)) && (obj2 is T))
        {
            value = (T) obj2;
            return true;
        }
        value = default(T);
        return false;
    }

    public static bool TryGetHierarchyProperty<T>(IVsHierarchy hierarchy, uint itemid, int propid, Func<object, T> converter, out T value)
    {
        object obj2;
        if (ErrorHandler.Succeeded(hierarchy.GetProperty(itemid, propid, out obj2)))
        {
            value = converter(obj2);
            return true;
        }
        value = default(T);
        return false;
    }
}

 
 


}
