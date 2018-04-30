using System;

namespace MirleOrdering.Api.ViewModels
{
    public class SearchOrderModel
    {
        public long? UserId { get; set; }
        public string Term { get; set; }
        public DateTime? OrderedStartOn { get; set; }
        public DateTime? OrderedEndOn { get; set; }
    }
}
