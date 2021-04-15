using System;
using System.Collections.Generic;
using System.Text;

namespace VisualStudio.ParsingSolution
{
    public static class Helper
    {


        #region Path

        public static bool IsPath(this EnvDTE.ProjectItem source, string path)
        {        
            string path2 = string.Join(@"\", GetPath(source));            
            return String.Equals(path, path2);        
        }

        public static IEnumerable<string> GetPath(EnvDTE.ProjectItem source)
        {

            List<string> l = new List<string>();
            
            var s = source.Collection.Parent;

            EnvDTE.ProjectItem p1 = s as EnvDTE.ProjectItem;
            if (p1 != null)
                l.AddRange(GetPath(p1));
            
            else
            {
                EnvDTE.Project p2 = s as EnvDTE.Project;
                if (p2 != null)
                {
                    l.AddRange(GetPath(p2));
                }
                else
                {

                }
            }

            l.Add(source.Name);
            return l;
        }

        public static IEnumerable<string> GetPath(EnvDTE.Project source)
        {
            List<string> l = new List<string>();

            var s = source.ParentProjectItem;

            EnvDTE.ProjectItem p1 = s as EnvDTE.ProjectItem;
            if (p1 != null)
            {
                l.AddRange(GetPath(p1));
                return l;
            }

            else
            {
                EnvDTE.Project p2 = s as EnvDTE.Project;
                if (p2 != null)
                {
                    l.AddRange(GetPath(p2));

                }
                else
                {

                }
            }

            l.Add(source.Name);
            return l;
        }

        #endregion


        #region Getfiles

        public static IEnumerable<EnvDTE.ProjectItem> GetFiles(EnvDTE.ProjectItems source)
        {

            foreach (EnvDTE.ProjectItem item2 in source)            
                foreach (var item3 in GetFiles(item2))                
                    yield return item3;
            
        }

        public static IEnumerable<EnvDTE.ProjectItem> GetFiles(EnvDTE.ProjectItem source)
        {

            yield return source;

             if (source.ProjectItems != null)
                foreach (EnvDTE.ProjectItem item2 in GetFiles(source.ProjectItems))                                                
                    yield return item2;   
        }

        public static IEnumerable<EnvDTE.ProjectItem> GetFiles(EnvDTE.Project source)
        {

            foreach (EnvDTE.ProjectItem item in source.ProjectItems)
            {
                yield return item;

                if (item.ProjectItems != null)

                    foreach (EnvDTE.ProjectItem item2 in GetFiles(item.ProjectItems))                    
                            yield return item2;                        
                    
            }


        }

        #endregion



        public static string GetNamespace(EnvDTE.ProjectItem source)
        {

            
            StringBuilder s1 = new StringBuilder();

            var s = source.Collection.Parent;

            EnvDTE.ProjectItem p1 = s as EnvDTE.ProjectItem;
            if (p1 != null)
                s1.Append(GetNamespace(p1));

            else
            {
                EnvDTE.Project p2 = s as EnvDTE.Project;
                if (p2 != null)
                {
                    s1.Append(p2.Properties.Item("DefaultNamespace").Value.ToString());
                }
             
            }

            if (s1.Length > 0)
                s1.Append(".");
            s1.Append(source.Name);
            return s1.ToString();

        }


        public static string GetNamespace(EnvDTE.Project projectEnCours)
        {

            StringBuilder s1 = new StringBuilder();

            if (projectEnCours != null)
                s1.Append(projectEnCours.Properties.Item("DefaultNamespace").Value.ToString());

            return s1.ToString();

        }

    }


    
}
