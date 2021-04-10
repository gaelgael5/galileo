namespace Bb.Galileo.Files.Datas
{
    public class ReferentialRelationship : ReferentialBase
    {


        public ReferentialRelationship(string type, FileModel file)
            : base(type, file)

        {
            this.Origin = new ReferentialRelationshipLink(file);
            this.Target = new ReferentialRelationshipLink(file);
        }

        public string Label
        {
            get => (string)this[nameof(Name)];
            set => this[nameof(Label)] = value;
        }

        public string Description
        {
            get => (string)this[nameof(Name)];
            set => this[nameof(Description)] = value;
        }

        public ReferentialRelationshipLink Origin { get; set; }

        public ReferentialRelationshipLink Target { get; set; }
    
    }


}
