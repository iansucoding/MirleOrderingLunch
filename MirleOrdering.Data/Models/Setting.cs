namespace MirleOrdering.Data.Models
{
    public class Setting : BaseEntity
    {
        // 時間到不可訂購
        public int StopHourOn { get; set; }
        // 公告
        public string Announcement { get; set; }
    }
}
