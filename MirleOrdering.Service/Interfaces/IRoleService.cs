using MirleOrdering.Data.Models;
using MirleOrdering.Service.ViewModels;
using System.Collections.Generic;

namespace MirleOrdering.Service.Interfaces
{
    public interface IRoleService :  IGenericService<Role, RoleViewModel, RoleBaseModel>
    {
        IEnumerable<SelectionViewModel> GetSelection();
    }
}
