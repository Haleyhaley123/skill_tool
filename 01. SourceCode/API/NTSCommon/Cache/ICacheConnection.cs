using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTS.Common.Cache
{
    public interface ICacheConnection
    {
        ConnectionMultiplexer Connection();
    }
}
