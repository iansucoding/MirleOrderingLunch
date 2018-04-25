using System;

namespace MirleOrdering.Service.ViewModels
{
    public class OrderBaseModel
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public int Amount { get; set; }
    }

    public class OrderViewModel : OrderBaseModel
    {
        public long OrderId { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public int? ProductPrice { get; set; }
        public DateTime OrderedOn { get; set; }
    }
}
