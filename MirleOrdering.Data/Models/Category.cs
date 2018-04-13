using System.Collections.Generic;

namespace MirleOrdering.Data.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Seq { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
