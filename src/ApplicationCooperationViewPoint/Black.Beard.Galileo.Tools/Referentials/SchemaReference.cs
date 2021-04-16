using Bb.Galileo.Files.Schemas;
using Bb.Galileo.Models;

namespace Bb.Galileo
{
    public class SchemaReference
    {

        public string Type { get; set; }

        public KindSchemaEnum Kind { get; internal set; }

        public string Schema { get; internal set; }

        public string FilePath { get; internal set; }

        public bool IsValidExistingFile { get; internal set; }

        public SchemaIdKindEnum SchemaIdKind { get; internal set; }

    }

    public enum SchemaIdKindEnum
    {
        Undefined,
        File,
        Url,
    }

}
