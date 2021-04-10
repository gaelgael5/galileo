﻿//------------------------------------------------------------------------------
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
			get { return global::Bb.ApplicationCooperationViewPoint.CooperationModel.DomainClassId; }
		}
		
		/// <summary>
		/// Returns the root elements to be displayed in the explorer.
		///</summary>
		protected override global::System.Collections.IList FindRootElements(DslModeling::Store store)
		{
			return store.ElementDirectory.FindElements( this.RootElementDomainClassId);
		}
	}
}
	


