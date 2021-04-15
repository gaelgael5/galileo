
namespace VisualStudio.ParsingSolution
{

    public class VirtualFolder : SolutionItem
    {
        
        public VirtualFolder(EnvDTE.Project project) : base(project)
        {
            
        }

        public virtual string FullName
        {
            get
            {
                return project.FullName;
            }
        }

    }

}
