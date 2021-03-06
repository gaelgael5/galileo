//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DslModeling = global::Microsoft.VisualStudio.Modeling;
using DslDesign = global::Microsoft.VisualStudio.Modeling.Design;
using DslDiagrams = global::Microsoft.VisualStudio.Modeling.Diagrams;

[module: global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "type", Target = "Bb.ApplicationCooperationViewPoint.CooperationViewPointDiagram")]

namespace Bb.ApplicationCooperationViewPoint
{
	/// <summary>
	/// Double-derived base class for DomainClass CooperationViewPointDiagram
	/// </summary>
	[DslDesign::DisplayNameResource("Bb.ApplicationCooperationViewPoint.CooperationViewPointDiagram.DisplayName", typeof(global::Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDomainModel), "Bb.ApplicationCooperationViewPoint.GeneratedCode.DomainModelResx")]
	[DslDesign::DescriptionResource("Bb.ApplicationCooperationViewPoint.CooperationViewPointDiagram.Description", typeof(global::Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDomainModel), "Bb.ApplicationCooperationViewPoint.GeneratedCode.DomainModelResx")]
	[DslModeling::DomainModelOwner(typeof(global::Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDomainModel))]
	[global::System.CLSCompliant(true)]
	[DslModeling::DomainObjectId("ca3f1951-3a94-4564-a805-02e3bfc91b8b")]
	public abstract partial class CooperationViewPointDiagramBase : DslDiagrams::Diagram
	{
		#region Diagram boilerplate
		private static DslDiagrams::StyleSet classStyleSet;
		private static global::System.Collections.Generic.IList<DslDiagrams::ShapeField> shapeFields;
		/// <summary>
		/// Per-class style set for this shape.
		/// </summary>
		protected override DslDiagrams::StyleSet ClassStyleSet
		{
			get
			{
				if (classStyleSet == null)
				{
					classStyleSet = CreateClassStyleSet();
				}
				return classStyleSet;
			}
		}
		
		/// <summary>
		/// Per-class ShapeFields for this shape
		/// </summary>
		public override global::System.Collections.Generic.IList<DslDiagrams::ShapeField> ShapeFields
		{
			get
			{
				if (shapeFields == null)
				{
					shapeFields = CreateShapeFields();
				}
				return shapeFields;
			}
		}
		#endregion
		#region Toolbox filters
		private static global::System.ComponentModel.ToolboxItemFilterAttribute[] toolboxFilters = new global::System.ComponentModel.ToolboxItemFilterAttribute[] {
					new global::System.ComponentModel.ToolboxItemFilterAttribute(global::Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointToolboxHelperBase.ToolboxFilterString, global::System.ComponentModel.ToolboxItemFilterType.Require) };
		
		/// <summary>
		/// Toolbox item filter attributes for this diagram.
		/// </summary>
		public override global::System.Collections.ICollection TargetToolboxItemFilterAttributes
		{
			get
			{
				return toolboxFilters;
			}
		}
		#endregion
		#region Auto-placement
		/// <summary>
		/// Indicate that child shapes should added through view fixup should be placed automatically.
		/// </summary>
		public override bool ShouldAutoPlaceChildShapes
		{
			get
			{
				return true;
			}
		}
		#endregion
		#region Shape mapping
		/// <summary>
		/// Called during view fixup to ask the parent whether a shape should be created for the given child element.
		/// </summary>
		/// <remarks>
		/// Always return true, since we assume there is only one diagram per model file for DSL scenarios.
		/// </remarks>
		protected override bool ShouldAddShapeForElement(DslModeling::ModelElement element)
		{
			return true;
		}
		
		/// <summary>
		/// Called during view fixup to configure the given child element, after it has been created.
		/// </summary>
		/// <remarks>
		/// Custom code for choosing the shapes attached to either end of a connector is called from here.
		/// </remarks>
		protected override void OnChildConfiguring(DslDiagrams::ShapeElement child, bool createdDuringViewFixup)
		{
			DslDiagrams::NodeShape sourceShape;
			DslDiagrams::NodeShape targetShape;
			DslDiagrams::BinaryLinkShape connector = child as DslDiagrams::BinaryLinkShape;
			if(connector == null)
			{
				base.OnChildConfiguring(child, createdDuringViewFixup);
				return;
			}
			this.GetSourceAndTargetForConnector(connector, out sourceShape, out targetShape);
			
			global::System.Diagnostics.Debug.Assert(sourceShape != null && targetShape != null, "Unable to find source and target shapes for connector.");
			connector.Connect(sourceShape, targetShape);
		}
		
