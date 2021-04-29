using Bb.Galileo.Files.Datas;

namespace Bb.Galileo.Viewpoints.Cooperations
{
    public class RelationshipItem
    {


        public RelationshipItem(ReferentialRelationship relationship, ReferentialEntity item)
        {
            this.Relationship = relationship;
            this.Entity = item;
        }

        public ReferentialRelationship Relationship { get; }

        public ReferentialEntity Entity { get; }

    }

}
