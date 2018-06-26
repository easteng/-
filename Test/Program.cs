using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheManager.Core;
using DT.Data.Core.Cache;
using DT.Data.Core.Redis;
using Newtonsoft.Json;
using Smart.SMSSend.Model;

namespace Test
{
    class Program
    {
        private IRedisManager _redis;
        
        static void Main(string[] args)
        {

             

            // var redis = CacheFactory.Build<object>(a =>
            // {
            //     a.WithSystemRuntimeCacheHandle()
            //         .And
            //         .WithRedisConfiguration("default", b =>
            //         {
            //             b.WithAllowAdmin()
            //                 .WithDatabase(0)
            //                 .WithEndpoint("192.168.70.6", 6379)
            //                 .WithPassword("123");
            //         })
            //         .WithMaxRetries(100);
            // });

            //redis.Add("Default:Kylin:SMS:22", 123456);

            // var model=new SmsCacheModel()
            // {
            //     StationName = "123"
            // };
            // var aa = redis.Get<SmsCacheModel>("Default:Kylin:SMS:3413020111960000001");

            // var bb = JsonConvert.DeserializeObject(aa);
        }

       
    }
}