		/// <summary>
		/// helper method to find the shapes for either end of a connector, including calling the user's custom code
		/// </summary>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal void GetSourceAndTargetForConnector(DslDiagrams::BinaryLinkShape connector, out DslDiagrams::NodeShape sourceShape, out DslDiagrams::NodeShape targetShape)
		{
			sourceShape = null;
			targetShape = null;
			
			if (sourceShape == null || targetShape == null)
			{
				DslDiagrams::NodeShape[] endShapes = GetEndShapesForConnector(connector);
				if(sourceShape == null)
				{
					sourceShape = endShapes[0];
				}
				if(targetShape == null)
				{
					targetShape = endShapes[1];
				}
			}
		}
		
		/// <summary>
		/// Helper method to find shapes for either end of a connector by looking for shapes associated with either end of the relationship mapped to the connector.
		/// </summary>
		private DslDiagrams::NodeShape[] GetEndShapesForConnector(DslDiagrams::BinaryLinkShape connector)
		{
			DslModeling::ElementLink link = connector.ModelElement as DslModeling::ElementLink;
			DslDiagrams::NodeShape sourceShape = null, targetShape = null;
			if (link != null)
			{
				global::System.Collections.ObjectModel.ReadOnlyCollection<DslModeling::ModelElement> linkedElements = link.LinkedElements;
				if (linkedElements.Count == 2)
				{
					DslDiagrams::Diagram currentDiagram = this.Diagram;
					DslModeling::LinkedElementCollection<DslDiagrams::PresentationElement> presentationElements = DslDiagrams::PresentationViewsSubject.GetPresentation(linkedElements[0]);
					foreach (DslDiagrams::PresentationElement presentationElement in presentationElements)
					{
						DslDiagrams::NodeShape shape = presentationElement as DslDiagrams::NodeShape;
						if (shape != null && shape.Diagram == currentDiagram)
						{
							sourceShape = shape;
							break;
						}
					}
					
					presentationElements = DslDiagrams::PresentationViewsSubject.GetPresentation(linkedElements[1]);
					foreach (DslDiagrams::PresentationElement presentationElement in presentationElements)
					{
						DslDiagrams::NodeShape shape = presentationElement as DslDiagrams::NodeShape;
						if (shape != null && shape.Diagram == currentDiagram)
						{
							targetShape = shape;
							break;
						}
					}
		
				}
			}
			
			return new DslDiagrams::NodeShape[] { sourceShape, targetShape };
		}
		
