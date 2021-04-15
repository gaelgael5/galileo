using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Galileo.Files.Schemas
{


    public enum MultiplicityEnum
    {
        Undefined,
        One,
        OneOrMany,
        ZeroOrOne,
        ZeroOrMany,
    }

    public enum RelationshipKindEnum
    {
        Undefined,
        Other,
        Access,
        Aggregation,
        Assignment,
        Association,
        Composition,
        Flow,
        Influence,
        Realization,
        Serving,
        Specialization,
        Triggering,
        UsedBy,
    }

    public enum PropertyDefinitionEnum
    {
        Text,
        Integer,
        Double,
        Bool,

        DateTime,
        Time,
        Date,
        Email,
        IdnEmail,
        Hostname,
        IdnHostname,
        Ipv4,
        Ipv6,
        Uri,
        UriReference,
        Iri,
        IriReference,
        UriTemplate,
        JsonPointer,
        Regex,

    }


}
