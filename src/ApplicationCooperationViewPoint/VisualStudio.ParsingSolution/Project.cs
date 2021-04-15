using System.Collections.Generic;
using System.Linq;
using VSLangProj;

namespace VisualStudio.ParsingSolution
{
    public class Project : SolutionItem
    {
       

        public Project(EnvDTE.Project project) : base(project)
        {

            /*             
                TargetFrameworkMoniker
                ComVisible
                EnableSecurityDebugging
                OptionCompare
                StartupObject
                ManifestCertificateThumbprint
                Trademark
                AssemblyOriginatorKeyFileType
                FileName
                WebServer
                AssemblyOriginatorKeyMode
                AssemblyKeyContainerName
                ProjectType
                ReferencePath
                PreBuildEvent
                Copyright
                ApplicationIcon
                ExcludedPermissions
                RunPostBuildEvent
                DefaultTargetSchema

                ManifestTimestampUrl
                ManifestKeyFile
                DebugSecurityZoneURL
                Product
                PostBuildEvent
                OptionStrict
                DefaultHTMLPageLayout
                DelaySign
                OutputType
                NeutralResourcesLanguage
                OptionExplicit
                OutputFileName
                ServerExtensionsVersion
                AssemblyGuid
                GenerateManifests
                AssemblyVersion
                Win32ResourceFile
                Description
                URL
                DefaultClientScript
                TargetFramework
                SignManifests
                OfflineURL
                WebServerVersion
                Publish
                AssemblyType
                FullPath
                WebAccessMethod
                AssemblyKeyProviderName
                TypeComplianceDiagnostics
                Company
                ActiveFileSharePath
                AssemblyOriginatorKeyFile
                ApplicationManifest
                AssemblyFileVersion
                AspnetVersion
                FileSharePath

                LocalPath
                
                LinkRepair
                TargetZone
                SignAssembly

             * 
             */

        }

        public string Title
        {
            get 
            { 
                var prop = FindProperty("Title");

                if (prop != null) 
                    return (string)prop.Value;

                return string.Empty;

            }
            set { FindProperty("Title").Value = value; }
        }

        public string RootNamespace
        {
            get 
            { 
                var prop = FindProperty("RootNamespace");

                if (prop != null) 
                    return (string)prop.Value;

                return string.Empty;
            }
            set { FindProperty("RootNamespace").Value = value; }
        }

        public string AssemblyName
        {
            get 
            {
                var prop = FindProperty("AssemblyName");

                if (prop != null) 
                    return (string)prop.Value;

                return string.Empty;
            }
            set { FindProperty("AssemblyName").Value = value; }
        }



        public string DefaultNamespace
        {
            get 
            { 
                var prop = FindProperty("DefaultNamespace");
                
                if (prop != null)
                    return (string)prop.Value;

                return string.Empty;
            }
            set { FindProperty("DefaultNamespace").Value = value; }
        }

        public EnvDTE.Project Source
        {
            get 
            {
                return project;
            }
        }

        public virtual string FullName
        {
            get
            {
                return project.FullName;
            }
        }


        public References GetReferences()
        {

            if (project.Object != null)
            {
                if (project.Object is VSProject vsProject)
                    return vsProject.References;
            }

            return null;

        }


        public Reference[] GetReferencesListeNoInSolution()
        {

            VSProject vsproj = project.Object as VSProject;
            List<Reference> prjs = new List<Reference>();

            foreach (Reference item in vsproj.References.OfType<Reference>())

                if (item.SourceProject == null)
                    prjs.Add(item);

            return prjs.ToArray();

        }

        public EnvDTE.Project[] GetReferencedProjectListe(bool recursif)
        {
            return GetReferenceedProjectListe(project, recursif);
        }

        private static EnvDTE.Project[] GetReferenceedProjectListe(EnvDTE.Project project, bool recursif)
        {

            // récup les projets ou chercher
            VSProject vsproj = project.Object as VSProject;
            List<EnvDTE.Project> prjs = new List<EnvDTE.Project>() { project };

            foreach (Reference item in vsproj.References.OfType<Reference>())

                if (item.SourceProject != null)
                {

                    prjs.Add(item.SourceProject);

                    if (recursif)
                    {

                        var lst = GetReferenceedProjectListe(item.SourceProject, recursif);

                        if (lst != null && lst.Count() > 0)
                            prjs.AddRange(lst);

                    }

                }

            return prjs.ToArray();

        }
    }
}
