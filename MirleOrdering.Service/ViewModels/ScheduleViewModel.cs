using System;

namespace MirleOrdering.Service.ViewModels
{
    public class ScheduleBaseModel
    {
        public long CategoryId { get; set; }
        public DateTime AvailableOn { get; set; }
        public string Remark { get; set; }
    }
    public class ScheduleViewModel : ScheduleBaseModel
    {
        public long ScheduleId { get; set; }
        public string CategoryName { get; set; }
    }
}
