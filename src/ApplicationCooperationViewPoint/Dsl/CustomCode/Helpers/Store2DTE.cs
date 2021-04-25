//***************************************************************************
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the MICROSOFT VISUAL STUDIO 2010
//    VISUALIZATION AND MODELING SOFTWARE DEVELOPMENT KIT license terms.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//***************************************************************************
using System;
using System.Diagnostics.Contracts;
using System.IO;
using EnvDTE;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Shell;
using System.Windows;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel.Composition;
using DslShell = global::Microsoft.VisualStudio.Modeling.Shell;
using System.Linq;
using System.Collections.Generic;

namespace Bb
{
    /// <summary>
    /// Set of utility methods enabling accessing information about the model in store when it is
    /// hosted into Visual Studio.
    /// </summary>
    public static class Store2DTE
    {
        /// <summary>
        /// Get the filename in which a Store is persisted
        /// </summary>
        /// <param name="store">Store of interest</param>
        /// <returns>
        /// The pathname for the file in which the store is serialized if this method is called
        /// from visual studio, and <c>null</c> otherwise
        /// </returns>
        public static string GetFileNameForStore(Store store)
        {

            Contract.Requires(store != null);

            try
            {

                var e = store.ElementDirectory;

                if (e != null)
                {

                    var f = e.AllElements.FirstOrDefault();
                    if (f != null)
                    {

                        //var j = f as devtm.Editor.TypeSelector.IFilePath;
                        //if (j != null)
                        //{
                        //    if (!string.IsNullOrEmpty(j.FilePath))
                        //        return j.FilePath;
                        //}

                        var mm = PresentationViewsSubject.GetPresentation(f).FirstOrDefault() as ShapeElement;
                        if (mm != null)
                        {
                            if (mm.Diagram.ActiveDiagramView is VSDiagramView diagr)
                            {
                                // Get the corresponding file
                                string filename = diagr.DocData.FileName;
                                return filename;
                            }

                        }
                    }

                }

            }
            catch (Exception)
            {
                string ee = "";
            }


            return string.Empty;
        }


        internal static IEnumerable<ProjectItem> GetOtherModels(Store store)
        {

            ProjectItem item = GetProjectItemForStore(store);

            if (item != null)
            {
                var tb = item.Name.Split('.');

                if (tb.Length > 1)
                {

                    string extension = item.Name.Split('.')[1];

                    foreach (ProjectItem i in item.Collection)
                        if (item != i && i.Name.EndsWith(extension))
                            yield return i;

                }
            }

        }


        /// <summary>
        /// Get the project Item corresponding to the item in the store
        /// </summary>
        /// <param name="store">Store of interest</param>
        /// <returns>The project item if this method is called from Visual Studio
        /// and <c>null</c> otherwise</returns>
        internal static ProjectItem GetProjectItemForStore(Store store, string storeFileName = "")
        {

            if (store == null)
                return null;

            DTE dte = (DTE)store.GetService(typeof(DTE));

            if (dte == null)
                dte = (DTE)Package.GetGlobalService(typeof(DTE));

            if (dte == null && provider != null)
                dte = provider;

            if (dte != null)
            {
                if (provider == null)
                    provider = dte;

                if (String.IsNullOrEmpty(storeFileName))
                    storeFileName = GetFileNameForStore(store);

                return dte.Solution.FindProjectItem(storeFileName);
            }
            
                
            return null;
            
        }
       

        [ThreadStatic]
        private static DTE provider;

        /// <summary>
        /// Get the project Item corresponding to the item in the store
        /// </summary>
        /// <param name="store">Store of interest</param>
        /// <returns>The project item if this method is called from Visual Studio
        /// and <c>null</c> otherwise</returns>
        internal static Solution GetSolutionForStore(Store store)
        {

            if (store == null)
                return null;

            DTE dte = (DTE)store.GetService(typeof(DTE));
            if (dte != null)
                return dte.Solution;
            else
                return null;
        }


        /// <summary>
        /// Get the project in which the store is.
        /// </summary>
        /// <param name="store">Store of interest</param>
        /// <returns>The project hosting the model if this method is called from Visual Studio
        /// and <c>null</c> otherwise</returns>
        internal static Project GetProjectForStore(this Store store, string storeFileName = "")
        {
            Contract.Requires(store != null);

            ProjectItem projectItem = GetProjectItemForStore(store, storeFileName);
            if (projectItem == null)
                return null;
            else
                return projectItem.ContainingProject;
            
        }


        ///// <summary>
        ///// Get the default namespace of a store
        ///// </summary>
        ///// <param name="store">Store of interest</param>
        ///// <returns>The default namespace for the file in which the model is serialized if this method is called from Visual Studio
        ///// and <c>string.Empty</c> otherwise</returns>
        ///// <remarks>RootNamespace property of the project to which this item belongs, added to the relative path of the project item with
        ///// respect to the project's path</remarks>
        //public static string GetDefaultNamespace(Store store)
        //{
        //    Contract.Requires(store != null);

        //    // Get the project holding the store
        //    Project project = GetProjectForStore(store);
        //    if (project == null)
        //        return string.Empty;
            
        //    // Get its properties
        //    if (project.Properties == null)
        //        return string.Empty;
            
        //    // Get the FullPath of the project
        //    string projectDirectoryName = Path.GetDirectoryName(project.Properties.Item("FullPath").Value as string);

        //    // Get the directory of the model held in the store
        //    string storeDirectory = Path.GetDirectoryName(GetFileNameForStore(store));

        //    // Compute the relative path of the model with respect to the project, hence the default namespace
        //    if ((storeDirectory.StartsWith(projectDirectoryName, StringComparison.OrdinalIgnoreCase)) && (storeDirectory != projectDirectoryName))
        //        return (project.Properties.Item("RootNamespace").Value as string) + "." + storeDirectory.Substring(projectDirectoryName.Length + 1);
        //    else
        //        return project.Properties.Item("RootNamespace").Value as string;

        //}

    }
}
