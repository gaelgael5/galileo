﻿namespace Bb.Galileo
{
 

    public enum KindSchemaEnum
    {
        Undefined,
        Entity,
        Relationship,
        Definition,
        Schema,
        SchemaDefinitions,
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
