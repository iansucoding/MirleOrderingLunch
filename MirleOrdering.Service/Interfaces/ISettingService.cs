using MirleOrdering.Data.Models;
using MirleOrdering.Service.ViewModels;

namespace MirleOrdering.Service.Interfaces
{
    public interface ISettingService : IGenericService<Setting, SettingViewModel, SettingBaseModel>
    {
        SettingViewModel GetLastOne();
    }
}
