namespace Bb.Galileo.Files.Schemas
{
    public class NumberConstraints
    {

        public int MultipleOf { get; set; } = 1;

        public int? Minimum { get; set; }

        public bool ExclusiveMinimum { get; set; } = false;

        public int? Maximum { get; set; }

        public bool ExclusiveMaximum { get; set; }


    }


}
