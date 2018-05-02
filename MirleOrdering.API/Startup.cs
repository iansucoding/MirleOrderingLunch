using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MirleOrdering.Api.Services;
using MirleOrdering.Data;
using MirleOrdering.Repo;
using MirleOrdering.Service;
using MirleOrdering.Service.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace MirleOrdering.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<MirleOrderingContext>(options => options.UseInMemoryDatabase("MirleOrderingContext"));
            services.AddDbContext<MirleOrderingContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MirleOrderingContext")));

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(AppService));
            services.AddTransient(typeof(AuthService));
            services.AddTransient(typeof(HelperService));
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            // ASP.NET Core Web API 很貼心的把回傳物件格式預設為 JSON camelCase
            services.AddMvc().AddJsonOptions(options =>
            {
                // 在轉型的過程中如果找不到欄位會自動轉成 null，不想傳送數值為 null 的欄位可以這樣設定
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }); ;

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles(); // UseStaticFiles 預設啟用靜態檔案的目錄是 wwwroot
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string url = "/swagger/v1/swagger.json";
                if (!env.IsDevelopment())
                {
                    url = $"/MirleOrderingAPI{url}";
                }
                c.SwaggerEndpoint(url, "My API V1");
            });
            app.UseCors("AllowAll");
            app.UseMvc();
        }
    }
}
