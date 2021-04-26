<?xml version="1.0" encoding="utf-8"?>
<Dsl xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="3ec5ff8c-b52e-4242-a4f8-799b97bfaa4c" Description="Provide a tool for edit cooperation viewpoint for galileo" Name="ApplicationCooperationViewPoint" DisplayName="Cooperation Viewpoint for Galileo" Namespace="Bb.ApplicationCooperationViewPoint" MajorVersion="0" MinorVersion="1" ProductName="Galileo Application Cooperation Viewpoint" CompanyName="Black.Beard" PackageGuid="6113886b-339d-4d50-9d9a-05cec44810af" PackageNamespace="Bb.ApplicationCooperationViewPoint" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Classes>
    <DomainClass Id="8be8ba93-37bd-430f-bab0-798cd8f43104" Description="The root in which all other elements are embedded. Appears as a diagram." Name="Model" DisplayName="Model" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true">
      <Properties>
        <DomainProperty Id="048aa5a8-8536-4484-836c-7792c8e92d57" Description="Description de Bb.ApplicationCooperationViewPoint.Model.Name" Name="Name" DisplayName="Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b963122b-9189-4127-ba26-23296661bba4" Description="Description de Bb.ApplicationCooperationViewPoint.Model.Target" Name="Target" DisplayName="Target" DefaultValue="Current">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a44053d4-9afc-469a-a7b3-83d4e00fa16f" Description="Description de Bb.ApplicationCooperationViewPoint.Model.Viewpoint Type" Name="ViewpointType" DisplayName="Viewpoint Type">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(ListCooperationViewpointEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Notes>Creates an embedding link when an element is dropped onto a model. </Notes>
          <Index>
            <DomainClassMoniker Name="ModelElement" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ModelHasElements.Elements</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="Concept" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ModelHasConcepts.Concepts</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="Relationship" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ModelHasRelationships.Relationships</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="403a7c63-3a36-4881-b0d5-0eaf0dcad50e" Description="Elements embedded in the model. Appear as boxes on the diagram." Name="ModelElement" DisplayName="Model Element" Namespace="Bb.ApplicationCooperationViewPoint" GeneratesDoubleDerived="true">
      <Properties>
        <DomainProperty Id="97022f17-f153-4a51-902e-c1886c9a533a" Description="Description for Bb.ApplicationCooperationViewPoint.ExampleElement.Name" Name="ReferenceSource" DisplayName="Reference Source" DefaultValue="" Kind="CustomStorage" IsElementName="true">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(TreeviewCooperationViewpointEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="186ee9fa-b929-4b89-8b01-ff239890ad0d" Description="Description de Bb.ApplicationCooperationViewPoint.ModelElement.Type" Name="Type" DisplayName="Type" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8e7d1b12-3853-4cd0-943b-7c2f36e6cab7" Description="Name of the element" Name="Name" DisplayName="Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="116737ff-8853-4ef4-abd3-772e37b72256" Description="Description de Bb.ApplicationCooperationViewPoint.ModelElement.Show Menu" Name="ShowMenu" DisplayName="Show Menu" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="SubElement" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ModelElementHasChildren.Children</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="e39ae37f-7927-4be7-a99c-94d0deb846f7" Description="Description de Bb.ApplicationCooperationViewPoint.SubElement" Name="SubElement" DisplayName="Sub Element" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true">
      <Properties>
        <DomainProperty Id="b3968e18-782b-4443-9994-f232739217b9" Description="Description de Bb.ApplicationCooperationViewPoint.SubElement.Reference Source" Name="ReferenceSource" DisplayName="Reference Source" Kind="CustomStorage" IsElementName="true">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(TreeviewCooperationViewpointEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c9c98eae-11ce-4989-b012-49c64d4dcf11" Description="Description de Bb.ApplicationCooperationViewPoint.SubElement.Type" Name="Type" DisplayName="Type" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="0a2706da-320c-4408-90ff-bcc277fd49a8" Description="Description de Bb.ApplicationCooperationViewPoint.SubElement.Name" Name="Name" DisplayName="Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="eaad73c4-a652-4596-b575-39e82c2f525a" Description="Description de Bb.ApplicationCooperationViewPoint.Concept" Name="Concept" DisplayName="Concept" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true">
      <Properties>
        <DomainProperty Id="8f8c5d55-111e-49f2-9518-cce39195b847" Description="Description de Bb.ApplicationCooperationViewPoint.Concept.Reference Source" Name="ReferenceSource" DisplayName="Reference Source" Kind="CustomStorage" IsElementName="true">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(TreeviewCooperationViewpointEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9c79719a-1349-4b5c-a936-a80094c0e392" Description="Description de Bb.ApplicationCooperationViewPoint.Concept.Name" Name="Name" DisplayName="Name" DefaultValue="Parcel Domain">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ffc34da1-2c9e-406d-ad38-d9262528981f" Description="Description de Bb.ApplicationCooperationViewPoint.Concept.Type" Name="Type" DisplayName="Type" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ConceptElement" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ConceptHasChildren.Children</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="3aba6490-0175-4397-b4b4-8ba4dd8df1d1" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptElement" Name="ConceptElement" DisplayName="Concept Element" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true">
      <Properties>
        <DomainProperty Id="16627524-3c73-434a-bbee-5ef23344e62a" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptElement.Reference Source" Name="ReferenceSource" DisplayName="Reference Source" Kind="CustomStorage" IsElementName="true">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(TreeviewCooperationViewpointEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1a662591-4601-4437-b8a1-b41d55d5c360" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptElement.Type" Name="Type" DisplayName="Type" DefaultValue="Application" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="95b8ccf0-5e6a-43a9-9c9a-be38ef263e48" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptElement.Name" Name="Name" DisplayName="Name" DefaultValue="Colis 21">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="ConceptSubElement" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>ConceptElementHasChildren.Children</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="a3a13452-14ee-4fe8-adcb-fd5c2f06ff68" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptSubElement" Name="ConceptSubElement" DisplayName="Concept Sub Element" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true">
      <Properties>
        <DomainProperty Id="e4021cd7-d1f3-43a4-801b-946fbe965c4c" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptSubElement.Reference Source" Name="ReferenceSource" DisplayName="Reference Source" Kind="CustomStorage" IsElementName="true">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(TreeviewCooperationViewpointEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="121c195f-3624-48e7-b11c-711b083deeb4" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptSubElement.Type" Name="Type" DisplayName="Type" DefaultValue="Module" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9f2c7807-ebf9-4579-b146-94f6643b87e7" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptSubElement.Name" Name="Name" DisplayName="Name" DefaultValue="Monitoring">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="95bd428c-7b5b-49c7-b8f9-5f753e1817a6" Description="Description de Bb.ApplicationCooperationViewPoint.Relationship" Name="Relationship" DisplayName="Relationship" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true">
      <Properties>
        <DomainProperty Id="c12f448e-0d28-4f1f-9195-90d47aad9e31" Description="Description de Bb.ApplicationCooperationViewPoint.Relationship.Reference Source" Name="ReferenceSource" DisplayName="Reference Source" Kind="CustomStorage" IsElementName="true">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(TreeviewCooperationViewpointEditor)" />
                <AttributeParameter Value="typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1648ea49-8782-4660-9727-6dfbd03c3b81" Description="Description de Bb.ApplicationCooperationViewPoint.Relationship.Name" Name="Name" DisplayName="Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b7c9cb49-d73d-4820-93e5-471e8bf820c0" Description="Description de Bb.ApplicationCooperationViewPoint.Relationship.Type" Name="Type" DisplayName="Type" IsUIReadOnly="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
  </Classes>
  <Relationships>
    <DomainRelationship Id="a56654a5-67b4-4c46-9617-d126e33ee736" Description="Embedding relationship between the Model and Elements" Name="ModelHasElements" DisplayName="Model Has Elements" Namespace="Bb.ApplicationCooperationViewPoint" IsEmbedding="true">
      <Source>
        <DomainRole Id="8e1caa2f-c5e8-439c-9909-d33e8bc13c8f" Description="" Name="Model" DisplayName="Model" PropertyName="Elements" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Elements">
          <RolePlayer>
            <DomainClassMoniker Name="Model" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="31224c1c-de69-4216-acdc-a8a347ed4bf2" Description="" Name="Element" DisplayName="Element" PropertyName="Model" Multiplicity="ZeroOne" PropagatesDelete="true" PropertyDisplayName="Model">
          <RolePlayer>
            <DomainClassMoniker Name="ModelElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="57c665d5-ef2c-41b6-96bd-08780d51c8e4" Description="Description de Bb.ApplicationCooperationViewPoint.ModelElementHasChildren" Name="ModelElementHasChildren" DisplayName="Model Element Has Children" Namespace="Bb.ApplicationCooperationViewPoint" IsEmbedding="true">
      <Source>
        <DomainRole Id="4e27a168-343f-4cf4-8011-78bee6fb63a9" Description="Description de Bb.ApplicationCooperationViewPoint.ModelElementHasChildren.ModelElement" Name="ModelElement" DisplayName="Model Element" PropertyName="Children" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Children">
          <RolePlayer>
            <DomainClassMoniker Name="ModelElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="705ae0aa-38b5-4591-9949-6862d0c2355d" Description="Description de Bb.ApplicationCooperationViewPoint.ModelElementHasChildren.SubElement" Name="SubElement" DisplayName="Sub Element" PropertyName="Parent" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Parent">
          <RolePlayer>
            <DomainClassMoniker Name="SubElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="5b6958dd-4b35-4cc6-9384-6300f5852984" Description="Description de Bb.ApplicationCooperationViewPoint.ModelHasConcepts" Name="ModelHasConcepts" DisplayName="Model Has Concepts" Namespace="Bb.ApplicationCooperationViewPoint" IsEmbedding="true">
      <Source>
        <DomainRole Id="582de730-ec77-4e7c-a2c3-d22c651d2dd4" Description="Description de Bb.ApplicationCooperationViewPoint.ModelHasConcepts.Model" Name="Model" DisplayName="Model" PropertyName="Concepts" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Concepts">
          <RolePlayer>
            <DomainClassMoniker Name="Model" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="55648a0d-20b7-4a1c-9987-b35bf37d1a38" Description="Description de Bb.ApplicationCooperationViewPoint.ModelHasConcepts.Concept" Name="Concept" DisplayName="Concept" PropertyName="Model" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Model">
          <RolePlayer>
            <DomainClassMoniker Name="Concept" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="bedbf741-5157-42e7-b9fb-548e33779023" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptHasChildren" Name="ConceptHasChildren" DisplayName="Concept Has Children" Namespace="Bb.ApplicationCooperationViewPoint" IsEmbedding="true">
      <Source>
        <DomainRole Id="995481df-64be-4c5e-a63e-e6bea50ee718" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptHasChildren.Concept" Name="Concept" DisplayName="Concept" PropertyName="Children" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Children">
          <RolePlayer>
            <DomainClassMoniker Name="Concept" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="c11d0eca-8d13-4da9-aaaf-1454d1dcd201" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptHasChildren.ConceptElement" Name="ConceptElement" DisplayName="Concept Element" PropertyName="Parent" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Parent">
          <RolePlayer>
            <DomainClassMoniker Name="ConceptElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="5eeb81ec-92bf-487f-834b-0045d600e9f4" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptElementHasChildren" Name="ConceptElementHasChildren" DisplayName="Concept Element Has Children" Namespace="Bb.ApplicationCooperationViewPoint" IsEmbedding="true">
      <Source>
        <DomainRole Id="e504b189-80a3-4840-9153-884c243758b9" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptElementHasChildren.ConceptElement" Name="ConceptElement" DisplayName="Concept Element" PropertyName="Children" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Children">
          <RolePlayer>
            <DomainClassMoniker Name="ConceptElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="1fac3fe2-b2c6-4094-8763-c8b96e9190f0" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptElementHasChildren.ConceptSubElement" Name="ConceptSubElement" DisplayName="Concept Sub Element" PropertyName="Parent" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Parent">
          <RolePlayer>
            <DomainClassMoniker Name="ConceptSubElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="55dbe5cb-6d89-457e-8dcd-092930328e46" Description="Description de Bb.ApplicationCooperationViewPoint.ModelHasRelationships" Name="ModelHasRelationships" DisplayName="Model Has Relationships" Namespace="Bb.ApplicationCooperationViewPoint" IsEmbedding="true">
      <Source>
        <DomainRole Id="0a33673b-918f-4f58-9d91-c4907991b9d9" Description="Description de Bb.ApplicationCooperationViewPoint.ModelHasRelationships.Model" Name="Model" DisplayName="Model" PropertyName="Relationships" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Relationships">
          <RolePlayer>
            <DomainClassMoniker Name="Model" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="77111e5d-a493-4d25-87ec-676ff46d1987" Description="Description de Bb.ApplicationCooperationViewPoint.ModelHasRelationships.Relationship" Name="Relationship" DisplayName="Relationship" PropertyName="Model" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Model">
          <RolePlayer>
            <DomainClassMoniker Name="Relationship" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="820c2cb8-59a2-498f-be37-580b2899fb31" Description="Description de Bb.ApplicationCooperationViewPoint.SubElementReferencesRelationship" Name="SubElementReferencesRelationship" DisplayName="Sub Element References Relationship" Namespace="Bb.ApplicationCooperationViewPoint">
      <Source>
        <DomainRole Id="1bc0ce3d-a6f8-424e-9924-267b3646ef84" Description="Description de Bb.ApplicationCooperationViewPoint.SubElementReferencesRelationship.SubElement" Name="SubElement" DisplayName="Sub Element" PropertyName="Relationship" PropertyDisplayName="Relationship">
          <RolePlayer>
            <DomainClassMoniker Name="SubElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="a0bfb131-2213-4886-bdc3-ec22cfae3118" Description="Description de Bb.ApplicationCooperationViewPoint.SubElementReferencesRelationship.Relationship" Name="Relationship" DisplayName="Relationship" PropertyName="SubElement" PropertyDisplayName="Sub Element">
          <RolePlayer>
            <DomainClassMoniker Name="Relationship" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="929c1a72-faf3-4091-aea2-942e25cc3693" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptReferencesRelationship" Name="ConceptReferencesRelationship" DisplayName="Concept References Relationship" Namespace="Bb.ApplicationCooperationViewPoint">
      <Source>
        <DomainRole Id="33d7c8d6-bb81-4cfe-8250-0860b7b128af" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptReferencesRelationship.Concept" Name="Concept" DisplayName="Concept" PropertyName="Relationship" PropertyDisplayName="Relationship">
          <RolePlayer>
            <DomainClassMoniker Name="Concept" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="45fc7239-cc02-40f9-b912-382eca5ab0b7" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptReferencesRelationship.Relationship" Name="Relationship" DisplayName="Relationship" PropertyName="Concept" PropertyDisplayName="Concept">
          <RolePlayer>
            <DomainClassMoniker Name="Relationship" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="c8a244dc-37ba-45b6-9bd6-da13e0f09c0f" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptElementReferencesRelationship" Name="ConceptElementReferencesRelationship" DisplayName="Concept Element References Relationship" Namespace="Bb.ApplicationCooperationViewPoint">
      <Source>
        <DomainRole Id="cc4d7dac-913b-44fe-84e1-1de45fd2d5de" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptElementReferencesRelationship.ConceptElement" Name="ConceptElement" DisplayName="Concept Element" PropertyName="Relationship" PropertyDisplayName="Relationship">
          <RolePlayer>
            <DomainClassMoniker Name="ConceptElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="5ff17734-e48c-4251-a9d9-8428a5c081a9" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptElementReferencesRelationship.Relationship" Name="Relationship" DisplayName="Relationship" PropertyName="ConceptElement" PropertyDisplayName="Concept Element">
          <RolePlayer>
            <DomainClassMoniker Name="Relationship" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="5878e7a2-52ad-499b-88b3-3c17c14e3f4c" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptSubElementReferencesRelationship" Name="ConceptSubElementReferencesRelationship" DisplayName="Concept Sub Element References Relationship" Namespace="Bb.ApplicationCooperationViewPoint">
      <Source>
        <DomainRole Id="b1fc2ccf-dbf3-418b-ae07-30bd25073503" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptSubElementReferencesRelationship.ConceptSubElement" Name="ConceptSubElement" DisplayName="Concept Sub Element" PropertyName="Relationship" PropertyDisplayName="Relationship">
          <RolePlayer>
            <DomainClassMoniker Name="ConceptSubElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="9517762b-beed-48a5-ae52-a1646c940c78" Description="Description de Bb.ApplicationCooperationViewPoint.ConceptSubElementReferencesRelationship.Relationship" Name="Relationship" DisplayName="Relationship" PropertyName="ConceptSubElement" PropertyDisplayName="Concept Sub Element">
          <RolePlayer>
            <DomainClassMoniker Name="Relationship" />
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
    <GeometryShape Id="8eada818-bdb1-4ddc-9c2e-d04318497508" Description="Shape used to represent ExampleElements on a Diagram." Name="CooperationShape" DisplayName="Cooperation Shape" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true" FixedTooltipText="Cooperation Shape" OutlineColor="Red" InitialWidth="2" InitialHeight="0.75" OutlineThickness="0.01" FillGradientMode="None" HasDefaultConnectionPoints="true" Geometry="RoundedRectangle">
      <Notes>The shape has a text decorator used to display the Name property of the mapped ExampleElement.</Notes>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.1">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontStyle="Bold" FontSize="14" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="TypeDecorator" DisplayName="Type Decorator" DefaultText="TypeDecorator" FontStyle="Italic" FontSize="7" />
      </ShapeHasDecorators>
    </GeometryShape>
    <GeometryShape Id="b8457ae5-9194-431c-853e-4c35bd690549" Description="Description de Bb.ApplicationCooperationViewPoint.CooperationSubShape" Name="CooperationSubShape" DisplayName="Cooperation Sub Shape" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true" FixedTooltipText="Cooperation Sub Shape" FillColor="WhiteSmoke" InitialHeight="1" OutlineThickness="0.02" HasDefaultConnectionPoints="true" Geometry="Rectangle">
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.1">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontStyle="Bold" FontSize="14" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="TypeDecorator" DisplayName="Type Decorator" DefaultText="TypeDecorator" FontStyle="Italic" FontSize="7" />
      </ShapeHasDecorators>
    </GeometryShape>
    <GeometryShape Id="c0e7a566-72f7-42db-a7a1-75e48652eb37" Description="Shape used to represent ExampleElements on a Diagram." Name="ConceptShape" DisplayName="Concept Shape" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true" FixedTooltipText="Concept Shape" OutlineColor="Blue" InitialWidth="2" InitialHeight="0.75" OutlineDashStyle="Dash" OutlineThickness="0.01" FillGradientMode="None" HasDefaultConnectionPoints="true" Geometry="Rectangle">
      <Notes>The shape has a text decorator used to display the Name property of the mapped ExampleElement.</Notes>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.1">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontStyle="Bold" FontSize="14" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="TypeDecorator" DisplayName="Type Decorator" DefaultText="TypeDecorator" FontStyle="Italic" FontSize="7" />
      </ShapeHasDecorators>
    </GeometryShape>
    <GeometryShape Id="e56815f2-5b13-40d2-bfc2-2343d55ccf47" Description="Shape used to represent ExampleElements on a Diagram." Name="ConceptElementShape" DisplayName="Concept Element Shape" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true" FixedTooltipText="Concept Element Shape" OutlineColor="113, 111, 110" InitialWidth="2" InitialHeight="0.75" OutlineThickness="0.01" FillGradientMode="None" HasDefaultConnectionPoints="true" Geometry="RoundedRectangle">
      <Notes>The shape has a text decorator used to display the Name property of the mapped ExampleElement.</Notes>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.1">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontStyle="Bold" FontSize="14" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="TypeDecorator" DisplayName="Type Decorator" DefaultText="TypeDecorator" FontStyle="Italic" FontSize="7" />
      </ShapeHasDecorators>
    </GeometryShape>
    <GeometryShape Id="df2714b9-0858-4e7c-ad77-000b48fdb1e1" Description="Shape used to represent ExampleElements on a Diagram." Name="ConceptSubElementShape" DisplayName="Concept Sub Element Shape" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true" FixedTooltipText="Concept Sub Element Shape" OutlineColor="113, 111, 110" InitialWidth="2" InitialHeight="0.75" OutlineThickness="0.01" FillGradientMode="None" HasDefaultConnectionPoints="true" Geometry="Rectangle">
      <Notes>The shape has a text decorator used to display the Name property of the mapped ExampleElement.</Notes>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.1">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontStyle="Bold" FontSize="14" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="TypeDecorator" DisplayName="Type Decorator" DefaultText="TypeDecorator" FontStyle="Italic" FontSize="7" />
      </ShapeHasDecorators>
    </GeometryShape>
    <GeometryShape Id="4ba97606-cdb3-4522-be6a-f80ca1dc9b14" Description="Shape used to represent ExampleElements on a Diagram." Name="RelationshipShape" DisplayName="Relationship Shape" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true" FixedTooltipText="Relationship Shape" OutlineColor="113, 111, 110" InitialWidth="2" InitialHeight="0.75" OutlineThickness="0.01" FillGradientMode="None" HasDefaultConnectionPoints="true" Geometry="Rectangle">
      <Notes>The shape has a text decorator used to display the Name property of the mapped ExampleElement.</Notes>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.1">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontStyle="Bold" FontSize="14" />
      </ShapeHasDecorators>
    </GeometryShape>
  </Shapes>
  <XmlSerializationBehavior Name="ApplicationCooperationViewPointSerializationBehavior" Namespace="Bb.ApplicationCooperationViewPoint">
    <ClassData>
      <XmlClassData TypeName="Model" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelMoniker" ElementName="model" MonikerTypeName="ModelMoniker">
        <DomainClassMoniker Name="Model" />
        <ElementData>
          <XmlRelationshipData RoleElementName="elements">
            <DomainRelationshipMoniker Name="ModelHasElements" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="Model/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="target">
            <DomainPropertyMoniker Name="Model/Target" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="viewpointType">
            <DomainPropertyMoniker Name="Model/ViewpointType" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="concepts">
            <DomainRelationshipMoniker Name="ModelHasConcepts" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="relationships">
            <DomainRelationshipMoniker Name="ModelHasRelationships" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelElement" MonikerAttributeName="referenceSource" SerializeId="true" MonikerElementName="modelElementMoniker" ElementName="modelElement" MonikerTypeName="ModelElementMoniker">
        <DomainClassMoniker Name="ModelElement" />
        <ElementData>
          <XmlPropertyData XmlName="referenceSource" IsMonikerKey="true">
            <DomainPropertyMoniker Name="ModelElement/ReferenceSource" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="ModelElement/Type" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="children">
            <DomainRelationshipMoniker Name="ModelElementHasChildren" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="ModelElement/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="showMenu" Representation="Ignore">
            <DomainPropertyMoniker Name="ModelElement/ShowMenu" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelHasElements" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelHasElementsMoniker" ElementName="modelHasElements" MonikerTypeName="ModelHasElementsMoniker">
        <DomainRelationshipMoniker Name="ModelHasElements" />
      </XmlClassData>
      <XmlClassData TypeName="CooperationShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="cooperationShapeMoniker" ElementName="cooperationShape" MonikerTypeName="CooperationShapeMoniker">
        <GeometryShapeMoniker Name="CooperationShape" />
      </XmlClassData>
      <XmlClassData TypeName="CooperationViewPointDiagram" MonikerAttributeName="" SerializeId="true" MonikerElementName="cooperationViewPointDiagramMoniker" ElementName="cooperationViewPointDiagram" MonikerTypeName="CooperationViewPointDiagramMoniker">
        <DiagramMoniker Name="CooperationViewPointDiagram" />
      </XmlClassData>
      <XmlClassData TypeName="CooperationSubShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="cooperationSubShapeMoniker" ElementName="cooperationSubShape" MonikerTypeName="CooperationSubShapeMoniker">
        <GeometryShapeMoniker Name="CooperationSubShape" />
      </XmlClassData>
      <XmlClassData TypeName="SubElement" MonikerAttributeName="" SerializeId="true" MonikerElementName="subElementMoniker" ElementName="subElement" MonikerTypeName="SubElementMoniker">
        <DomainClassMoniker Name="SubElement" />
        <ElementData>
          <XmlPropertyData XmlName="referenceSource">
            <DomainPropertyMoniker Name="SubElement/ReferenceSource" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="SubElement/Type" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="SubElement/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="relationship">
            <DomainRelationshipMoniker Name="SubElementReferencesRelationship" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelElementHasChildren" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelElementHasChildrenMoniker" ElementName="modelElementHasChildren" MonikerTypeName="ModelElementHasChildrenMoniker">
        <DomainRelationshipMoniker Name="ModelElementHasChildren" />
      </XmlClassData>
      <XmlClassData TypeName="Concept" MonikerAttributeName="" SerializeId="true" MonikerElementName="conceptMoniker" ElementName="concept" MonikerTypeName="ConceptMoniker">
        <DomainClassMoniker Name="Concept" />
        <ElementData>
          <XmlPropertyData XmlName="referenceSource">
            <DomainPropertyMoniker Name="Concept/ReferenceSource" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="children">
            <DomainRelationshipMoniker Name="ConceptHasChildren" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="Concept/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="relationship">
            <DomainRelationshipMoniker Name="ConceptReferencesRelationship" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="Concept/Type" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConceptShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="conceptShapeMoniker" ElementName="conceptShape" MonikerTypeName="ConceptShapeMoniker">
        <GeometryShapeMoniker Name="ConceptShape" />
      </XmlClassData>
      <XmlClassData TypeName="ModelHasConcepts" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelHasConceptsMoniker" ElementName="modelHasConcepts" MonikerTypeName="ModelHasConceptsMoniker">
        <DomainRelationshipMoniker Name="ModelHasConcepts" />
      </XmlClassData>
      <XmlClassData TypeName="ConceptElement" MonikerAttributeName="" SerializeId="true" MonikerElementName="conceptElementMoniker" ElementName="conceptElement" MonikerTypeName="ConceptElementMoniker">
        <DomainClassMoniker Name="ConceptElement" />
        <ElementData>
          <XmlPropertyData XmlName="referenceSource">
            <DomainPropertyMoniker Name="ConceptElement/ReferenceSource" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="ConceptElement/Type" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="children">
            <DomainRelationshipMoniker Name="ConceptElementHasChildren" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="ConceptElement/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="relationship">
            <DomainRelationshipMoniker Name="ConceptElementReferencesRelationship" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConceptElementShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="conceptElementShapeMoniker" ElementName="conceptElementShape" MonikerTypeName="ConceptElementShapeMoniker">
        <GeometryShapeMoniker Name="ConceptElementShape" />
      </XmlClassData>
      <XmlClassData TypeName="ConceptHasChildren" MonikerAttributeName="" SerializeId="true" MonikerElementName="conceptHasChildrenMoniker" ElementName="conceptHasChildren" MonikerTypeName="ConceptHasChildrenMoniker">
        <DomainRelationshipMoniker Name="ConceptHasChildren" />
      </XmlClassData>
      <XmlClassData TypeName="ConceptSubElement" MonikerAttributeName="" SerializeId="true" MonikerElementName="conceptSubElementMoniker" ElementName="conceptSubElement" MonikerTypeName="ConceptSubElementMoniker">
        <DomainClassMoniker Name="ConceptSubElement" />
        <ElementData>
          <XmlPropertyData XmlName="referenceSource">
            <DomainPropertyMoniker Name="ConceptSubElement/ReferenceSource" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="ConceptSubElement/Type" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="ConceptSubElement/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="relationship">
            <DomainRelationshipMoniker Name="ConceptSubElementReferencesRelationship" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConceptElementHasChildren" MonikerAttributeName="" SerializeId="true" MonikerElementName="conceptElementHasChildrenMoniker" ElementName="conceptElementHasChildren" MonikerTypeName="ConceptElementHasChildrenMoniker">
        <DomainRelationshipMoniker Name="ConceptElementHasChildren" />
      </XmlClassData>
      <XmlClassData TypeName="ConceptSubElementShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="conceptSubElementShapeMoniker" ElementName="conceptSubElementShape" MonikerTypeName="ConceptSubElementShapeMoniker">
        <GeometryShapeMoniker Name="ConceptSubElementShape" />
      </XmlClassData>
      <XmlClassData TypeName="Relationship" MonikerAttributeName="" SerializeId="true" MonikerElementName="relationshipMoniker" ElementName="relationship" MonikerTypeName="RelationshipMoniker">
        <DomainClassMoniker Name="Relationship" />
        <ElementData>
          <XmlPropertyData XmlName="referenceSource">
            <DomainPropertyMoniker Name="Relationship/ReferenceSource" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="Relationship/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="Relationship/Type" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ModelHasRelationships" MonikerAttributeName="" SerializeId="true" MonikerElementName="modelHasRelationshipsMoniker" ElementName="modelHasRelationships" MonikerTypeName="ModelHasRelationshipsMoniker">
        <DomainRelationshipMoniker Name="ModelHasRelationships" />
      </XmlClassData>
      <XmlClassData TypeName="RelationshipShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="relationshipShapeMoniker" ElementName="relationshipShape" MonikerTypeName="RelationshipShapeMoniker">
        <GeometryShapeMoniker Name="RelationshipShape" />
      </XmlClassData>
      <XmlClassData TypeName="SubElementReferencesRelationship" MonikerAttributeName="" SerializeId="true" MonikerElementName="subElementReferencesRelationshipMoniker" ElementName="subElementReferencesRelationship" MonikerTypeName="SubElementReferencesRelationshipMoniker">
        <DomainRelationshipMoniker Name="SubElementReferencesRelationship" />
      </XmlClassData>
      <XmlClassData TypeName="ConceptReferencesRelationship" MonikerAttributeName="" SerializeId="true" MonikerElementName="conceptReferencesRelationshipMoniker" ElementName="conceptReferencesRelationship" MonikerTypeName="ConceptReferencesRelationshipMoniker">
        <DomainRelationshipMoniker Name="ConceptReferencesRelationship" />
      </XmlClassData>
      <XmlClassData TypeName="ConceptElementReferencesRelationship" MonikerAttributeName="" SerializeId="true" MonikerElementName="conceptElementReferencesRelationshipMoniker" ElementName="conceptElementReferencesRelationship" MonikerTypeName="ConceptElementReferencesRelationshipMoniker">
        <DomainRelationshipMoniker Name="ConceptElementReferencesRelationship" />
      </XmlClassData>
      <XmlClassData TypeName="ConceptSubElementReferencesRelationship" MonikerAttributeName="" SerializeId="true" MonikerElementName="conceptSubElementReferencesRelationshipMoniker" ElementName="conceptSubElementReferencesRelationship" MonikerTypeName="ConceptSubElementReferencesRelationshipMoniker">
        <DomainRelationshipMoniker Name="ConceptSubElementReferencesRelationship" />
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="ApplicationCooperationViewPointExplorer">
    <CustomNodeSettings>
      <ExplorerNodeSettings ShowsDomainClass="true">
        <Class>
          <DomainClassMoniker Name="Concept" />
        </Class>
        <PropertyDisplayed>
          <PropertyPath>
            <DomainPropertyMoniker Name="Concept/Name" />
            <DomainPath />
          </PropertyPath>
        </PropertyDisplayed>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings ShowsDomainClass="true">
        <Class>
          <DomainClassMoniker Name="ConceptElement" />
        </Class>
        <PropertyDisplayed>
          <PropertyPath>
            <DomainPropertyMoniker Name="ConceptElement/Name" />
            <DomainPath />
          </PropertyPath>
        </PropertyDisplayed>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings ShowsDomainClass="true">
        <Class>
          <DomainClassMoniker Name="ConceptSubElement" />
        </Class>
        <PropertyDisplayed>
          <PropertyPath>
            <DomainPropertyMoniker Name="ConceptSubElement/Name" />
            <DomainPath />
          </PropertyPath>
        </PropertyDisplayed>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings ShowsDomainClass="true">
        <Class>
          <DomainClassMoniker Name="ModelElement" />
        </Class>
        <PropertyDisplayed>
          <PropertyPath>
            <DomainPropertyMoniker Name="ModelElement/Name" />
            <DomainPath />
          </PropertyPath>
        </PropertyDisplayed>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings ShowsDomainClass="true">
        <Class>
          <DomainClassMoniker Name="SubElement" />
        </Class>
        <PropertyDisplayed>
          <PropertyPath>
            <DomainPropertyMoniker Name="SubElement/Name" />
            <DomainPath />
          </PropertyPath>
        </PropertyDisplayed>
      </ExplorerNodeSettings>
    </CustomNodeSettings>
  </ExplorerBehavior>
  <ConnectionBuilders>
    <ConnectionBuilder Name="SubElementReferencesRelationshipBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="SubElementReferencesRelationship" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="SubElement" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Relationship" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="ConceptReferencesRelationshipBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ConceptReferencesRelationship" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Concept" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Relationship" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="ConceptElementReferencesRelationshipBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ConceptElementReferencesRelationship" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ConceptElement" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Relationship" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="ConceptSubElementReferencesRelationshipBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="ConceptSubElementReferencesRelationship" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="ConceptSubElement" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Relationship" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
  </ConnectionBuilders>
  <Diagram Id="ca3f1951-3a94-4564-a805-02e3bfc91b8b" Description="Description for Bb.ApplicationCooperationViewPoint.ApplicationCooperationViewPointDiagram" Name="CooperationViewPointDiagram" DisplayName="Minimal Language Diagram" Namespace="Bb.ApplicationCooperationViewPoint" HasCustomConstructor="true" GeneratesDoubleDerived="true">
    <Class>
      <DomainClassMoniker Name="Model" />
    </Class>
    <ShapeMaps>
      <ShapeMap>
        <DomainClassMoniker Name="ModelElement" />
        <ParentElementPath>
          <DomainPath>ModelHasElements.Model/!Model</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CooperationShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ModelElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CooperationShape/TypeDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ModelElement/Type" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="CooperationShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="SubElement" />
        <ParentElementPath>
          <DomainPath>ModelElementHasChildren.Parent/!ModelElement/ModelHasElements.Model/!Model</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CooperationSubShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="SubElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CooperationShape/TypeDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="SubElement/Type" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="CooperationSubShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="ConceptElement" />
        <ParentElementPath>
          <DomainPath>ConceptHasChildren.Parent/!Concept/ModelHasConcepts.Model/!Model</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CooperationShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ConceptElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="CooperationShape/TypeDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ConceptElement/Type" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="ConceptElementShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="ConceptSubElement" />
        <ParentElementPath>
          <DomainPath>ConceptElementHasChildren.Parent/!ConceptElement/ConceptHasChildren.Parent/!Concept/ModelHasConcepts.Model/!Model</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ConceptSubElementShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ConceptSubElement/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ConceptSubElementShape/TypeDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="ConceptSubElement/Type" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="ConceptSubElementShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="Concept" />
        <ParentElementPath>
          <DomainPath>ModelHasConcepts.Model/!Model</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ConceptShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Concept/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ConceptShape/TypeDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Concept/Type" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="ConceptShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="Relationship" />
        <ParentElementPath>
          <DomainPath>ModelHasRelationships.Model/!Model</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="RelationshipShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Relationship/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="ConceptSubElementShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Relationship/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="RelationshipShape" />
      </ShapeMap>
    </ShapeMaps>
  </Diagram>
  <Designer CopyPasteGeneration="CopyPasteOnly" FileExtension="covp" EditorGuid="d430d989-356e-4935-8012-a5020680189a">
    <RootClass>
      <DomainClassMoniker Name="Model" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="ApplicationCooperationViewPointSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="Cooperation Viewpoint" />
    <Validation UsesMenu="false" UsesOpen="true" UsesSave="true" UsesLoad="false" />
    <DiagramMoniker Name="CooperationViewPointDiagram" />
  </Designer>
  <Explorer ExplorerGuid="581e6349-7922-43c9-bb72-0222757290ef" Title="ApplicationCooperationViewPoint Explorer">
    <ExplorerBehaviorMoniker Name="ApplicationCooperationViewPoint/ApplicationCooperationViewPointExplorer" />
  </Explorer>
</Dsl>