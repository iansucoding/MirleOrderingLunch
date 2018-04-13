namespace MirleOrdering.Service.ViewModels
{
    public class ProductBaseModel
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Seq { get; set; }
        public long? CategoryId { get; set; }
    }
    public class ProductViewModel : ProductBaseModel
    {
        public long ProductId { get; set; }
        public string CategoryName { get; set; }
    }
}
