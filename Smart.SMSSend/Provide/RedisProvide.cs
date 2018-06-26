/*********************************************
* 命名空间:Smart.SMSSend.Provide
* 功 能： redis接口实现
* 类 名： RedisProvide
* 作 者:  东腾
* 时 间： 2018/6/14 14:40:07 
**********************************************
*/
using Abp.Dependency;
using DT.Data.Core.Redis;
using Smart.SMSSend.Interface;
using Smart.SMSSend.Model;
using System;
using System.Configuration;
using System.Linq;
using SmsCacheModel = Smart.SMSSend.Model.SmsCacheModel;
using DateTime = System.DateTime;

namespace Smart.SMSSend.Provide
{
    public class RedisProvide : IRedis, ITransientDependency
    {
        private readonly IRedisManager _cache;

        public RedisProvide()
        {
            _cache = new DT.Data.Core.Redis.RedisProvide();
        }
        public SmsSendModel GetSmsSendModel(ReceiveDataModel model)
        {
            var smsModel = new SmsSendModel();

            //短信发送两种逻辑，1、有报警则根据配置的时间间隔进行发送；2、当前与上一次接收的值超过了阈值，进行发送。

            //首先判断此监测点是否报警，若报警，则继续执行下一步
            if (string.IsNullOrEmpty(model.AlertId)) return smsModel;


            var stationKey = model.StationKey;

            var cache = _cache.Get<SmsCacheModel>("Default:Kylin:SMS:" + stationKey);

            //根据监测点编号判断是否有缓存 
            if (cache == null) return smsModel;

            smsModel.SystemCode = ConfigurationManager.AppSettings["SystemCode"];
            smsModel.IsSend = cache.IsEnabled != 0;
            var smsInfo = cache.StationName + "," + model.TagName + "," + model.TagValue +
                          model.Units + "," + model.SaveTime;
            smsModel.MsgString = smsInfo;
            smsModel.MsgTempId = cache.TemplateId==""? ConfigurationManager.AppSettings["DefaultTemplateId"] : cache.TemplateId;
            smsModel.Phone = cache.PhoneString;
            smsModel.StationKey = stationKey;
            smsModel.Value = decimal.Parse(model.TagValue);
            smsModel.SaveTime = model.SaveTime;
            if (cache.Interval==0)
            {
                cache.Interval = int.Parse(ConfigurationManager.AppSettings["DefaultSendInterval"]);
            }

            if (decimal.Parse(model.TagValue) - cache.LastValue >= cache.ChangeDiff)
            {
                //立刻发送
                UpdateCache(stationKey,cache, model.TagValue,model.SaveTime);
                return smsModel;
            }
            else if (cache.LastTime.AddMinutes((int)cache.Interval) <= DateTime.Now)
            {
                //根据时间间隔发送
                UpdateCache(stationKey,cache, model.TagValue, model.SaveTime);
                return smsModel;
            }
            else
            {
                return null;
            }
        }

        private void UpdateCache(string key,SmsCacheModel model,string value,DateTime time)
        {
            model.LastTime = time;
            model.LastValue = decimal.Parse(value);
            _cache.Updata("Default:Kylin:SMS:"+key, model);
        }
    }
}
