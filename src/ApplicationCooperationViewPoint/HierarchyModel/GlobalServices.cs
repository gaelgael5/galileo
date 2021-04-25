using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.ComponentModelHost;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;

namespace devtm.Documentation.HierarchyModel
{

    public static class GlobalServices
    {
        // Properties
        public static Microsoft.VisualStudio.OLE.Interop.IOleComponentManager ComponentManager
        {
            get
            {
                return (Package.GetGlobalService(typeof(Microsoft.VisualStudio.OLE.Interop.SOleComponentManager)) as Microsoft.VisualStudio.OLE.Interop.IOleComponentManager);
            }
        }

        public static Microsoft.VisualStudio.ComponentModelHost.IComponentModel ComponentModel
        {
            get
            {
                return (Package.GetGlobalService(typeof(Microsoft.VisualStudio.ComponentModelHost.IComponentModel)) as Microsoft.VisualStudio.ComponentModelHost.IComponentModel);
            }
        }

        public static DTE DTE
        {
            get
            {
                return (Package.GetGlobalService(typeof(SDTE)) as DTE);
            }
        }

        public static IVsFindSymbol FindSymbol
        {
            get
            {
                return (Package.GetGlobalService(typeof(SVsObjectSearch)) as IVsFindSymbol);
            }
        }

        public static IVsMonitorSelection MonitorSelection
        {
            get
            {
                return (Package.GetGlobalService(typeof(SVsShellMonitorSelection)) as IVsMonitorSelection);
            }
        }

        public static IVsObjectManager2 ObjectManager
        {
            get
            {
                return (Package.GetGlobalService(typeof(SVsObjectManager)) as IVsObjectManager2);
            }
        }

        public static IVsRunningDocumentTable RDT
        {
            get
            {
                return (Package.GetGlobalService(typeof(SVsRunningDocumentTable)) as IVsRunningDocumentTable);
            }
        }

        public static IVsShell Shell
        {
            get
            {
                return (Package.GetGlobalService(typeof(SVsShell)) as IVsShell);
            }
        }

        public static IVsSolution Solution
        {
            get
            {
                return (Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution);
            }
        }

        public static IVsSolutionBuildManager SolutionBuildManager
        {
            get
            {
                return (Package.GetGlobalService(typeof(SVsSolutionBuildManager)) as IVsSolutionBuildManager);
            }
        }

        //public static IVsThreadedWaitDialog2 ThreadedWaitDialog
        //{
        //    get
        //    {
        //        return (Package.GetGlobalService(typeof(SVsThreadedWaitDialog)) as IVsThreadedWaitDialog2);
        //    }
        //}

        public static IVsUIShell UIShell
        {
            get
            {
                return (Package.GetGlobalService(typeof(SVsUIShell)) as IVsUIShell);
            }
        }
    }


}
