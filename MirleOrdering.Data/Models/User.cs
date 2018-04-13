namespace MirleOrdering.Data.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public virtual Role Role { get; set; }
        public long? RoleId { get; set; }
        public virtual Group Group { get; set; }
        public long? GroupId { get; set; }
        public string Password { get; set; }
    }
}
