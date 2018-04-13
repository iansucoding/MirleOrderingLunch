using MirleOrdering.Data.Models;
using MirleOrdering.Service.ViewModels;
using System.Collections.Generic;

namespace MirleOrdering.Service.Interfaces
{
    public interface ICategoryService : IGenericService<Category, CategoryViewModel, CategoryBaseModel>
    {
        CategoryViewModel GetByIdWithProducts(long id);
        IEnumerable<CategoryViewModel> GetAllWithProducts();
        IEnumerable<SelectionViewModel> GetSelection();
    }
}