		/// <summary>
		/// Creates a new shape for the given model element as part of view fixup
		/// </summary>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Generated code.")]
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Generated code.")]
		protected override DslDiagrams::ShapeElement CreateChildShape(DslModeling::ModelElement element)
		{
			if(element is global::Bb.ApplicationCooperationViewPoint.ModelElement)
			{
				global::Bb.ApplicationCooperationViewPoint.CooperationShape newShape = new global::Bb.ApplicationCooperationViewPoint.CooperationShape(this.Partition);
				if(newShape != null) newShape.Size = newShape.DefaultSize; // set default shape size
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.SubElement)
			{
				global::Bb.ApplicationCooperationViewPoint.CooperationSubShape newShape = new global::Bb.ApplicationCooperationViewPoint.CooperationSubShape(this.Partition);
				if(newShape != null) newShape.Size = newShape.DefaultSize; // set default shape size
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.ConceptElement)
			{
				global::Bb.ApplicationCooperationViewPoint.ConceptElementShape newShape = new global::Bb.ApplicationCooperationViewPoint.ConceptElementShape(this.Partition);
				if(newShape != null) newShape.Size = newShape.DefaultSize; // set default shape size
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.ConceptSubElement)
			{
				global::Bb.ApplicationCooperationViewPoint.ConceptSubElementShape newShape = new global::Bb.ApplicationCooperationViewPoint.ConceptSubElementShape(this.Partition);
				if(newShape != null) newShape.Size = newShape.DefaultSize; // set default shape size
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.Concept)
			{
				global::Bb.ApplicationCooperationViewPoint.ConceptShape newShape = new global::Bb.ApplicationCooperationViewPoint.ConceptShape(this.Partition);
				if(newShape != null) newShape.Size = newShape.DefaultSize; // set default shape size
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.Relationship)
			{
				global::Bb.ApplicationCooperationViewPoint.RelationshipShape newShape = new global::Bb.ApplicationCooperationViewPoint.RelationshipShape(this.Partition);
				if(newShape != null) newShape.Size = newShape.DefaultSize; // set default shape size
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.RelationshipReferencesRightModelElement)
			{
				global::Bb.ApplicationCooperationViewPoint.TargetConnector newShape = new global::Bb.ApplicationCooperationViewPoint.TargetConnector(this.Partition);
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.RelationshipReferencesRightConcept)
			{
				global::Bb.ApplicationCooperationViewPoint.TargetConnector newShape = new global::Bb.ApplicationCooperationViewPoint.TargetConnector(this.Partition);
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.RelationshipReferencesRightConceptElement)
			{
				global::Bb.ApplicationCooperationViewPoint.TargetConnector newShape = new global::Bb.ApplicationCooperationViewPoint.TargetConnector(this.Partition);
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.RelationshipReferencesRightConceptSubElement)
			{
				global::Bb.ApplicationCooperationViewPoint.TargetConnector newShape = new global::Bb.ApplicationCooperationViewPoint.TargetConnector(this.Partition);
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.ModelElementReferencesRightRelationships)
			{
				global::Bb.ApplicationCooperationViewPoint.OriginConnector newShape = new global::Bb.ApplicationCooperationViewPoint.OriginConnector(this.Partition);
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.SubElementReferencesRightRelationships)
			{
				global::Bb.ApplicationCooperationViewPoint.OriginConnector newShape = new global::Bb.ApplicationCooperationViewPoint.OriginConnector(this.Partition);
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.ConceptReferencesRightRelationships)
			{
				global::Bb.ApplicationCooperationViewPoint.OriginConnector newShape = new global::Bb.ApplicationCooperationViewPoint.OriginConnector(this.Partition);
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.ConceptElementReferencesRightRelationships)
			{
				global::Bb.ApplicationCooperationViewPoint.OriginConnector newShape = new global::Bb.ApplicationCooperationViewPoint.OriginConnector(this.Partition);
				return newShape;
			}
			if(element is global::Bb.ApplicationCooperationViewPoint.ConceptSubElementReferencesRightRelationships)
			{
				global::Bb.ApplicationCooperationViewPoint.OriginConnector newShape = new global::Bb.ApplicationCooperationViewPoint.OriginConnector(this.Partition);
				return newShape;
			}
			return base.CreateChildShape(element);
		}
		#endregion
		#region Decorator mapping
		/// <summary>
		/// Initialize shape decorator mappings.  This is done here rather than in individual shapes because decorator maps
		/// are defined per diagram type rather than per shape type.
		/// </summary>
		protected override void InitializeShapeFields(global::System.Collections.Generic.IList<DslDiagrams::ShapeField> shapeFields)
		{
			base.InitializeShapeFields(shapeFields);
			global::Bb.ApplicationCooperationViewPoint.CooperationShape.DecoratorsInitialized += CooperationShapeDecoratorMap.OnDecoratorsInitialized;
			global::Bb.ApplicationCooperationViewPoint.CooperationSubShape.DecoratorsInitialized += CooperationSubShapeDecoratorMap.OnDecoratorsInitialized;
			global::Bb.ApplicationCooperationViewPoint.ConceptSubElementShape.DecoratorsInitialized += ConceptSubElementShapeDecoratorMap.OnDecoratorsInitialized;
			global::Bb.ApplicationCooperationViewPoint.ConceptShape.DecoratorsInitialized += ConceptShapeDecoratorMap.OnDecoratorsInitialized;
			global::Bb.ApplicationCooperationViewPoint.RelationshipShape.DecoratorsInitialized += RelationshipShapeDecoratorMap.OnDecoratorsInitialized;
		}
		
