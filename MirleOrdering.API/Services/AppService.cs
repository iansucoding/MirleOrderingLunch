using Microsoft.Extensions.Configuration;

namespace MirleOrdering.Api.Services
{
    public class AppService
    {
        private readonly IConfiguration _config;

        public AppService(IConfiguration config)
        {
            _config = config;
        }
    }
}
