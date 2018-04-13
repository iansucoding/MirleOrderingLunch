using System;

namespace MirleOrdering.Data.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Seq { get; set; }

        public int Price { get; set; }


        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }
    }
}
