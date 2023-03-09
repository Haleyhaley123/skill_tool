using Microsoft.Extensions.Options;
using NTS.Common.RedisCache;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTS.Common.Cache
{
    public class CacheConnection : ICacheConnection
    {
        private static ConnectionMultiplexer _muxer;
        private readonly ConnectionMultiplexer _connection;

        public CacheConnection(IOptions<RedisCacheSettingModel> options)
        {
            this._connection = GetMuxer(options.Value.ConnectionString);
        }

        public ConnectionMultiplexer Connection()
        {
            return this._connection;
        }

        private static ConnectionMultiplexer GetMuxer(string connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
                return null;

            if (_muxer == null)
                _muxer = ConnectionMultiplexer.Connect(connectionString);

            return _muxer;
        }
    }
}
