using System;
using System.Collections.Generic;
using EnvDTE;

namespace VisualStudio.ParsingSolution
{
    public class Item : SolutionItem
    {


        protected EnvDTE.ProjectItem s;


        public Item(EnvDTE.ProjectItem s) : base()
        {
            this.s = s;


            //Debug.WriteLine("----------------------------");
            //foreach (Property item in s.Properties)
            //{
            //    Debug.WriteLine(item.Name);
            //}
            //Debug.WriteLine("----------------------------");

            /*
             
                Extension
                FileName
                CustomToolOutput
                DateModified
                IsLink
                BuildAction
                SubType
                CopyToOutputDirectory
                IsSharedDesignTimeBuildInput
                ItemType
                IsCustomToolOutput
                HTMLTitle
                CustomTool
                URL
                Filesize
                CustomToolNamespace
                Author
                FullPath
                IsDependentFile
                IsDesignTimeBuildInput
                DateCreated
                LocalPath
                ModifiedBy
             * 
             */
        }


        //public string DefaultNamespace
        //{
        //    get { return FindProperty("DefaultNamespace").Value; }
        //    set { FindProperty("DefaultNamespace").Value = value; }
        //}


        protected override void BuildProperties()
        {
            _properties = new Dictionary<string, Property>();

            foreach (Property item in s.Properties)
                _properties.Add(item.Name, item);

        }

        public override IEnumerable<T> GetItem<T>(bool recurcif = true)
        {

            if (s.ProjectItems != null)
                foreach (ProjectItem item in s.ProjectItems)
                {
                    Item i = new Item(item);

                    if (i is T)
                        yield return i as T;

                    if (recurcif)
                        foreach (SolutionItem i2 in i.GetItem<T>(recurcif))
                        yield return i2 as T;

                }

        }


        public override string Kind
        {
            get
            {
                return s.Kind;
            }
        }


        public override string Name
        {
            get
            {
                return s.Name;
            }
            set
            {
                s.Name = value;
            }
        }

        public override Object Object { get { return s; } }

    }
}
