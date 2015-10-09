using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;


namespace DbBase.Redis
{
    public class HashOperator : RedisOperatorBase
    {
        public HashOperator() : base() { }

        #region:.Item
        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public  bool Item_Set<T>(string key, T t)
        {
            try
            {
                iRedis.Set<T>(key, t, new TimeSpan(1, 0, 0));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        /// <summary>
        /// 获取单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public  T Item_Get<T>(string key) where T : class
        {
            return iRedis.Get<T>(key);
        }

        /// <summary>
        /// 移除单体
        /// </summary>
        /// <param name="key"></param>
        public  bool Item_Remove(string key)
        {
            return iRedis.Remove(key);
        }
        #endregion:.

        #region:.List
        /// <summary>
        /// 添加数据List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="t">数据</param>
        public  void List_Add<T>(string key, T t)
        {
            var redisTypedClient = iRedis.As<T>();
            
            redisTypedClient.AddItemToList(redisTypedClient.Lists[key], t);
        }

        /// <summary>
        /// 添加数据List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="t">数据</param>
        public List<T> List_Get<T>(string key)
        {
            var redisTypedClient = iRedis.As<T>();
            return redisTypedClient.GetAllItemsFromList(redisTypedClient.Lists[key]);
        }

        /// <summary>
        /// 从List移除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="t">数据</param>
        /// <returns></returns>
        public bool List_Remove<T>(string key, T t)
        {
            var redisTypedClient = iRedis.As<T>();
            return redisTypedClient.RemoveItemFromList(redisTypedClient.Lists[key], t) > 0;
        }

        /// <summary>
        /// 移除List的所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        public  void List_RemoveAll<T>(string key)
        {
            var redisTypedClient = iRedis.As<T>();
            redisTypedClient.Lists[key].RemoveAll();
        }

        /// <summary>
        /// 获取List数据总数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public  long List_Count(string key)
        {
            return iRedis.GetListCount(key);
        }

        /// <summary>
        /// 获取范围数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="start">开始数</param>
        /// <param name="count">总数</param>
        /// <returns></returns>
        public  List<T> List_GetRange<T>(string key, int start, int count)
        {
            var c = iRedis.As<T>();
            return c.Lists[key].GetRange(start, start + count - 1);
        }

        /// <summary>
        /// 获取总数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public  List<T> List_GetList<T>(string key)
        {
            var c = iRedis.As<T>();
            return c.Lists[key].GetRange(0, c.Lists[key].Count);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public  List<T> List_GetList<T>(string key, int pageIndex, int pageSize)
        {
            int start = pageSize * (pageIndex - 1);
            return List_GetRange<T>(key, start, pageSize);
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="datetime">时间</param>
        public  void List_SetExpire(string key, DateTime datetime)
        {
            iRedis.ExpireEntryAt(key, datetime);
        }
        #endregion:.

        #region:.Set
        /// <summary>
        /// 添加单体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="t">数据</param>
        public  void Set_Add<T>(string key, T t)
        {
            var redisTypedClient = iRedis.As<T>();
            redisTypedClient.Sets[key].Add(t);
        }

        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="t">数据</param>
        /// <returns></returns>
        public  bool Set_Contains<T>(string key, T t)
        {
            var redisTypedClient = iRedis.As<T>();
            return redisTypedClient.Sets[key].Contains(t);
        }

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="t">数据</param>
        /// <returns></returns>
        public  bool Set_Remove<T>(string key, T t)
        {
            var redisTypedClient = iRedis.As<T>();
            return redisTypedClient.Sets[key].Remove(t);
        }
        #endregion:.

        #region:.Hash
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="hashId">id</param>
        /// <param name="key">值</param>
        /// <returns></returns>
        public  bool Exist<T>(string hashId, string key)
        {
            return iRedis.HashContainsEntry(hashId, key);
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="hashId">id</param>
        /// <param name="key">值</param>
        /// <param name="t">数据</param>
        /// <returns></returns>
        public  bool Set<T>(string hashId, string key, T t)
        {
            var value = JsonSerializer.SerializeToString<T>(t);
            return iRedis.SetEntryInHash(hashId, key, value);
        }

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        public  T Get<T>(string hashId, string key)
        {
            string value = iRedis.GetValueFromHash(hashId, key);
            return JsonSerializer.DeserializeFromString<T>(value);
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="hashId">id</param>
        /// <param name="key">值</param>
        /// <returns></returns>
        public  bool Remove(string hashId, string key)
        {
            return iRedis.RemoveEntryFromHash(hashId, key);
        }

        /// <summary>
        /// 移除整个hash
        /// </summary>
        /// <param name="key">值</param>
        /// <returns></returns>
        public  bool Remove(string key)
        {
            return iRedis.Remove(key);
        }

        /// <summary>
        /// 获取整个hash的数据
        /// </summary>
        public  List<T> GetAll<T>(string hashId)
        {
            var result = new List<T>();
            var list = iRedis.GetHashValues(hashId);
            if (list != null && list.Count > 0)
            {
                list.ForEach(x =>
                {
                    var value = JsonSerializer.DeserializeFromString<T>(x);
                    result.Add(value);
                });
            }
            return result;
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        public  void SetExpire(string key, DateTime datetime)
        {
            iRedis.ExpireEntryAt(key, datetime);
        }
        #endregion:.
    }
}
