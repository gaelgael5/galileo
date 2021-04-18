using Bb.Galileo.Files.Datas;
using System.Collections.Generic;

namespace Bb.Galileo.Viewpoints.Cooperations
{

    public class ViewpointProjectionEntities
    {

        public ViewpointProjectionEntities()
        {
            Entities = new List<ViewpointProjectionEntity>();
        }

        public List<ViewpointProjectionEntity> Entities { get; }

    }

    public class ViewpointProjectionEntity
    {

        public ViewpointProjectionEntity()
        {
            Relationships = new List<ViewpointProjectionRelationship>();
        }

        public ReferentialEntity Entity { get; }

        public List<ViewpointProjectionRelationship> Relationships { get; }

    }

    public class ViewpointProjectionRelationship
    {

        public ReferentialRelationship Relationship { get; }

    }

}
