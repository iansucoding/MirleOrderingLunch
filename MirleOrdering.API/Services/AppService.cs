using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MirleOrdering.Api.Services
{
    public class AppService
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _environment;

        public AppService(IConfiguration config, IHostingEnvironment environment)
        {
            _config = config;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }
        public async Task<string> Upload(IFormFile file, string name = null)
        {
            if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
            {
                _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            var dir = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var fileName = file.FileName;
            var path = Path.Combine(dir, fileName);
            if (!string.IsNullOrEmpty(name)) // 使用自訂的檔案名稱
            {
                var extension = Path.GetExtension(path);
                fileName = $"{name}{extension}";
                path = path.Replace(file.FileName, fileName);
            }
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    try
                    {
                        await file.CopyToAsync(fileStream);
                        return fileName;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            else
            {
                throw new Exception("file is empty");
            }
        }
    }
}