		/// <summary>
		/// Class containing decorator path traversal methods for CooperationShape.
		/// </summary>
		internal static partial class CooperationShapeDecoratorMap
		{
			/// <summary>
			/// Event handler called when decorator initialization is complete for CooperationShape.  Adds decorator mappings for this shape or connector.
			/// </summary>
			public static void OnDecoratorsInitialized(object sender, global::System.EventArgs e)
			{
				DslDiagrams::ShapeElement shape = (DslDiagrams::ShapeElement)sender;
				DslDiagrams::AssociatedPropertyInfo propertyInfo;
				
				propertyInfo = new DslDiagrams::AssociatedPropertyInfo(global::Bb.ApplicationCooperationViewPoint.ModelElement.NameDomainPropertyId);
				DslDiagrams::ShapeElement.FindDecorator(shape.Decorators, "NameDecorator").AssociateValueWith(shape.Store, propertyInfo);
				
				propertyInfo = new DslDiagrams::AssociatedPropertyInfo(global::Bb.ApplicationCooperationViewPoint.ModelElement.TypeDomainPropertyId);
				DslDiagrams::ShapeElement.FindDecorator(shape.Decorators, "TypeDecorator").AssociateValueWith(shape.Store, propertyInfo);
			}
		}
		
		/// <summary>
		/// Class containing decorator path traversal methods for CooperationSubShape.
		/// </summary>
		internal static partial class CooperationSubShapeDecoratorMap
		{
			/// <summary>
			/// Event handler called when decorator initialization is complete for CooperationSubShape.  Adds decorator mappings for this shape or connector.
			/// </summary>
			public static void OnDecoratorsInitialized(object sender, global::System.EventArgs e)
			{
				DslDiagrams::ShapeElement shape = (DslDiagrams::ShapeElement)sender;
				DslDiagrams::AssociatedPropertyInfo propertyInfo;
				
				propertyInfo = new DslDiagrams::AssociatedPropertyInfo(global::Bb.ApplicationCooperationViewPoint.SubElement.TypeDomainPropertyId);
				DslDiagrams::ShapeElement.FindDecorator(shape.Decorators, "TypeDecorator").AssociateValueWith(shape.Store, propertyInfo);
				
				propertyInfo = new DslDiagrams::AssociatedPropertyInfo(global::Bb.ApplicationCooperationViewPoint.SubElement.NameDomainPropertyId);
				DslDiagrams::ShapeElement.FindDecorator(shape.Decorators, "NameDecorator").AssociateValueWith(shape.Store, propertyInfo);
			}
		}
		
		/// <summary>
		/// Class containing decorator path traversal methods for ConceptSubElementShape.
		/// </summary>
		internal static partial class ConceptSubElementShapeDecoratorMap
		{
			/// <summary>
			/// Event handler called when decorator initialization is complete for ConceptSubElementShape.  Adds decorator mappings for this shape or connector.
			/// </summary>
			public static void OnDecoratorsInitialized(object sender, global::System.EventArgs e)
			{
				DslDiagrams::ShapeElement shape = (DslDiagrams::ShapeElement)sender;
				DslDiagrams::AssociatedPropertyInfo propertyInfo;
				
				propertyInfo = new DslDiagrams::AssociatedPropertyInfo(global::Bb.ApplicationCooperationViewPoint.ConceptSubElement.NameDomainPropertyId);
				DslDiagrams::ShapeElement.FindDecorator(shape.Decorators, "NameDecorator").AssociateValueWith(shape.Store, propertyInfo);
				
				propertyInfo = new DslDiagrams::AssociatedPropertyInfo(global::Bb.ApplicationCooperationViewPoint.ConceptSubElement.TypeDomainPropertyId);
				DslDiagrams::ShapeElement.FindDecorator(shape.Decorators, "TypeDecorator").AssociateValueWith(shape.Store, propertyInfo);
			}
		}
		
