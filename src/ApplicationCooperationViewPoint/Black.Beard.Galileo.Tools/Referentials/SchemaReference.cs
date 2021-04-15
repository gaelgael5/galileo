namespace Bb.Galileo
{
    public class SchemaReference
    {


        public string Type { get; set; }

        public KindSchemaEnum Kind { get; internal set; }

        public string Schema { get; internal set; }

        public string FilePath { get; internal set; }
        public bool IsValidFile { get; internal set; }
    }


}
