/*********************************************
* 命名空间:LedSendServer.Common
* 功 能： 数据解析实现
* 类 名： Resolution
* 作 者:  东腾
* 时 间： 2018/6/25 11:43:40 
**********************************************
*/

using System;
using System.Collections.Generic;
using Abp.Dependency;
using DT.Data.Core.HttpRequest;
using DT.Data.Core.Redis;
using LedSendServer.Model;
using Newtonsoft.Json;
using Smart.SMSSend.Model;

namespace LedSendServer.Common
{
    public class Resolution:IResolution,ITransientDependency
    {
        private readonly IRedisManager _redis;
        
        private double NormalValue { get; set; }
        private double L1 { get; set; }
        private double L2 { get; set; }
        private double L3 { get; set; }

        private DtRestClient _client;
        private DtRestRequest _dtRest;

        public Resolution(IRedisManager redis)
        {
            _redis = redis;
            
            NormalValue = 0.03;//允许的误差值

            L1 = 0.03;
            L2 = 0.1;
            L3 = 0.15;

            //TODO 获取天气情况 

        }

        public List<SendModel> GetLedModel(Model.ReceiveDataModel model)
        {
            var list=new List<SendModel>();
            try
            {
                //根据监测点信息判断是否有缓存存在
                var value = double.Parse(model.TagValue);
                var cache = _redis.Get<List<LedCache>>("Default:Kylin:LED:" + model.StationKey);
                if (cache != null)
                {

                    foreach (var item in cache)
                    {
                        var smodel=new SendModel();

                        //获取led的缓存信息
                        smodel.CardCode = item.Led.PHONE;
                        smodel.StationKey = item.MonitorRecord.BMID;
                        smodel.StationName = item.MonitorRecord.BMMC;

                        //判断是否问正常

                        if (value <= NormalValue)
                        {
                            //正常值
                            smodel.IsNormal = true;
                            smodel.IsSendMsg = false;
                            smodel.Level = 0;
                        }
                        else
                        {
                            smodel.IsNormal = false;

                            if (value > L1 && value < L2)
                            {
                                smodel.Level = 1;
                                //TODO 出现误报的情况，需要判断是否发送短信

                            }
                            else if (value >= L2 && value > L3)
                            {
                                smodel.Level = 2;
                                smodel.IsSendMsg = false;
                            }
                            else
                            {
                                smodel.Level = 3;
                                smodel.IsSendMsg = false;
                            }

                          
                        }
                        list.Add(smodel);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return list;
        }
        //发送短信
        public bool SendMsg(SmsSendModel model)
        {
            throw new System.NotImplementedException();
        }
        //发送led信息
        public bool SendLed(SendModel model)
        {
            throw new NotImplementedException();
        }
    }
}