		/// <summary>
		/// Class containing decorator path traversal methods for ConceptShape.
		/// </summary>
		internal static partial class ConceptShapeDecoratorMap
		{
			/// <summary>
			/// Event handler called when decorator initialization is complete for ConceptShape.  Adds decorator mappings for this shape or connector.
			/// </summary>
			public static void OnDecoratorsInitialized(object sender, global::System.EventArgs e)
			{
				DslDiagrams::ShapeElement shape = (DslDiagrams::ShapeElement)sender;
				DslDiagrams::AssociatedPropertyInfo propertyInfo;
				
				propertyInfo = new DslDiagrams::AssociatedPropertyInfo(global::Bb.ApplicationCooperationViewPoint.Concept.NameDomainPropertyId);
				DslDiagrams::ShapeElement.FindDecorator(shape.Decorators, "NameDecorator").AssociateValueWith(shape.Store, propertyInfo);
				
				propertyInfo = new DslDiagrams::AssociatedPropertyInfo(global::Bb.ApplicationCooperationViewPoint.Concept.TypeDomainPropertyId);
				DslDiagrams::ShapeElement.FindDecorator(shape.Decorators, "TypeDecorator").AssociateValueWith(shape.Store, propertyInfo);
			}
		}
		
		/// <summary>
		/// Class containing decorator path traversal methods for RelationshipShape.
		/// </summary>
		internal static partial class RelationshipShapeDecoratorMap
		{
			/// <summary>
			/// Event handler called when decorator initialization is complete for RelationshipShape.  Adds decorator mappings for this shape or connector.
			/// </summary>
			public static void OnDecoratorsInitialized(object sender, global::System.EventArgs e)
			{
				DslDiagrams::ShapeElement shape = (DslDiagrams::ShapeElement)sender;
				DslDiagrams::AssociatedPropertyInfo propertyInfo;
				
				propertyInfo = new DslDiagrams::AssociatedPropertyInfo(global::Bb.ApplicationCooperationViewPoint.Relationship.NameDomainPropertyId);
				DslDiagrams::ShapeElement.FindDecorator(shape.Decorators, "NameDecorator").AssociateValueWith(shape.Store, propertyInfo);
				
				propertyInfo = new DslDiagrams::AssociatedPropertyInfo(global::Bb.ApplicationCooperationViewPoint.Relationship.NameDomainPropertyId);
				DslDiagrams::ShapeElement.FindDecorator(shape.Decorators, "NameDecorator").AssociateValueWith(shape.Store, propertyInfo);
				
				propertyInfo = new DslDiagrams::AssociatedPropertyInfo(global::Bb.ApplicationCooperationViewPoint.Relationship.LabelDomainPropertyId);
				DslDiagrams::ShapeElement.FindDecorator(shape.Decorators, "LabelDecorator").AssociateValueWith(shape.Store, propertyInfo);
			}
		}
		
		#endregion
		#region Constructors, domain class Id
	
		/// <summary>
		/// CooperationViewPointDiagram domain class Id.
		/// </summary>
		public static readonly new global::System.Guid DomainClassId = new global::System.Guid(0xca3f1951, 0x3a94, 0x4564, 0xa8, 0x05, 0x02, 0xe3, 0xbf, 0xc9, 0x1b, 0x8b);
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="partition">Partition where new element is to be created.</param>
		/// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		protected CooperationViewPointDiagramBase(DslModeling::Partition partition, DslModeling::PropertyAssignment[] propertyAssignments)
			: base(partition, propertyAssignments)
		{
		}
		#endregion
	}
	/// <summary>
	/// DomainClass CooperationViewPointDiagram
	/// Description for
	/// Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDiagram
	/// </summary>
	[global::System.CLSCompliant(true)]
			
