using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;


namespace VisualStudio.ParsingSolution
{
    public class Solution : SolutionItem
    {

        public Solution (IServiceProvider service) : base() 
        {
            //this.service = service;
            dte = (EnvDTE.DTE)service.GetService(typeof(EnvDTE.DTE));
            solution = dte.Solution;
        }

        public Solution()
            : base()
        {
            dte = (EnvDTE.DTE)Package.GetGlobalService(typeof(EnvDTE.DTE));
            if (dte != null)
                solution = dte.Solution;
        }

        public Solution(EnvDTE.Solution solution) : base()
        {
            if (solution != null)
            {
                dte = solution.DTE;
                if (dte != null)
                    this.solution = dte.Solution;
            }
        }

        public override string Name
        {
            get { return (string)FindProperty("Name").Value; }
            set { FindProperty("Name").Value = value; }
        }

        public EnvDTE.DTE GetDTE { get { return dte; } }


        public override IEnumerable<T> GetItem<T>(bool recurcif = true)
        {


            if (dte.Solution.Projects != null)
            {

                EnvDTE.DTE dte1 = dte ?? (EnvDTE.DTE)Package.GetGlobalService(typeof(EnvDTE.DTE));

                foreach (EnvDTE.Project project in dte1.Solution.Projects)
                {

                    SolutionItem fld = null;

                    if (project.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}" || project.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
                        fld = new FolderSolution(project);

                    else if (project.Kind == "{EA6618E8-6E24-4528-94BE-6889FE16485C}")
                        fld = new VirtualFolder(project);

                    else
                        fld = new Project(project);

                    if (fld is T)
                        yield return fld as T;

                    foreach (T item in fld.GetItem<T>(recurcif))
                        yield return item;

                }

            }

        }



        protected override void BuildProperties()
        {
            _properties = new Dictionary<string, Property>();

            foreach (Property item in solution.Properties)
                _properties.Add(item.Name, item);

        }

        private readonly EnvDTE.Solution solution;
        readonly EnvDTE.DTE dte;
        //readonly IServiceProvider service;


    }
}
