//------------------------------------------------------------------------------
// <auto-generated>
//	 This code was generated by a tool.
//
//	 Changes to this file may cause incorrect behavior and will be lost if
//	 the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslShell = global::Microsoft.VisualStudio.Modeling.Shell;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;

namespace Bb.ApplicationCooperationViewPoint
{
	/// <summary>
	/// Double-derived class to allow easier code customization.
	/// </summary>
	internal partial class ApplicationCooperationViewPointExplorer : ApplicationCooperationViewPointExplorerBase
	{
		/// <summary>
		/// Constructs a new ApplicationCooperationViewPointExplorer.
		/// </summary>
		public ApplicationCooperationViewPointExplorer(global::System.IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
		}
	}
	
	/// <summary>
	/// Control hosted in the ApplicationCooperationViewPointExplorerToolWindow.
	/// </summary>
	internal abstract class ApplicationCooperationViewPointExplorerBase : DslShell::ModelExplorerTreeContainer
	{
		/// <summary>
		/// Constructs a new ApplicationCooperationViewPointExplorerBase.
		/// </summary>
		protected ApplicationCooperationViewPointExplorerBase(global::System.IServiceProvider serviceProvider) : base(serviceProvider)
		{
			
			
			// Adds custom tree node settings...
			global::System.Resources.ResourceManager resourceManager = global::Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDomainModel.SingletonResourceManager;
			
			this.AddExplorerNodeCustomSetting(global::Bb.ApplicationCooperationViewPoint.Concept.DomainClassId, 
							null, 
							true); 
			this.AddExplorerNodeCustomSetting(global::Bb.ApplicationCooperationViewPoint.ConceptElement.DomainClassId, 
							null, 
							true); 
			this.AddExplorerNodeCustomSetting(global::Bb.ApplicationCooperationViewPoint.ConceptSubElement.DomainClassId, 
							null, 
							true); 
			this.AddExplorerNodeCustomSetting(global::Bb.ApplicationCooperationViewPoint.ModelElement.DomainClassId, 
							null, 
							true); 
			this.AddExplorerNodeCustomSetting(global::Bb.ApplicationCooperationViewPoint.SubElement.DomainClassId, 
							null, 
							true); 
			
			// Add a call back to provide ModelElementTreeNode TreeNode name in the Model Explorer
			this.GetModelElementDisplayNameEventHandler = new DslShell.GetModelElementDisplayNameEventHandler(GetModelElementDisplayName);
		}
	
	
	
		/// <summary>
		/// Create IElementVisitor
		/// </summary>
		/// <returns>IElementVisitor</returns>
		protected override DslModeling::IElementVisitor CreateElementVisitor()
		{
			return new DslShell::ExplorerElementVisitor(this);
		}
	
		/// <summary>
		/// Specifies the context menu that should be shown for the model explorer.
		///</summary>
		protected override global::System.ComponentModel.Design.CommandID ContextMenuCommandId
		{
			get
			{
				return Constants.ApplicationCooperationViewPointExplorerMenu;
			}
		}
		
		/// <summary>
		/// Returns the root elements domain class Id. The is the very top level tree node in the TreeView
		///</summary>
		protected override global::System.Guid RootElementDomainClassId
		{
			get { return global::Bb.ApplicationCooperationViewPoint.Model.DomainClassId; }
		}
		
		/// <summary>
		/// Returns the root elements to be displayed in the explorer.
		///</summary>
		protected override global::System.Collections.IList FindRootElements(DslModeling::Store store)
		{
			return store.ElementDirectory.FindElements( this.RootElementDomainClassId);
		}
			
		/// <summary>
		/// Method to supply the name for ModelElementTreeNode object in the TreeView.
		/// </summary>
		/// <param name="modelElement">Element to be displayed in the tree node</param>
		/// <returns>Name shown in the Model Explorer</returns>
		private string GetModelElementDisplayName(DslModeling::ModelElement modelElement)
		{
			string treeNodeDisplayName = null;
			DslModeling::DomainDataDirectory directory = modelElement.Store.DomainDataDirectory;
			DslModeling::DomainPropertyInfo domainPropertyInfo = null;
			DslModeling::ModelElement redirectedElement = null;
			
			switch ( modelElement.GetDomainClass().Id.ToString( "D", System.Globalization.CultureInfo.InvariantCulture) )
			{
				case "eaad73c4-a652-4596-b575-39e82c2f525a":	// Concept.DomainClassId
				{
					domainPropertyInfo = directory.FindDomainProperty( global::Bb.ApplicationCooperationViewPoint.Concept.NameDomainPropertyId);
					redirectedElement = modelElement;
				}			
				break;
				
				case "3aba6490-0175-4397-b4b4-8ba4dd8df1d1":	// ConceptElement.DomainClassId
				{
					domainPropertyInfo = directory.FindDomainProperty( global::Bb.ApplicationCooperationViewPoint.ConceptElement.NameDomainPropertyId);
					redirectedElement = modelElement;
				}			
				break;
				
				case "a3a13452-14ee-4fe8-adcb-fd5c2f06ff68":	// ConceptSubElement.DomainClassId
				{
					domainPropertyInfo = directory.FindDomainProperty( global::Bb.ApplicationCooperationViewPoint.ConceptSubElement.NameDomainPropertyId);
					redirectedElement = modelElement;
				}			
				break;
				
				case "403a7c63-3a36-4881-b0d5-0eaf0dcad50e":	// ModelElement.DomainClassId
				{
					domainPropertyInfo = directory.FindDomainProperty( global::Bb.ApplicationCooperationViewPoint.ModelElement.NameDomainPropertyId);
					redirectedElement = modelElement;
				}			
				break;
				
				case "e39ae37f-7927-4be7-a99c-94d0deb846f7":	// SubElement.DomainClassId
				{
					domainPropertyInfo = directory.FindDomainProperty( global::Bb.ApplicationCooperationViewPoint.SubElement.NameDomainPropertyId);
					redirectedElement = modelElement;
				}			
				break;
				
		
			}
			
			if (domainPropertyInfo != null && redirectedElement != null)
			{
				// Get the name based on the designated domian property
				treeNodeDisplayName = domainPropertyInfo.GetValue(redirectedElement) as string;
			}
			else
			{
				// The passed in modelElement does not have a DomainPath specified. Try access the default name from the element.
				DslModeling::DomainClassInfo.TryGetName(modelElement, out treeNodeDisplayName);
			}
			return treeNodeDisplayName;
		}
	}
}
	


