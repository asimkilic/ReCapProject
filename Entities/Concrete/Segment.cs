using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Segment:IEntity
    {
        public int Id { get; set; }
        public string SegmentClass { get; set; }
        public string Description { get; set; }

    }
}
