namespace Bb.Galileo
{
 

    public enum KindSchemaEnum
    {
        Undefined,
        Entity,
        Relationship,
        Definition,
        Schema,
        SchemaEntity,
        SchemaLink,
        SchemaDefinitions,
        CooperationViewpoint,
        SchemaLayerDefinitions,
    }

    public enum ElementEnum
    {
        Entity,
        Relationship,
        EntityDefinition,
        RelationshipDefinition,
    }

    public enum SeverityEnum
    {
        Verbose,
        Information,
        Warning,
        Error,
    }
}
