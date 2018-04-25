using System;

namespace MirleOrdering.API.ViewModels
{
    public class SearchOrderModel
    {
        public long? UserId { get; set; }
        public string Term { get; set; }
        public DateTime? OrderedStartOn { get; set; }
        public DateTime? OrderedEndOn { get; set; }
    }
}
