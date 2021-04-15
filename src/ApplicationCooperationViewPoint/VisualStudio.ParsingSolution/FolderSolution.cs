
namespace VisualStudio.ParsingSolution
{
    
    public class FolderSolution : SolutionItem
    {

        public FolderSolution(EnvDTE.Project project) : base(project)
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
