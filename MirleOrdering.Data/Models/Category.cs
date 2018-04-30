using System.Collections.Generic;

namespace MirleOrdering.Data.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }

        public int Seq { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
