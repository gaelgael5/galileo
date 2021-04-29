using Bb.Galileo.Files.Datas;
using Bb.Galileo.Files.Viewpoints;
using System;
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
            Children = new List<ViewpointProjectionEntity>();
            Relationships = new List<ViewpointProjectionRelationship>();
        }

        public ReferentialEntity Entity { get; set; }

        public List<ViewpointProjectionEntity> Children { get; }

        public List<ViewpointProjectionRelationship> Relationships { get; }
        public ViewpointItem Kind { get; internal set; }
    }

    public class ViewpointProjectionRelationship
    {

        public ReferentialRelationship Relationship { get; set; }

        public ViewpointItem Kind { get; internal set; }

        public ReferentialEntity TargetEntity { get; internal set; }
    }

}
