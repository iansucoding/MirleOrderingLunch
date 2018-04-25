using MirleOrdering.Data.Models;
using MirleOrdering.Service.ViewModels;

namespace MirleOrdering.Service.Interfaces
{
    public interface IScheduleService : IGenericService<Schedule, ScheduleViewModel, ScheduleBaseModel>
    {
        CategoryViewModel GetTodaySchedule();
    }
}
