namespace MirleOrdering.Service.ViewModels
{
    public class UserBaseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public long? RoleId { get; set; }
        public long? GroupId { get; set; }
        public int Balance { get; set; }
    }
    public class UserViewModel : UserBaseModel
    {
        public long UserId { get; set; }
        public string RoleName { get; set; }
        public string GroupName { get; set; }
    }
}
