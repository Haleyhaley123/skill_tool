using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NTS.Common.RedisCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTS.Common.Cache
{
    public static class CacheServiceCollectionExtensions
    {
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConfig = configuration.GetSection("RedisCacheSettings");
            services.Configure<RedisCacheSettingModel>(redisConfig);

            services.AddSingleton<ICacheConnection, CacheConnection>();
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
