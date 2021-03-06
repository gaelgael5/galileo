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
namespace Bb.ApplicationCooperationViewPoint
{
	/// <summary>
	/// DomainModel ApplicationCooperationViewPointDomainModel
	/// Provide a tool for edit cooperation viewpoint for galileo
	/// </summary>
	[DslDesign::DisplayNameResource("Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDomainModel.DisplayName", typeof(global::Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDomainModel), "Bb.ApplicationCooperationViewPoint.GeneratedCode.DomainModelResx")]
	[DslDesign::DescriptionResource("Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDomainModel.Description", typeof(global::Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDomainModel), "Bb.ApplicationCooperationViewPoint.GeneratedCode.DomainModelResx")]
	[global::System.CLSCompliant(true)]
	[DslModeling::DependsOnDomainModel(typeof(global::Microsoft.VisualStudio.Modeling.CoreDomainModel))]
	[DslModeling::DependsOnDomainModel(typeof(global::Microsoft.VisualStudio.Modeling.Diagrams.CoreDesignSurfaceDomainModel))]
	[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Generated code.")]
	[DslModeling::DomainObjectId("3ec5ff8c-b52e-4242-a4f8-799b97bfaa4c")]
	public partial class ApplicationCooperationViewPointDomainModel : DslModeling::DomainModel
	{
		#region Constructor, domain model Id
	
		/// <summary>
		/// ApplicationCooperationViewPointDomainModel domain model Id.
		/// </summary>
		public static readonly global::System.Guid DomainModelId = new global::System.Guid(0x3ec5ff8c, 0xb52e, 0x4242, 0xa4, 0xf8, 0x79, 0x9b, 0x97, 0xbf, 0xaa, 0x4c);
	
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="store">Store containing the domain model.</param>
		public ApplicationCooperationViewPointDomainModel(DslModeling::Store store)
			: base(store, DomainModelId)
		{
			// Call the partial method to allow any required serialization setup to be done.
			this.InitializeSerialization(store);		
		}
		
	
		///<Summary>
		/// Defines a partial method that will be called from the constructor to
		/// allow any necessary serialization setup to be done.
		///</Summary>
		///<remarks>
		/// For a DSL created with the DSL Designer wizard, an implementation of this 
		/// method will be generated in the GeneratedCode\SerializationHelper.cs class.
		///</remarks>
		partial void InitializeSerialization(DslModeling::Store store);
	
	
		#endregion
		#region Domain model reflection
			
		/// <summary>
		/// Gets the list of generated domain model types (classes, rules, relationships).
		/// </summary>
		/// <returns>List of types.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Generated code.")]	
		protected sealed override global::System.Type[] GetGeneratedDomainModelTypes()
		{
			return new global::System.Type[]
			{
				typeof(Model),
				typeof(ModelElement),
				typeof(SubElement),
				typeof(Concept),
				typeof(ConceptElement),
				typeof(ConceptSubElement),
				typeof(Relationship),
				typeof(ModelHasElements),
				typeof(ModelElementHasChildren),
				typeof(ModelHasConcepts),
				typeof(ConceptHasChildren),
				typeof(ConceptElementHasChildren),
				typeof(ModelHasRelationships),
				typeof(SubElementReferencesRightRelationships),
				typeof(ConceptReferencesRightRelationships),
				typeof(ConceptElementReferencesRightRelationships),
				typeof(ConceptSubElementReferencesRightRelationships),
				typeof(ModelElementReferencesRightRelationships),
				typeof(RelationshipReferencesRightModelElement),
				typeof(RelationshipReferencesRightSubElement),
				typeof(RelationshipReferencesRightConcept),
				typeof(RelationshipReferencesRightConceptElement),
				typeof(RelationshipReferencesRightConceptSubElement),
				typeof(CooperationViewPointDiagram),
				typeof(TargetConnector),
				typeof(OriginConnector),
				typeof(CooperationShape),
				typeof(CooperationSubShape),
				typeof(ConceptShape),
				typeof(ConceptElementShape),
				typeof(ConceptSubElementShape),
				typeof(RelationshipShape),
				typeof(global::Bb.ApplicationCooperationViewPoint.FixUpDiagram),
				typeof(global::Bb.ApplicationCooperationViewPoint.ConnectorRolePlayerChanged),
			};
		}
		/// <summary>
		/// Gets the list of generated domain properties.
		/// </summary>
		/// <returns>List of property data.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Generated code.")]	
		protected sealed override DomainMemberInfo[] GetGeneratedDomainProperties()
		{
			return new DomainMemberInfo[]
			{
				new DomainMemberInfo(typeof(Model), "Name", Model.NameDomainPropertyId, typeof(Model.NamePropertyHandler)),
				new DomainMemberInfo(typeof(Model), "Target", Model.TargetDomainPropertyId, typeof(Model.TargetPropertyHandler)),
				new DomainMemberInfo(typeof(Model), "ViewpointType", Model.ViewpointTypeDomainPropertyId, typeof(Model.ViewpointTypePropertyHandler)),
				new DomainMemberInfo(typeof(ModelElement), "ReferenceSource", ModelElement.ReferenceSourceDomainPropertyId, typeof(ModelElement.ReferenceSourcePropertyHandler)),
				new DomainMemberInfo(typeof(ModelElement), "Type", ModelElement.TypeDomainPropertyId, typeof(ModelElement.TypePropertyHandler)),
				new DomainMemberInfo(typeof(ModelElement), "Name", ModelElement.NameDomainPropertyId, typeof(ModelElement.NamePropertyHandler)),
				new DomainMemberInfo(typeof(ModelElement), "ShowMenu", ModelElement.ShowMenuDomainPropertyId, typeof(ModelElement.ShowMenuPropertyHandler)),
				new DomainMemberInfo(typeof(SubElement), "ReferenceSource", SubElement.ReferenceSourceDomainPropertyId, typeof(SubElement.ReferenceSourcePropertyHandler)),
				new DomainMemberInfo(typeof(SubElement), "Type", SubElement.TypeDomainPropertyId, typeof(SubElement.TypePropertyHandler)),
				new DomainMemberInfo(typeof(SubElement), "Name", SubElement.NameDomainPropertyId, typeof(SubElement.NamePropertyHandler)),
				new DomainMemberInfo(typeof(Concept), "ReferenceSource", Concept.ReferenceSourceDomainPropertyId, typeof(Concept.ReferenceSourcePropertyHandler)),
				new DomainMemberInfo(typeof(Concept), "Name", Concept.NameDomainPropertyId, typeof(Concept.NamePropertyHandler)),
				new DomainMemberInfo(typeof(Concept), "Type", Concept.TypeDomainPropertyId, typeof(Concept.TypePropertyHandler)),
				new DomainMemberInfo(typeof(ConceptElement), "ReferenceSource", ConceptElement.ReferenceSourceDomainPropertyId, typeof(ConceptElement.ReferenceSourcePropertyHandler)),
				new DomainMemberInfo(typeof(ConceptElement), "Type", ConceptElement.TypeDomainPropertyId, typeof(ConceptElement.TypePropertyHandler)),
				new DomainMemberInfo(typeof(ConceptElement), "Name", ConceptElement.NameDomainPropertyId, typeof(ConceptElement.NamePropertyHandler)),
				new DomainMemberInfo(typeof(ConceptSubElement), "ReferenceSource", ConceptSubElement.ReferenceSourceDomainPropertyId, typeof(ConceptSubElement.ReferenceSourcePropertyHandler)),
				new DomainMemberInfo(typeof(ConceptSubElement), "Type", ConceptSubElement.TypeDomainPropertyId, typeof(ConceptSubElement.TypePropertyHandler)),
				new DomainMemberInfo(typeof(ConceptSubElement), "Name", ConceptSubElement.NameDomainPropertyId, typeof(ConceptSubElement.NamePropertyHandler)),
				new DomainMemberInfo(typeof(Relationship), "ReferenceSource", Relationship.ReferenceSourceDomainPropertyId, typeof(Relationship.ReferenceSourcePropertyHandler)),
				new DomainMemberInfo(typeof(Relationship), "Name", Relationship.NameDomainPropertyId, typeof(Relationship.NamePropertyHandler)),
				new DomainMemberInfo(typeof(Relationship), "Label", Relationship.LabelDomainPropertyId, typeof(Relationship.LabelPropertyHandler)),
			};
		}
		/// <summary>
		/// Gets the list of generated domain roles.
		/// </summary>
		/// <returns>List of role data.</returns>
		protected sealed override DomainRolePlayerInfo[] GetGeneratedDomainRoles()
		{
			return new DomainRolePlayerInfo[]
			{
				new DomainRolePlayerInfo(typeof(ModelHasElements), "Model", ModelHasElements.ModelDomainRoleId),
				new DomainRolePlayerInfo(typeof(ModelHasElements), "Element", ModelHasElements.ElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(ModelElementHasChildren), "ModelElement", ModelElementHasChildren.ModelElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(ModelElementHasChildren), "SubElement", ModelElementHasChildren.SubElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(ModelHasConcepts), "Model", ModelHasConcepts.ModelDomainRoleId),
				new DomainRolePlayerInfo(typeof(ModelHasConcepts), "Concept", ModelHasConcepts.ConceptDomainRoleId),
				new DomainRolePlayerInfo(typeof(ConceptHasChildren), "Concept", ConceptHasChildren.ConceptDomainRoleId),
				new DomainRolePlayerInfo(typeof(ConceptHasChildren), "ConceptElement", ConceptHasChildren.ConceptElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(ConceptElementHasChildren), "ConceptElement", ConceptElementHasChildren.ConceptElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(ConceptElementHasChildren), "ConceptSubElement", ConceptElementHasChildren.ConceptSubElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(ModelHasRelationships), "Model", ModelHasRelationships.ModelDomainRoleId),
				new DomainRolePlayerInfo(typeof(ModelHasRelationships), "Relationship", ModelHasRelationships.RelationshipDomainRoleId),
				new DomainRolePlayerInfo(typeof(SubElementReferencesRightRelationships), "SubElement", SubElementReferencesRightRelationships.SubElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(SubElementReferencesRightRelationships), "Relationship", SubElementReferencesRightRelationships.RelationshipDomainRoleId),
				new DomainRolePlayerInfo(typeof(ConceptReferencesRightRelationships), "Concept", ConceptReferencesRightRelationships.ConceptDomainRoleId),
				new DomainRolePlayerInfo(typeof(ConceptReferencesRightRelationships), "Relationship", ConceptReferencesRightRelationships.RelationshipDomainRoleId),
				new DomainRolePlayerInfo(typeof(ConceptElementReferencesRightRelationships), "ConceptElement", ConceptElementReferencesRightRelationships.ConceptElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(ConceptElementReferencesRightRelationships), "Relationship", ConceptElementReferencesRightRelationships.RelationshipDomainRoleId),
				new DomainRolePlayerInfo(typeof(ConceptSubElementReferencesRightRelationships), "ConceptSubElement", ConceptSubElementReferencesRightRelationships.ConceptSubElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(ConceptSubElementReferencesRightRelationships), "Relationship", ConceptSubElementReferencesRightRelationships.RelationshipDomainRoleId),
				new DomainRolePlayerInfo(typeof(ModelElementReferencesRightRelationships), "ModelElement", ModelElementReferencesRightRelationships.ModelElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(ModelElementReferencesRightRelationships), "Relationship", ModelElementReferencesRightRelationships.RelationshipDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationshipReferencesRightModelElement), "Relationship", RelationshipReferencesRightModelElement.RelationshipDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationshipReferencesRightModelElement), "ModelElement", RelationshipReferencesRightModelElement.ModelElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationshipReferencesRightSubElement), "Relationship", RelationshipReferencesRightSubElement.RelationshipDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationshipReferencesRightSubElement), "SubElement", RelationshipReferencesRightSubElement.SubElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationshipReferencesRightConcept), "Relationship", RelationshipReferencesRightConcept.RelationshipDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationshipReferencesRightConcept), "Concept", RelationshipReferencesRightConcept.ConceptDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationshipReferencesRightConceptElement), "Relationship", RelationshipReferencesRightConceptElement.RelationshipDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationshipReferencesRightConceptElement), "ConceptElement", RelationshipReferencesRightConceptElement.ConceptElementDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationshipReferencesRightConceptSubElement), "Relationship", RelationshipReferencesRightConceptSubElement.RelationshipDomainRoleId),
				new DomainRolePlayerInfo(typeof(RelationshipReferencesRightConceptSubElement), "ConceptSubElement", RelationshipReferencesRightConceptSubElement.ConceptSubElementDomainRoleId),
			};
		}
		#endregion
		#region Factory methods
		private static global::System.Collections.Generic.Dictionary<global::System.Type, int> createElementMap;
	
		/// <summary>
		/// Creates an element of specified type.
		/// </summary>
		/// <param name="partition">Partition where element is to be created.</param>
		/// <param name="elementType">Element type which belongs to this domain model.</param>
		/// <param name="propertyAssignments">New element property assignments.</param>
		/// <returns>Created element.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Generated code.")]	
		public sealed override DslModeling::ModelElement CreateElement(DslModeling::Partition partition, global::System.Type elementType, DslModeling::PropertyAssignment[] propertyAssignments)
		{
			if (elementType == null) throw new global::System.ArgumentNullException("elementType");
	
			if (createElementMap == null)
			{
				createElementMap = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(16);
				createElementMap.Add(typeof(Model), 0);
				createElementMap.Add(typeof(ModelElement), 1);
				createElementMap.Add(typeof(SubElement), 2);
				createElementMap.Add(typeof(Concept), 3);
				createElementMap.Add(typeof(ConceptElement), 4);
				createElementMap.Add(typeof(ConceptSubElement), 5);
				createElementMap.Add(typeof(Relationship), 6);
				createElementMap.Add(typeof(CooperationViewPointDiagram), 7);
				createElementMap.Add(typeof(TargetConnector), 8);
				createElementMap.Add(typeof(OriginConnector), 9);
				createElementMap.Add(typeof(CooperationShape), 10);
				createElementMap.Add(typeof(CooperationSubShape), 11);
				createElementMap.Add(typeof(ConceptShape), 12);
				createElementMap.Add(typeof(ConceptElementShape), 13);
				createElementMap.Add(typeof(ConceptSubElementShape), 14);
				createElementMap.Add(typeof(RelationshipShape), 15);
			}
			int index;
			if (!createElementMap.TryGetValue(elementType, out index))
			{
				// construct exception error message		
				string exceptionError = string.Format(
								global::System.Globalization.CultureInfo.CurrentCulture,
								global::Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDomainModel.SingletonResourceManager.GetString("UnrecognizedElementType"),
								elementType.Name);
				throw new global::System.ArgumentException(exceptionError, "elementType");
			}
			switch (index)
			{
				// A constructor was not generated for Model because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 0: return new Model(partition, propertyAssignments);
				case 1: return new ModelElement(partition, propertyAssignments);
				// A constructor was not generated for SubElement because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 2: return new SubElement(partition, propertyAssignments);
				// A constructor was not generated for Concept because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 3: return new Concept(partition, propertyAssignments);
				// A constructor was not generated for ConceptElement because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 4: return new ConceptElement(partition, propertyAssignments);
				// A constructor was not generated for ConceptSubElement because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 5: return new ConceptSubElement(partition, propertyAssignments);
				// A constructor was not generated for Relationship because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 6: return new Relationship(partition, propertyAssignments);
				// A constructor was not generated for CooperationViewPointDiagram because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 7: return new CooperationViewPointDiagram(partition, propertyAssignments);
				case 8: return new TargetConnector(partition, propertyAssignments);
				case 9: return new OriginConnector(partition, propertyAssignments);
				// A constructor was not generated for CooperationShape because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 10: return new CooperationShape(partition, propertyAssignments);
				// A constructor was not generated for CooperationSubShape because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 11: return new CooperationSubShape(partition, propertyAssignments);
				// A constructor was not generated for ConceptShape because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 12: return new ConceptShape(partition, propertyAssignments);
				// A constructor was not generated for ConceptElementShape because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 13: return new ConceptElementShape(partition, propertyAssignments);
				// A constructor was not generated for ConceptSubElementShape because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 14: return new ConceptSubElementShape(partition, propertyAssignments);
				// A constructor was not generated for RelationshipShape because it had HasCustomConstructor
				// set to true. Please provide the constructor below.
				case 15: return new RelationshipShape(partition, propertyAssignments);
				default: return null;
			}
		}
	
		private static global::System.Collections.Generic.Dictionary<global::System.Type, int> createElementLinkMap;
	
		/// <summary>
		/// Creates an element link of specified type.
		/// </summary>
		/// <param name="partition">Partition where element is to be created.</param>
		/// <param name="elementLinkType">Element link type which belongs to this domain model.</param>
		/// <param name="roleAssignments">List of relationship role assignments for the new link.</param>
		/// <param name="propertyAssignments">New element property assignments.</param>
		/// <returns>Created element link.</returns>
		[global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		public sealed override DslModeling::ElementLink CreateElementLink(DslModeling::Partition partition, global::System.Type elementLinkType, DslModeling::RoleAssignment[] roleAssignments, DslModeling::PropertyAssignment[] propertyAssignments)
		{
			if (elementLinkType == null) throw new global::System.ArgumentNullException("elementLinkType");
			if (roleAssignments == null) throw new global::System.ArgumentNullException("roleAssignments");
	
			if (createElementLinkMap == null)
			{
				createElementLinkMap = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(16);
				createElementLinkMap.Add(typeof(ModelHasElements), 0);
				createElementLinkMap.Add(typeof(ModelElementHasChildren), 1);
				createElementLinkMap.Add(typeof(ModelHasConcepts), 2);
				createElementLinkMap.Add(typeof(ConceptHasChildren), 3);
				createElementLinkMap.Add(typeof(ConceptElementHasChildren), 4);
				createElementLinkMap.Add(typeof(ModelHasRelationships), 5);
				createElementLinkMap.Add(typeof(SubElementReferencesRightRelationships), 6);
				createElementLinkMap.Add(typeof(ConceptReferencesRightRelationships), 7);
				createElementLinkMap.Add(typeof(ConceptElementReferencesRightRelationships), 8);
				createElementLinkMap.Add(typeof(ConceptSubElementReferencesRightRelationships), 9);
				createElementLinkMap.Add(typeof(ModelElementReferencesRightRelationships), 10);
				createElementLinkMap.Add(typeof(RelationshipReferencesRightModelElement), 11);
				createElementLinkMap.Add(typeof(RelationshipReferencesRightSubElement), 12);
				createElementLinkMap.Add(typeof(RelationshipReferencesRightConcept), 13);
				createElementLinkMap.Add(typeof(RelationshipReferencesRightConceptElement), 14);
				createElementLinkMap.Add(typeof(RelationshipReferencesRightConceptSubElement), 15);
			}
			int index;
			if (!createElementLinkMap.TryGetValue(elementLinkType, out index))
			{
				// construct exception error message
				string exceptionError = string.Format(
								global::System.Globalization.CultureInfo.CurrentCulture,
								global::Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDomainModel.SingletonResourceManager.GetString("UnrecognizedElementLinkType"),
								elementLinkType.Name);
				throw new global::System.ArgumentException(exceptionError, "elementLinkType");
			
			}
			switch (index)
			{
				case 0: return new ModelHasElements(partition, roleAssignments, propertyAssignments);
				case 1: return new ModelElementHasChildren(partition, roleAssignments, propertyAssignments);
				case 2: return new ModelHasConcepts(partition, roleAssignments, propertyAssignments);
				case 3: return new ConceptHasChildren(partition, roleAssignments, propertyAssignments);
				case 4: return new ConceptElementHasChildren(partition, roleAssignments, propertyAssignments);
				case 5: return new ModelHasRelationships(partition, roleAssignments, propertyAssignments);
				case 6: return new SubElementReferencesRightRelationships(partition, roleAssignments, propertyAssignments);
				case 7: return new ConceptReferencesRightRelationships(partition, roleAssignments, propertyAssignments);
				case 8: return new ConceptElementReferencesRightRelationships(partition, roleAssignments, propertyAssignments);
				case 9: return new ConceptSubElementReferencesRightRelationships(partition, roleAssignments, propertyAssignments);
				case 10: return new ModelElementReferencesRightRelationships(partition, roleAssignments, propertyAssignments);
				case 11: return new RelationshipReferencesRightModelElement(partition, roleAssignments, propertyAssignments);
				case 12: return new RelationshipReferencesRightSubElement(partition, roleAssignments, propertyAssignments);
				case 13: return new RelationshipReferencesRightConcept(partition, roleAssignments, propertyAssignments);
				case 14: return new RelationshipReferencesRightConceptElement(partition, roleAssignments, propertyAssignments);
				case 15: return new RelationshipReferencesRightConceptSubElement(partition, roleAssignments, propertyAssignments);
				default: return null;
			}
		}
		#endregion
		#region Resource manager
		
		private static global::System.Resources.ResourceManager resourceManager;
		
		/// <summary>
		/// The base name of this model's resources.
		/// </summary>
		public const string ResourceBaseName = "Bb.ApplicationCooperationViewPoint.GeneratedCode.DomainModelResx";
		
		/// <summary>
		/// Gets the DomainModel's ResourceManager. If the ResourceManager does not already exist, then it is created.
		/// </summary>
		public override global::System.Resources.ResourceManager ResourceManager
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				return ApplicationCooperationViewPointDomainModel.SingletonResourceManager;
			}
		}
	
		/// <summary>
		/// Gets the Singleton ResourceManager for this domain model.
		/// </summary>
		public static global::System.Resources.ResourceManager SingletonResourceManager
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			get
			{
				if (ApplicationCooperationViewPointDomainModel.resourceManager == null)
				{
					ApplicationCooperationViewPointDomainModel.resourceManager = new global::System.Resources.ResourceManager(ResourceBaseName, typeof(ApplicationCooperationViewPointDomainModel).Assembly);
				}
				return ApplicationCooperationViewPointDomainModel.resourceManager;
			}
		}
		#endregion
		#region Copy/Remove closures
		/// <summary>
		/// CopyClosure cache
		/// </summary>
		private static DslModeling::IElementVisitorFilter copyClosure;
		/// <summary>
		/// DeleteClosure cache
		/// </summary>
		private static DslModeling::IElementVisitorFilter removeClosure;
		/// <summary>
		/// Returns an IElementVisitorFilter that corresponds to the ClosureType.
		/// </summary>
		/// <param name="type">closure type</param>
		/// <param name="rootElements">collection of root elements</param>
		/// <returns>IElementVisitorFilter or null</returns>
		public override DslModeling::IElementVisitorFilter GetClosureFilter(DslModeling::ClosureType type, global::System.Collections.Generic.ICollection<DslModeling::ModelElement> rootElements)
		{
			switch (type)
			{
				case DslModeling::ClosureType.CopyClosure:
					return ApplicationCooperationViewPointDomainModel.CopyClosure;
				case DslModeling::ClosureType.DeleteClosure:
					return ApplicationCooperationViewPointDomainModel.DeleteClosure;
			}
			return base.GetClosureFilter(type, rootElements);
		}
		/// <summary>
		/// CopyClosure cache
		/// </summary>
		private static DslModeling::IElementVisitorFilter CopyClosure
		{
			get
			{
				// Incorporate all of the closures from the models we extend
				if (ApplicationCooperationViewPointDomainModel.copyClosure == null)
				{
					DslModeling::ChainingElementVisitorFilter copyFilter = new DslModeling::ChainingElementVisitorFilter();
					copyFilter.AddFilter(new ApplicationCooperationViewPointCopyClosure());
					copyFilter.AddFilter(new DslModeling::CoreCopyClosure());
					copyFilter.AddFilter(new DslDiagrams::CoreDesignSurfaceCopyClosure());
					
					ApplicationCooperationViewPointDomainModel.copyClosure = copyFilter;
				}
				return ApplicationCooperationViewPointDomainModel.copyClosure;
			}
		}
		/// <summary>
		/// DeleteClosure cache
		/// </summary>
		private static DslModeling::IElementVisitorFilter DeleteClosure
		{
			get
			{
				// Incorporate all of the closures from the models we extend
				if (ApplicationCooperationViewPointDomainModel.removeClosure == null)
				{
					DslModeling::ChainingElementVisitorFilter removeFilter = new DslModeling::ChainingElementVisitorFilter();
					removeFilter.AddFilter(new ApplicationCooperationViewPointDeleteClosure());
					removeFilter.AddFilter(new DslModeling::CoreDeleteClosure());
					removeFilter.AddFilter(new DslDiagrams::CoreDesignSurfaceDeleteClosure());
		
					ApplicationCooperationViewPointDomainModel.removeClosure = removeFilter;
				}
				return ApplicationCooperationViewPointDomainModel.removeClosure;
			}
		}
		#endregion
		#region Diagram rule helpers
		/// <summary>
		/// Enables rules in this domain model related to diagram fixup for the given store.
		/// If diagram data will be loaded into the store, this method should be called first to ensure
		/// that the diagram behaves properly.
		/// </summary>
		public static void EnableDiagramRules(DslModeling::Store store)
		{
			if(store == null) throw new global::System.ArgumentNullException("store");
			
			DslModeling::RuleManager ruleManager = store.RuleManager;
			ruleManager.EnableRule(typeof(global::Bb.ApplicationCooperationViewPoint.FixUpDiagram));
			ruleManager.EnableRule(typeof(global::Bb.ApplicationCooperationViewPoint.ConnectorRolePlayerChanged));
		}
		
		/// <summary>
		/// Disables rules in this domain model related to diagram fixup for the given store.
		/// </summary>
		public static void DisableDiagramRules(DslModeling::Store store)
		{
			if(store == null) throw new global::System.ArgumentNullException("store");
			
			DslModeling::RuleManager ruleManager = store.RuleManager;
			ruleManager.DisableRule(typeof(global::Bb.ApplicationCooperationViewPoint.FixUpDiagram));
			ruleManager.DisableRule(typeof(global::Bb.ApplicationCooperationViewPoint.ConnectorRolePlayerChanged));
		}
		#endregion
	}
		
	#region Copy/Remove closure classes
	/// <summary>
	/// Remove closure visitor filter
	/// </summary>
	[global::System.CLSCompliant(true)]
	public partial class ApplicationCooperationViewPointDeleteClosure : ApplicationCooperationViewPointDeleteClosureBase, DslModeling::IElementVisitorFilter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ApplicationCooperationViewPointDeleteClosure() : base()
		{
		}
	}
	
	/// <summary>
	/// Base class for remove closure visitor filter
	/// </summary>
	[global::System.CLSCompliant(true)]
	public partial class ApplicationCooperationViewPointDeleteClosureBase : DslModeling::IElementVisitorFilter
	{
		/// <summary>
		/// DomainRoles
		/// </summary>
		private global::System.Collections.Specialized.HybridDictionary domainRoles;
		/// <summary>
		/// Constructor
		/// </summary>
		public ApplicationCooperationViewPointDeleteClosureBase()
		{
			#region Initialize DomainData Table
			DomainRoles.Add(global::Bb.ApplicationCooperationViewPoint.ModelHasElements.ElementDomainRoleId, true);
			DomainRoles.Add(global::Bb.ApplicationCooperationViewPoint.ModelElementHasChildren.SubElementDomainRoleId, true);
			DomainRoles.Add(global::Bb.ApplicationCooperationViewPoint.ModelHasConcepts.ConceptDomainRoleId, true);
			DomainRoles.Add(global::Bb.ApplicationCooperationViewPoint.ConceptHasChildren.ConceptElementDomainRoleId, true);
			DomainRoles.Add(global::Bb.ApplicationCooperationViewPoint.ConceptElementHasChildren.ConceptSubElementDomainRoleId, true);
			DomainRoles.Add(global::Bb.ApplicationCooperationViewPoint.ModelHasRelationships.RelationshipDomainRoleId, true);
			#endregion
		}
		/// <summary>
		/// Called to ask the filter if a particular relationship from a source element should be included in the traversal
		/// </summary>
		/// <param name="walker">ElementWalker that is traversing the model</param>
		/// <param name="sourceElement">Model Element playing the source role</param>
		/// <param name="sourceRoleInfo">DomainRoleInfo of the role that the source element is playing in the relationship</param>
		/// <param name="domainRelationshipInfo">DomainRelationshipInfo for the ElementLink in question</param>
		/// <param name="targetRelationship">Relationship in question</param>
		/// <returns>Yes if the relationship should be traversed</returns>
		public virtual DslModeling::VisitorFilterResult ShouldVisitRelationship(DslModeling::ElementWalker walker, DslModeling::ModelElement sourceElement, DslModeling::DomainRoleInfo sourceRoleInfo, DslModeling::DomainRelationshipInfo domainRelationshipInfo, DslModeling::ElementLink targetRelationship)
		{
			return DslModeling::VisitorFilterResult.Yes;
		}
		/// <summary>
		/// Called to ask the filter if a particular role player should be Visited during traversal
		/// </summary>
		/// <param name="walker">ElementWalker that is traversing the model</param>
		/// <param name="sourceElement">Model Element playing the source role</param>
		/// <param name="elementLink">Element Link that forms the relationship to the role player in question</param>
		/// <param name="targetDomainRole">DomainRoleInfo of the target role</param>
		/// <param name="targetRolePlayer">Model Element that plays the target role in the relationship</param>
		/// <returns></returns>
		public virtual DslModeling::VisitorFilterResult ShouldVisitRolePlayer(DslModeling::ElementWalker walker, DslModeling::ModelElement sourceElement, DslModeling::ElementLink elementLink, DslModeling::DomainRoleInfo targetDomainRole, DslModeling::ModelElement targetRolePlayer)
		{
			if (targetDomainRole == null) throw new global::System.ArgumentNullException("targetDomainRole");
			return this.DomainRoles.Contains(targetDomainRole.Id) ? DslModeling::VisitorFilterResult.Yes : DslModeling::VisitorFilterResult.DoNotCare;
		}
		/// <summary>
		/// DomainRoles
		/// </summary>
		private global::System.Collections.Specialized.HybridDictionary DomainRoles
		{
			get
			{
				if (this.domainRoles == null)
				{
					this.domainRoles = new global::System.Collections.Specialized.HybridDictionary();
				}
				return this.domainRoles;
			}
		}
	
	}
	/// <summary>
	/// Copy closure visitor filter
	/// </summary>
	[global::System.CLSCompliant(true)]
	public partial class ApplicationCooperationViewPointCopyClosure : ApplicationCooperationViewPointCopyClosureBase, DslModeling::IElementVisitorFilter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ApplicationCooperationViewPointCopyClosure() : base()
		{
		}
	}
	/// <summary>
	/// Base class for copy closure visitor filter
	/// </summary>
	[global::System.CLSCompliant(true)]
	public partial class ApplicationCooperationViewPointCopyClosureBase : DslModeling::CopyClosureFilter, DslModeling::IElementVisitorFilter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ApplicationCooperationViewPointCopyClosureBase():base()
		{
		}
	}
	#endregion
		
}

