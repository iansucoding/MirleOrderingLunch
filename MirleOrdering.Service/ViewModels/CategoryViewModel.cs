using System.Collections.Generic;

namespace MirleOrdering.Service.ViewModels
{
    public class CategoryBaseModel
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int Seq { get; set; }
    }

    public class CategoryViewModel : CategoryBaseModel
    {
        public long CategoryId { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
