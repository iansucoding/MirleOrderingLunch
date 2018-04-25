using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MirleOrdering.Data.Models
{
    public class Schedule : BaseEntity
    {
        [Column(TypeName = "Date")]
        public DateTime AvailableOn { get; set; }
        public string  Remark { get; set; }

        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }
    }
}