	public partial class CooperationViewPointDiagram : CooperationViewPointDiagramBase
	{
		#region Constructors
		// Constructors were not generated for this class because it had HasCustomConstructor
		// set to true. Please provide the constructors below in a partial class.
		///// <summary>
		///// Constructor
		///// </summary>
		///// <param name="store">Store where new element is to be created.</param>
		///// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		//public CooperationViewPointDiagram(DslModeling::Store store, params DslModeling::PropertyAssignment[] propertyAssignments)
		//	: this(store != null ? store.DefaultPartitionForClass(DomainClassId) : null, propertyAssignments)
		//{
		//}
		//
		///// <summary>
		///// Constructor
		///// </summary>
		///// <param name="partition">Partition where new element is to be created.</param>
		///// <param name="propertyAssignments">List of domain property id/value pairs to set once the element is created.</param>
		//public CooperationViewPointDiagram(DslModeling::Partition partition, params DslModeling::PropertyAssignment[] propertyAssignments)
		//	: base(partition, propertyAssignments)
		//{
		//}
		#endregion
	}
}
namespace Bb.ApplicationCooperationViewPoint
{
	
		/// <summary>
		/// Double derived implementation for the rule that initiates view fixup when an element that has an associated shape is added to the model.
		/// This now enables the DSL author to everride the SkipFixUp() method 
		/// </summary>
		internal partial class FixUpDiagramBase : DslModeling::AddRule
		{
			protected virtual bool SkipFixup(DslModeling::ModelElement childElement)
			{
				return childElement.IsDeleted;
			}
		}
	
