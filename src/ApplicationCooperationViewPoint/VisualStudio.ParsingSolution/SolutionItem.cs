using System;
using System.Collections.Generic;
using System.Linq;

using EnvDTE;

namespace VisualStudio.ParsingSolution
{


    public class SolutionItem
    {


        protected EnvDTE.Project project;

        public SolutionItem()
        {

        }

        protected Dictionary<string, Property> _properties = null;
        protected Property FindProperty(string p)
        {

            if (_properties == null)
                BuildProperties();

            try
            {
                var prop = _properties[p];
                return prop;
            }
            catch (Exception)
            {
                
            }

            return null;
        }

        protected virtual void BuildProperties()
        {
            _properties = new Dictionary<string, Property>();
            try
            {

                foreach (Property item in project.Properties)                
                    _properties.Add(item.Name, item);
                
            }
            catch (Exception) { }

        }

        public SolutionItem(EnvDTE.Project project)
        {
            // TODO: Complete member initialization
            this.project = project;
        }


        public virtual IEnumerable<T> GetItem<T>(bool recurcif = true) where T : SolutionItem
        {

            if (project != null && project.ProjectItems != null)
            {

                foreach (EnvDTE.ProjectItem s in project.ProjectItems)
                {
                    if (s == null)
                        continue;

                    SolutionItem fld = null;

                    if (s.SubProject is EnvDTE.Project proj)
                        fld = new Project(proj);

                    else if (s.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
                        fld = new FolderSolution(s as EnvDTE.Project);

                    else if (s.Kind == "{EA6618E8-6E24-4528-94BE-6889FE16485C}")
                        fld = new VirtualFolder(s as EnvDTE.Project);

                    else if (s.Kind == "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}")
                        fld = new ItemFolder(s);
                    else
                    {

                        // 
                        fld = new Item(s);
                    }



                    if (fld is T)
                        yield return fld as T;


                    if (recurcif)
                        foreach (SolutionItem i2 in fld.GetItem<T>(recurcif))
                            yield return i2 as T;


                }

            }

        }


        public SolutionItem Find(string p)
        {

            SolutionItem i = GetItem<SolutionItem>(false).Where(c => c.Name == p).FirstOrDefault();

            return i;

        }


        public virtual string Kind
        {
            get
            {
                return project.Kind;
            }
        }


        public virtual string Name
        {
            get
            {
                return project.Name;
            }
            set
            {
                project.Name = value;
            }
        }


        public virtual Object Object { get { return project; } }


    }


}
