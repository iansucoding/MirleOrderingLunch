using Microsoft.AspNetCore.Http;

namespace MirleOrdering.Api.ViewModels
{
    public class CategoryCreationModel
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }
        public IFormFile File { get; set; }
    }
}