		/// <summary>
		/// Rule that initiates view fixup when an element that has an associated shape is added to the model. 
		/// </summary>
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.ModelElement), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddShapeParentExistRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.SubElement), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddShapeParentExistRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.ConceptElement), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddShapeParentExistRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.ConceptSubElement), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddShapeParentExistRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.Concept), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddShapeParentExistRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.Relationship), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddShapeParentExistRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.RelationshipReferencesRightModelElement), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.RelationshipReferencesRightConcept), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.RelationshipReferencesRightConceptElement), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.RelationshipReferencesRightConceptSubElement), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.ModelElementReferencesRightRelationships), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.SubElementReferencesRightRelationships), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.ConceptReferencesRightRelationships), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.ConceptElementReferencesRightRelationships), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.ConceptSubElementReferencesRightRelationships), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		internal sealed partial class FixUpDiagram : FixUpDiagramBase
		{
			[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
			public override void ElementAdded(DslModeling::ElementAddedEventArgs e)
			{
				if(e == null) throw new global::System.ArgumentNullException("e");
			
				DslModeling::ModelElement childElement = e.ModelElement;
				if (this.SkipFixup(childElement))
					return;
				DslModeling::ModelElement parentElement;
				if(childElement is DslModeling::ElementLink)
				{
					parentElement = GetParentForRelationship((DslModeling::ElementLink)childElement);
				} else
				if(childElement is global::Bb.ApplicationCooperationViewPoint.ModelElement)
				{
					parentElement = GetParentForModelElement((global::Bb.ApplicationCooperationViewPoint.ModelElement)childElement);
				} else
				if(childElement is global::Bb.ApplicationCooperationViewPoint.SubElement)
				{
					parentElement = GetParentForSubElement((global::Bb.ApplicationCooperationViewPoint.SubElement)childElement);
				} else
				if(childElement is global::Bb.ApplicationCooperationViewPoint.ConceptElement)
				{
					parentElement = GetParentForConceptElement((global::Bb.ApplicationCooperationViewPoint.ConceptElement)childElement);
				} else
				if(childElement is global::Bb.ApplicationCooperationViewPoint.ConceptSubElement)
				{
					parentElement = GetParentForConceptSubElement((global::Bb.ApplicationCooperationViewPoint.ConceptSubElement)childElement);
				} else
				if(childElement is global::Bb.ApplicationCooperationViewPoint.Concept)
				{
					parentElement = GetParentForConcept((global::Bb.ApplicationCooperationViewPoint.Concept)childElement);
				} else
				if(childElement is global::Bb.ApplicationCooperationViewPoint.Relationship)
				{
					parentElement = GetParentForRelationship((global::Bb.ApplicationCooperationViewPoint.Relationship)childElement);
				} else
				{
					parentElement = null;
				}
				
				if(parentElement != null)
				{
					DslDiagrams::Diagram.FixUpDiagram(parentElement, childElement);
				}
			}
			public static global::Bb.ApplicationCooperationViewPoint.Model GetParentForModelElement( global::Bb.ApplicationCooperationViewPoint.ModelElement root )
			{
				// Segments 0 and 1
				global::Bb.ApplicationCooperationViewPoint.Model result = root.Model;
				if ( result == null ) return null;
				return result;
			}
			public static global::Bb.ApplicationCooperationViewPoint.Model GetParentForSubElement( global::Bb.ApplicationCooperationViewPoint.SubElement root )
			{
				// Segments 0 and 1
				global::Bb.ApplicationCooperationViewPoint.ModelElement root2 = root.Parent;
				if ( root2 == null ) return null;
				// Segments 2 and 3
				global::Bb.ApplicationCooperationViewPoint.Model result = root2.Model;
				if ( result == null ) return null;
				return result;
			}
			public static global::Bb.ApplicationCooperationViewPoint.Model GetParentForConceptElement( global::Bb.ApplicationCooperationViewPoint.ConceptElement root )
			{
				// Segments 0 and 1
				global::Bb.ApplicationCooperationViewPoint.Concept root2 = root.Parent;
				if ( root2 == null ) return null;
				// Segments 2 and 3
				global::Bb.ApplicationCooperationViewPoint.Model result = root2.Model;
				if ( result == null ) return null;
				return result;
			}
			public static global::Bb.ApplicationCooperationViewPoint.Model GetParentForConceptSubElement( global::Bb.ApplicationCooperationViewPoint.ConceptSubElement root )
			{
				// Segments 0 and 1
				global::Bb.ApplicationCooperationViewPoint.ConceptElement root2 = root.Parent;
				if ( root2 == null ) return null;
				// Segments 2 and 3
				global::Bb.ApplicationCooperationViewPoint.Concept root4 = root2.Parent;
				if ( root4 == null ) return null;
				// Segments 4 and 5
				global::Bb.ApplicationCooperationViewPoint.Model result = root4.Model;
				if ( result == null ) return null;
				return result;
			}
			public static global::Bb.ApplicationCooperationViewPoint.Model GetParentForConcept( global::Bb.ApplicationCooperationViewPoint.Concept root )
			{
				// Segments 0 and 1
				global::Bb.ApplicationCooperationViewPoint.Model result = root.Model;
				if ( result == null ) return null;
				return result;
			}
			public static global::Bb.ApplicationCooperationViewPoint.Model GetParentForRelationship( global::Bb.ApplicationCooperationViewPoint.Relationship root )
			{
				// Segments 0 and 1
				global::Bb.ApplicationCooperationViewPoint.Model result = root.Model;
				if ( result == null ) return null;
				return result;
			}
			private static DslModeling::ModelElement GetParentForRelationship(DslModeling::ElementLink elementLink)
			{
				global::System.Collections.ObjectModel.ReadOnlyCollection<DslModeling::ModelElement> linkedElements = elementLink.LinkedElements;
	
				if (linkedElements.Count == 2)
				{
					DslDiagrams::ShapeElement sourceShape = linkedElements[0] as DslDiagrams::ShapeElement;
					DslDiagrams::ShapeElement targetShape = linkedElements[1] as DslDiagrams::ShapeElement;
	
					if(sourceShape == null)
					{
						DslModeling::LinkedElementCollection<DslDiagrams::PresentationElement> presentationElements = DslDiagrams::PresentationViewsSubject.GetPresentation(linkedElements[0]);
						foreach (DslDiagrams::PresentationElement presentationElement in presentationElements)
						{
							DslDiagrams::ShapeElement shape = presentationElement as DslDiagrams::ShapeElement;
							if (shape != null)
							{
								sourceShape = shape;
								break;
							}
						}
					}
					
					if(targetShape == null)
					{
						DslModeling::LinkedElementCollection<DslDiagrams::PresentationElement> presentationElements = DslDiagrams::PresentationViewsSubject.GetPresentation(linkedElements[1]);
						foreach (DslDiagrams::PresentationElement presentationElement in presentationElements)
						{
							DslDiagrams::ShapeElement shape = presentationElement as DslDiagrams::ShapeElement;
							if (shape != null)
							{
								targetShape = shape;
								break;
							}
						}
					}
					
					if(sourceShape == null || targetShape == null)
					{
						global::System.Diagnostics.Debug.Fail("Unable to find source and/or target shape for view fixup.");
						return null;
					}
	
					DslDiagrams::ShapeElement sourceParent = sourceShape.ParentShape;
					DslDiagrams::ShapeElement targetParent = targetShape.ParentShape;
	
					while (sourceParent != targetParent && sourceParent != null)
					{
						DslDiagrams::ShapeElement curParent = targetParent;
						while (sourceParent != curParent && curParent != null)
						{
							curParent = curParent.ParentShape;
						}
	
						if(sourceParent == curParent)
						{
							break;
						}
						else
						{
							sourceParent = sourceParent.ParentShape;
						}
					}
	
					while (sourceParent != null)
					{
						// ensure that the parent can parent connectors (i.e., a diagram or a swimlane).
						if(sourceParent is DslDiagrams::Diagram || sourceParent is DslDiagrams::SwimlaneShape)
						{
							break;
						}
						else
						{
							sourceParent = sourceParent.ParentShape;
						}
					}
	
					global::System.Diagnostics.Debug.Assert(sourceParent != null && sourceParent.ModelElement != null, "Unable to find common parent for view fixup.");
					return sourceParent.ModelElement;
				}
	
				return null;
			}
		}
		
	
		/// <summary>
		/// Reroute a connector when the role players of its underlying relationship change
		/// </summary>
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.RelationshipReferencesRightModelElement), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.RelationshipReferencesRightConcept), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.RelationshipReferencesRightConceptElement), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.RelationshipReferencesRightConceptSubElement), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.ModelElementReferencesRightRelationships), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.SubElementReferencesRightRelationships), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.ConceptReferencesRightRelationships), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.ConceptElementReferencesRightRelationships), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		[DslModeling::RuleOn(typeof(global::Bb.ApplicationCooperationViewPoint.ConceptSubElementReferencesRightRelationships), FireTime = DslModeling::TimeToFire.TopLevelCommit, Priority = DslDiagrams::DiagramFixupConstants.AddConnectionRulePriority, InitiallyDisabled=true)]
		internal sealed class ConnectorRolePlayerChanged : DslModeling::RolePlayerChangeRule
		{
			/// <summary>
			/// Reroute a connector when the role players of its underlying relationship change
			/// </summary>
			public override void RolePlayerChanged(DslModeling::RolePlayerChangedEventArgs e)
			{
				if (e == null) throw new global::System.ArgumentNullException("e");
	
				global::System.Collections.ObjectModel.ReadOnlyCollection<DslDiagrams::PresentationViewsSubject> connectorLinks = DslDiagrams::PresentationViewsSubject.GetLinksToPresentation(e.ElementLink);
				foreach (DslDiagrams::PresentationViewsSubject connectorLink in connectorLinks)
				{
					// Fix up any binary link shapes attached to the element link.
					DslDiagrams::BinaryLinkShape linkShape = connectorLink.Presentation as DslDiagrams::BinaryLinkShape;
					if (linkShape != null)
					{
						global::Bb.ApplicationCooperationViewPoint.CooperationViewPointDiagram diagram = linkShape.Diagram as global::Bb.ApplicationCooperationViewPoint.CooperationViewPointDiagram;
						if (diagram != null)
						{
							if (e.NewRolePlayer != null)
							{
								DslDiagrams::NodeShape fromShape;
								DslDiagrams::NodeShape toShape;
								diagram.GetSourceAndTargetForConnector(linkShape, out fromShape, out toShape);
								if (fromShape != null && toShape != null)
								{
									if (!object.Equals(fromShape, linkShape.FromShape))
									{
										linkShape.FromShape = fromShape;
									}
									if (!object.Equals(linkShape.ToShape, toShape))
									{
										linkShape.ToShape = toShape;
									}
								}
								else
								{
									// delete the connector if we cannot find an appropriate target shape.
									linkShape.Delete();
								}
							}
							else
							{
								// delete the connector if the new role player is null.
								linkShape.Delete();
							}
						}
					}
				}
			}
		}
	}
