using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace DbBase.Redis
{
    public abstract class RedisOperatorBase : IDisposable
    {
        protected IRedisClient iRedis { get; set; }
        private bool _disposed = false;

        protected RedisOperatorBase()
        {
            iRedis = RedisManager.GetClient();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    iRedis.Dispose();
                    iRedis = null;
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            iRedis.Save();
        }

        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            iRedis.SaveAsync();
        }
    }
}
