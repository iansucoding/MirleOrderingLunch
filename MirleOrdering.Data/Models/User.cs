namespace MirleOrdering.Data.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual Role Role { get; set; }
        public long? RoleId { get; set; }
        public virtual Group Group { get; set; }
        public long? GroupId { get; set; }
        public string Password { get; set; }
        public int Balance { get; set; }
    }
}
