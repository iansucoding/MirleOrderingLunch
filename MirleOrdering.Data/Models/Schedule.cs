using System;

namespace MirleOrdering.Data.Models
{
    public class Schedule : BaseEntity
    {
        public int CategoryId { get; set; }
        public DateTime AvailableOn { get; set; }
        public string  Remark { get; set; }
    }
}
