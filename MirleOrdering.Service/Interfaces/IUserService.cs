using MirleOrdering.Data.Models;
using MirleOrdering.Service.ViewModels;

namespace MirleOrdering.Service.Interfaces
{
    public interface IUserService : IGenericService<User, UserViewModel, UserBaseModel>
    {

    }
}
