namespace MirleOrdering.Data.Models
{
    public class Order : BaseEntity
    {
        public User User { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
