namespace Bb.Galileo.Files.Datas
{
    public class ReferentialEntity : ReferentialBase
    {

        public ReferentialEntity(string type, FileModel fullPath)
            : base(type, fullPath)
        {

        }


        public string Label
        {
            get => (string)this[nameof(Label)];
            set => this[nameof(Label)] = value;
        }

        public string Description
        {
            get => (string)this[nameof(Description)];
            set => this[nameof(Description)] = value;
        }

    }


}
