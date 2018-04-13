using MirleOrdering.Data.Models;
using MirleOrdering.Service.ViewModels;
using System.Collections.Generic;

namespace MirleOrdering.Service.Interfaces
{
    public interface IGroupService : IGenericService<Group, GroupViewModel, GroupBaseModel>
    {
        IEnumerable<SelectionViewModel> GetSelection();
    }
}
