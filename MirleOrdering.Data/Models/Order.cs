namespace MirleOrdering.Data.Models
{
    public class Order : BaseEntity
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Amount { get; set; }
    }
}
