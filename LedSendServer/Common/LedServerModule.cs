/*********************************************
* 命名空间:LedSendServer.Common
* 功 能： redis 模块
* 类 名： LedServerModule
* 作 者:  东腾
* 时 间： 2018/6/26 11:38:55 
**********************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Modules;
using DT.Data.Core.Cache;
using DT.Data.Core.Oracle;
using DT.Data.Core.Redis;

namespace LedSendServer.Common
{
    [DependsOn(typeof(AbpRedisModule))]
    [DependsOn(typeof(AbpCacheModuble))]
    public class LedServerModule:AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
