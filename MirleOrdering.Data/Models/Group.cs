using System.Collections.Generic;

namespace MirleOrdering.Data.Models
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}