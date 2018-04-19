namespace MirleOrdering.Service.ViewModels
{
    public class SettingBaseModel
    {
        public int StopHourOn { get; set; }
        public string Announcement { get; set; }
    }
    public class SettingViewModel : SettingBaseModel
    {
        public long SettingId { get; set; }
    }
}
