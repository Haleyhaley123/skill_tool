using Foundatio.Caching;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using NTS.Common.Attributes;
using NTS.Common.Files;
using NTS.Common.Models;
using NTS.Common.RedisCache;
using NTS.Common.Users;
using NTS.Document.Excel;
using NTS.Document.Word;
using StackExchange.Redis;
using System.Text;
using ToolManage.Models.Helpers;
using Training.Api.AppInitialize;
using Training.Models.Entities;
using Training.Services.Auth;
using Training.Services.Categorys;
using Training.Services.Combobox;
using Training.Services.FileView;
using Training.Services.Log;
using Training.Services.Signalr;
using Training.Services.Users;

namespace Training.Api
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyHeader()
                     .AllowAnyMethod()
                     .SetIsOriginAllowed((host) => true)
                     .AllowCredentials()
                     ;
                });
            });

            services.AddDbContext<TrainingContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            // Add detection services container and device resolver service.
            services.AddDetection();

            // Add framework services.
            services.AddControllersWithViews();
            services.AddSession();

            services.AddSignalR();

            services.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = 4294967296;
            });

            var appSettingsSection = Configuration.GetSection("AppSetting");
            services.Configure<AppSettingModel>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettingModel>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });

            //services.AddCache(Configuration);
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddNtsUpload(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Training", Version = "v1" });
            });

            services.AddMvc(config =>
            {
                config.Filters.Add(new ApiHandleExceptionSystemAttribute());
            })
                 .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                 .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


            var redisSettingSection = Configuration.GetSection("RedisCacheSettings");
            services.Configure<RedisCacheSettingModel>(redisSettingSection);

            var redisConfig = redisSettingSection.Get<RedisCacheSettingModel>();
            var muxer = ConnectionMultiplexer.Connect(redisConfig.ConnectionString);
            services.AddSingleton<ICacheClient>(s => new RedisCacheClient(new RedisCacheClientOptions { ConnectionMultiplexer = muxer }));

            services.AddScoped<IComboboxService, ComboboxService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<INtsUserService, NtsUserService>();
            services.AddScoped<IUploadFileService, UploadFileService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<ILogEventService, LogEventService>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<IFileViewService, FileViewService>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTgxNDc2QDMxMzkyZTM0MmUzMGR1VkRzdTNobWNrRW54eERGc0dKUzM4SlRlU21XV3NuV2UyYTdkUUFmQTA9;NTgxNDc3QDMxMzkyZTM0MmUzMGJQa3VmcFU1b0lSemIyM3JHYzZpVWR3T3dzZTBJUThwRGxldHgvYWN3WEk9;NTgxNDc4QDMxMzkyZTM0MmUzMEJyeUFGYVIxZ0w1cUZ2OGtQVnI5VHBISXdDZGF6a1B1Q0ZTYVgvMUdXOVE9;NTgxNDc5QDMxMzkyZTM0MmUzMEVaSnFiRUYyZ0EwMW1PWDZmd282MjZ5aWRzRWxKSUE1S1RNYlAyVlBCZlE9;NTgxNDgwQDMxMzkyZTM0MmUzMGJVbzZxUmhOQWNORUlhZ0Evc1E4dTFCTktwbmZZeHRoT1pWZDhzdThVeG89;NTgxNDgxQDMxMzkyZTM0MmUzMGtzWisrb1ZEVG5NSHI5a0ZFZUpHdW4zTHU5SHlRdS9YSXpKWHg0VjVlRkE9;NTgxNDgyQDMxMzkyZTM0MmUzMGtnZXRGTWJLdlk1bkVnVlB5RW5vc005MEpqc1ROQ3F2a2JtdVNkMUNGMHM9;NTgxNDgzQDMxMzkyZTM0MmUzMEdOTGtpRVg4WnoxY25MZ3hzYjhaYS9OVFM5ZUM2MWZNMmVaK2pYNm5FcTQ9;NTgxNDg0QDMxMzkyZTM0MmUzME5qVk8xWmhHN0x3dGFWY202OHBXV28zU25DalVRdXcybFdQV0pHTTVIWFk9;NTgxNDg1QDMxMzkyZTM0MmUzMEZjalpnN2J3c3ZKdm5QTWhKU3RQY0xXSk05N2RLb2E4bmFiN2hFckR2Vm89;NTgxNDg2QDMxMzkyZTM0MmUzMFB0L0J0WHJYc1lxUGpFU1VxWnVZQXpCRGc3LzUvQnhQRTFGd0JQVWdueHM9");
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Training v1"));
            }

            loggerFactory.AddFile("Logs/log-{Date}.txt");
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDetection();
            app.UseSession();
            app.UseFileServer();
            app.UseRouting();
            app.UseNtsUploadStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SignalrHub>("/signalr");

                endpoints.MapControllerRoute(
                    name: "default",
                  pattern: "{controller=angularhome}/{action=index}/{id?}"
                );
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TrainingContext>();
                context.Database.Migrate();

                InitUser.Init(context);
                InitPermission.Init(context);
            }
        }
    }
}
