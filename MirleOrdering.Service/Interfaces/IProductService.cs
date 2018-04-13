using MirleOrdering.Data.Models;
using MirleOrdering.Service.ViewModels;

namespace MirleOrdering.Service.Interfaces
{
    public interface IProductService : IGenericService<Product, ProductViewModel, ProductBaseModel>
    {
        ReturnViewModel Patch(long id, string name, int price, string desc);
        bool IsProductExisted(long id);
    }
}
