<?xml version="1.0" encoding="utf-8"?>
<Dsl xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="3ec5ff8c-b52e-4242-a4f8-799b97bfaa4c" Description="Description for Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPoint" Name="ApplicationCooperationViewPoint" DisplayName="ApplicationCooperationViewPoint" Namespace="Bb.ApplicationCooperationViewPoint" ProductName="Galileo Application Cooperation Viewpoint" CompanyName="Black.Beard" PackageGuid="6113886b-339d-4d50-9d9a-05cec44810af" PackageNamespace="Bb.ApplicationCooperationViewPoint" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Classes>
    <DomainClass Id="8be8ba93-37bd-430f-bab0-798cd8f43104" Description="The root in which all other elements are embedded. Appears as a diagram." Name="CooperationModel" DisplayName="Cooperation Model" Namespace="Bb.ApplicationCooperationViewPoint">
      <Properties>
        <DomainProperty Id="048aa5a8-8536-4484-836c-7792c8e92d57" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationModel.Name" Name="Name" DisplayName="Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b963122b-9189-4127-ba26-23296661bba4" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationModel.Target" Name="Target" DisplayName="Target">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a44053d4-9afc-469a-a7b3-83d4e00fa16f" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationModel.Type" Name="Type" DisplayName="Type">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Notes>Creates an embedding link when an element is dropped onto a model. </Notes>
          <Index>
            <DomainClassMoniker Name="CooperationElement" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>CooperationModelHasElements.Elements</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="403a7c63-3a36-4881-b0d5-0eaf0dcad50e" Description="Elements embedded in the model. Appear as boxes on the diagram." Name="CooperationElement" DisplayName="Cooperation Element" Namespace="Bb.ApplicationCooperationViewPoint">
      <Properties>
        <DomainProperty Id="97022f17-f153-4a51-902e-c1886c9a533a" Description="Description for Bb.ApplicationCooperationViewPoint.ExampleElement.Name" Name="SourceReference" DisplayName="Source Reference" DefaultValue="" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="CooperationSubElement" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>CooperationElementHasCooperationSubElement.CooperationSubElement</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="e39ae37f-7927-4be7-a99c-94d0deb846f7" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationSubElement" Name="CooperationSubElement" DisplayName="Cooperation Sub Element" Namespace="Bb.ApplicationCooperationViewPoint">
      <Properties>
        <DomainProperty Id="b3968e18-782b-4443-9994-f232739217b9" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationSubElement.Reference Source" Name="ReferenceSource" DisplayName="Reference Source">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
  </Classes>
  <Relationships>
    <DomainRelationship Id="a56654a5-67b4-4c46-9617-d126e33ee736" Description="Embedding relationship between the Model and Elements" Name="CooperationModelHasElements" DisplayName="Cooperation Model Has Elements" Namespace="Bb.ApplicationCooperationViewPoint" IsEmbedding="true">
      <Source>
        <DomainRole Id="8e1caa2f-c5e8-439c-9909-d33e8bc13c8f" Description="" Name="CooperationModel" DisplayName="Cooperation Model" PropertyName="Elements" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Elements">
          <RolePlayer>
            <DomainClassMoniker Name="CooperationModel" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="31224c1c-de69-4216-acdc-a8a347ed4bf2" Description="" Name="Element" DisplayName="Element" PropertyName="CooperationModel" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Cooperation Model">
          <RolePlayer>
            <DomainClassMoniker Name="CooperationElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="b42ccc47-8c9e-4875-8b5d-4f129b6d2a59" Description="Reference relationship between Elements." Name="CooperationElementReferencesTargets" DisplayName="Cooperation Element References Targets" Namespace="Bb.ApplicationCooperationViewPoint">
      <Source>
        <DomainRole Id="4da54a79-f251-47e5-bb31-0964f98feaa3" Description="Description for Bb.ApplicationCooperationViewPoint.ExampleRelationship.Target" Name="Source" DisplayName="Source" PropertyName="Targets" PropertyDisplayName="Targets">
          <RolePlayer>
            <DomainClassMoniker Name="CooperationElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="4bf9ba8e-1912-4d37-8325-1aa50ddb63fb" Description="Description for Bb.ApplicationCooperationViewPoint.ExampleRelationship.Source" Name="Target" DisplayName="Target" PropertyName="Sources" PropertyDisplayName="Sources">
          <RolePlayer>
            <DomainClassMoniker Name="CooperationElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="065c38c4-7f53-46b7-87ab-6ebfcd243ab6" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationElementHasCooperationSubElement" Name="CooperationElementHasCooperationSubElement" DisplayName="Cooperation Element Has Cooperation Sub Element" Namespace="Bb.ApplicationCooperationViewPoint" IsEmbedding="true">
      <Source>
        <DomainRole Id="798fe20c-d3f4-4796-ac52-542298851ce8" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationElementHasCooperationSubElement.Parent" Name="Parent" DisplayName="Parent" PropertyName="CooperationSubElement" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Cooperation Sub Element">
          <RolePlayer>
            <DomainClassMoniker Name="CooperationElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="2d373726-da2b-45f8-b7d9-3ce7eb9f8f95" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationElementHasCooperationSubElement.CooperationSubElement" Name="CooperationSubElement" DisplayName="Cooperation Sub Element" PropertyName="Parent" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Parent">
          <RolePlayer>
            <DomainClassMoniker Name="CooperationSubElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="0a79f2c6-f1b8-4956-9cfd-da6cb3efb1c5" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationSubElementReferencesTargetCooperationSubElement" Name="CooperationSubElementReferencesTargetCooperationSubElement" DisplayName="Cooperation Sub Element References Target Cooperation Sub Element" Namespace="Bb.ApplicationCooperationViewPoint">
      <Source>
        <DomainRole Id="8fb85cb6-ff65-49cb-b1f9-1b097eed4a7c" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationSubElementReferencesTargetCooperationSubElement.SourceCooperationSubElement" Name="SourceCooperationSubElement" DisplayName="Source Cooperation Sub Element" PropertyName="TargetCooperationSubElement" PropertyDisplayName="Target Cooperation Sub Element">
          <RolePlayer>
            <DomainClassMoniker Name="CooperationSubElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="918b929d-6958-4610-a4b9-c02e94d73542" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationSubElementReferencesTargetCooperationSubElement.TargetCooperationSubElement" Name="TargetCooperationSubElement" DisplayName="Target Cooperation Sub Element" PropertyName="SourceCooperationSubElement" PropertyDisplayName="Source Cooperation Sub Element">
          <RolePlayer>
            <DomainClassMoniker Name="CooperationSubElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
  </Relationships>
  <Types>
    <ExternalType Name="DateTime" Namespace="System" />
    <ExternalType Name="String" Namespace="System" />
    <ExternalType Name="Int16" Namespace="System" />
    <ExternalType Name="Int32" Namespace="System" />
    <ExternalType Name="Int64" Namespace="System" />
    <ExternalType Name="UInt16" Namespace="System" />
    <ExternalType Name="UInt32" Namespace="System" />
    <ExternalType Name="UInt64" Namespace="System" />
    <ExternalType Name="SByte" Namespace="System" />
    <ExternalType Name="Byte" Namespace="System" />
    <ExternalType Name="Double" Namespace="System" />
    <ExternalType Name="Single" Namespace="System" />
    <ExternalType Name="Guid" Namespace="System" />
    <ExternalType Name="Boolean" Namespace="System" />
    <ExternalType Name="Char" Namespace="System" />
  </Types>
  <Shapes>
    <GeometryShape Id="8eada818-bdb1-4ddc-9c2e-d04318497508" Description="Shape used to represent ExampleElements on a Diagram." Name="CooperationShape" DisplayName="Cooperation Shape" Namespace="Bb.ApplicationCooperationViewPoint" FixedTooltipText="Cooperation Shape" FillColor="242, 239, 229" OutlineColor="113, 111, 110" InitialWidth="2" InitialHeight="0.75" OutlineThickness="0.01" Geometry="Rectangle">
      <Notes>The shape has a text decorator used to display the Name property of the mapped ExampleElement.</Notes>
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" />
      </ShapeHasDecorators>
    </GeometryShape>
    <GeometryShape Id="b8457ae5-9194-431c-853e-4c35bd690549" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationSubShape" Name="CooperationSubShape" DisplayName="Cooperation Sub Shape" Namespace="Bb.ApplicationCooperationViewPoint" FixedTooltipText="Cooperation Sub Shape" InitialHeight="1" Geometry="Rectangle">
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="TextName" DisplayName="Text Name" DefaultText="TextName" />
      </ShapeHasDecorators>
    </GeometryShape>
  </Shapes>
  <Connectors>
    <Connector Id="3481ef8b-8f10-4afd-9861-a31145fd30c2" Description="Connector between the ExampleShapes. Represents ExampleRelationships on the Diagram." Name="ExampleConnector" DisplayName="Example Connector" Namespace="Bb.ApplicationCooperationViewPoint" FixedTooltipText="Example Connector" Color="113, 111, 110" TargetEndStyle="EmptyArrow" Thickness="0.01" />
    <Connector Id="8af21c04-aa64-42da-beff-afc70166c219" Description="Description de Bb.ApplicationCooperationViewPoint.Connector1" Name="Connector1" DisplayName="Connector1" Namespace="Bb.ApplicationCooperationViewPoint" FixedTooltipText="Connector1" />
  </Connectors>
  <XmlSerializationBehavior Name="ApplicationCooperationViewPointSerializationBehavior" Namespace="Bb.ApplicationCooperationViewPoint">
    <ClassData>
      <XmlClassData TypeName="CooperationModel" MonikerAttributeName="" SerializeId="true" MonikerElementName="cooperationModelMoniker" ElementName="cooperationModel" MonikerTypeName="CooperationModelMoniker">
        <DomainClassMoniker Name="CooperationModel" />
        <ElementData>
          <XmlRelationshipData RoleElementName="elements">
            <DomainRelationshipMoniker Name="CooperationModelHasElements" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="CooperationModel/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="target">
            <DomainPropertyMoniker Name="CooperationModel/Target" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="CooperationModel/Type" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="CooperationElement" MonikerAttributeName="sourceReference" SerializeId="true" MonikerElementName="cooperationElementMoniker" ElementName="cooperationElement" MonikerTypeName="CooperationElementMoniker">
        <DomainClassMoniker Name="CooperationElement" />
        <ElementData>
          <XmlPropertyData XmlName="sourceReference" IsMonikerKey="true">
            <DomainPropertyMoniker Name="CooperationElement/SourceReference" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="targets">
            <DomainRelationshipMoniker Name="CooperationElementReferencesTargets" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="cooperationSubElement">
            <DomainRelationshipMoniker Name="CooperationElementHasCooperationSubElement" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="CooperationModelHasElements" MonikerAttributeName="" SerializeId="true" MonikerElementName="cooperationModelHasElementsMoniker" ElementName="cooperationModelHasElements" MonikerTypeName="CooperationModelHasElementsMoniker">
        <DomainRelationshipMoniker Name="CooperationModelHasElements" />
      </XmlClassData>
      <XmlClassData TypeName="CooperationElementReferencesTargets" MonikerAttributeName="" SerializeId="true" MonikerElementName="cooperationElementReferencesTargetsMoniker" ElementName="cooperationElementReferencesTargets" MonikerTypeName="CooperationElementReferencesTargetsMoniker">
        <DomainRelationshipMoniker Name="CooperationElementReferencesTargets" />
      </XmlClassData>
      <XmlClassData TypeName="CooperationShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="cooperationShapeMoniker" ElementName="cooperationShape" MonikerTypeName="CooperationShapeMoniker">
        <GeometryShapeMoniker Name="CooperationShape" />
      </XmlClassData>
      <XmlClassData TypeName="ExampleConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="exampleConnectorMoniker" ElementName="exampleConnector" MonikerTypeName="ExampleConnectorMoniker">
        <ConnectorMoniker Name="ExampleConnector" />
      </XmlClassData>
      <XmlClassData TypeName="ApplicationCooperationViewPointDiagram" MonikerAttributeName="" SerializeId="true" MonikerElementName="applicationCooperationViewPointDiagramMoniker" ElementName="applicationCooperationViewPointDiagram" MonikerTypeName="ApplicationCooperationViewPointDiagramMoniker">
        <DiagramMoniker Name="ApplicationCooperationViewPointDiagram" />
      </XmlClassData>
      <XmlClassData TypeName="CooperationSubShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="cooperationSubShapeMoniker" ElementName="cooperationSubShape" MonikerTypeName="CooperationSubShapeMoniker">
        <GeometryShapeMoniker Name="CooperationSubShape" />
      </XmlClassData>
      <XmlClassData TypeName="CooperationSubElement" MonikerAttributeName="" SerializeId="true" MonikerElementName="cooperationSubElementMoniker" ElementName="cooperationSubElement" MonikerTypeName="CooperationSubElementMoniker">
        <DomainClassMoniker Name="CooperationSubElement" />
        <ElementData>
          <XmlPropertyData XmlName="referenceSource">
            <DomainPropertyMoniker Name="CooperationSubElement/ReferenceSource" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="targetCooperationSubElement">
            <DomainRelationshipMoniker Name="CooperationSubElementReferencesTargetCooperationSubElement" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="CooperationElementHasCooperationSubElement" MonikerAttributeName="" SerializeId="true" MonikerElementName="cooperationElementHasCooperationSubElementMoniker" ElementName="cooperationElementHasCooperationSubElement" MonikerTypeName="CooperationElementHasCooperationSubElementMoniker">
        <DomainRelationshipMoniker Name="CooperationElementHasCooperationSubElement" />
      </XmlClassData>
      <XmlClassData TypeName="CooperationSubElementReferencesTargetCooperationSubElement" MonikerAttributeName="" SerializeId="true" MonikerElementName="cooperationSubElementReferencesTargetCooperationSubElementMoniker" ElementName="cooperationSubElementReferencesTargetCooperationSubElement" MonikerTypeName="CooperationSubElementReferencesTargetCooperationSubElementMoniker">
        <DomainRelationshipMoniker Name="CooperationSubElementReferencesTargetCooperationSubElement" />
      </XmlClassData>
      <XmlClassData TypeName="Connector1" MonikerAttributeName="" SerializeId="true" MonikerElementName="connector1Moniker" ElementName="connector1" MonikerTypeName="Connector1Moniker">
        <ConnectorMoniker Name="Connector1" />
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="ApplicationCooperationViewPointExplorer" />
  <ConnectionBuilders>
    <ConnectionBuilder Name="CooperationElementReferencesTargetsBuilder">
      <Notes>Provides for the creation of an ExampleRelationship by pointing at two ExampleElements.</Notes>
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="CooperationElementReferencesTargets" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="CooperationElement" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="CooperationElement" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="CooperationSubElementReferencesTargetCooperationSubElementBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="CooperationSubElementReferencesTargetCooperationSubElement" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="CooperationSubElement" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="CooperationSubElement" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
  </ConnectionBuilders>
  <Diagram Id="ca3f1951-3a94-4564-a805-02e3bfc91b8b" Description="Description for Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDiagram" Name="ApplicationCooperationViewPointDiagram" DisplayName="Minimal Language Diagram" Namespace="Bb.ApplicationCooperationViewPoint">
    <Class>
      <DomainClassMoniker Name="CooperationModel" />
    </Class>
    <ShapeMaps>
      <ShapeMap>
        <DomainClassMoniker Name="CooperationElement" />
        <ParentElementPath>
          <DomainPath>CooperationModelHasElements.CooperationModel/!CooperationModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CooperationShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="CooperationElement/SourceReference" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="CooperationShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="CooperationSubElement" />
        <ParentElementPath>
          <DomainPath>CooperationElementHasCooperationSubElement.Parent/!Parent/CooperationModelHasElements.CooperationModel/!CooperationModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CooperationSubShape/TextName" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="CooperationSubElement/ReferenceSource" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="CooperationSubShape" />
      </ShapeMap>
    </ShapeMaps>
    <ConnectorMaps>
      <ConnectorMap>
        <ConnectorMoniker Name="ExampleConnector" />
        <DomainRelationshipMoniker Name="CooperationElementReferencesTargets" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="Connector1" />
        <DomainRelationshipMoniker Name="CooperationSubElementReferencesTargetCooperationSubElement" />
      </ConnectorMap>
    </ConnectorMaps>
  </Diagram>
  <Designer CopyPasteGeneration="CopyPasteOnly" FileExtension="covp" EditorGuid="d430d989-356e-4935-8012-a5020680189a">
    <RootClass>
      <DomainClassMoniker Name="CooperationModel" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="ApplicationCooperationViewPointSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="Cooperation Viewpoint">
      <ElementTool Name="ElementRoot" ToolboxIcon="resources\exampleshapetoolbitmap.bmp" Caption="Element Root" Tooltip="Create a root Element" HelpKeyword="CreateExampleClassF1Keyword">
        <DomainClassMoniker Name="CooperationElement" />
      </ElementTool>
      <ConnectionTool Name="RootElementRelationship" ToolboxIcon="resources\exampleconnectortoolbitmap.bmp" Caption="Relationship between root elements" Tooltip="Drag between ExampleElements to create an ExampleRelationship" HelpKeyword="ConnectExampleRelationF1Keyword" SourceCursorIcon="Resources\ConnectorSourceSearch.cur" TargetCursorIcon="Resources\ConnectorTargetSearch.cur">
        <ConnectionBuilderMoniker Name="ApplicationCooperationViewPoint/CooperationElementReferencesTargetsBuilder" />
      </ConnectionTool>
      <ElementTool Name="SubElement" ToolboxIcon="resources\exampleshapetoolbitmap.bmp" Caption="Sub Element" Tooltip="Sub Element" HelpKeyword="SubElement">
        <DomainClassMoniker Name="CooperationSubElement" />
      </ElementTool>
      <ConnectionTool Name="CollaborationSub" ToolboxIcon="resources\exampleconnectortoolbitmap.bmp" Caption="Relationship between sub elements" Tooltip="Collaboration Sub" HelpKeyword="CollaborationSub" SourceCursorIcon="Resources\ConnectorSourceSearch.cur" TargetCursorIcon="Resources\ConnectorTargetSearch.cur">
        <ConnectionBuilderMoniker Name="ApplicationCooperationViewPoint/CooperationSubElementReferencesTargetCooperationSubElementBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <Validation UsesMenu="false" UsesOpen="false" UsesSave="true" UsesLoad="false" />
    <DiagramMoniker Name="ApplicationCooperationViewPointDiagram" />
  </Designer>
  <Explorer ExplorerGuid="581e6349-7922-43c9-bb72-0222757290ef" Title="ApplicationCooperationViewPoint Explorer">
    <ExplorerBehaviorMoniker Name="ApplicationCooperationViewPoint/ApplicationCooperationViewPointExplorer" />
  </Explorer>
</Dsl>