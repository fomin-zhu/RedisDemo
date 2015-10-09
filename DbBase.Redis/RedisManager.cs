using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace DbBase.Redis
{
    public class RedisManager
    {
        private static readonly RedisConfigInfo redisConfig = RedisConfigInfo.GetConfig();
        private static PooledRedisClientManager _prcm;

        static RedisManager()
        {
            CreateManager();
        }

        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {
            var writeServerList = SplitString(redisConfig.WriteServerList, ",");
            var readServerList = SplitString(redisConfig.ReadServerList, ",");
            _prcm = new PooledRedisClientManager(readServerList, writeServerList, new RedisClientManagerConfig
            {
                MaxWritePoolSize = redisConfig.MaxWritePoolSize,
                MaxReadPoolSize = redisConfig.MaxReadPoolSize,
                AutoStart = redisConfig.AutoStart
            });
        }

        private static string[] SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public static IRedisClient GetClient()
        {
            if (_prcm == null)
                CreateManager();
            return _prcm.GetClient();
        }
    }
}
