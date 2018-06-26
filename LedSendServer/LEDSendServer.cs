/*********************************************
* 命名空间:LedSendServer
* 功 能： led信息发送服务
* 类 名： LEDSendServer
* 作 者:  东腾
* 时 间： 2018/6/21 15:26:09 
**********************************************
*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Abp.Dependency;
using DT.Data.Core.Cache;
using DT.Data.Core.Redis;
using LedSendServer.Model;
using log4net;
using log4net.Core;
using LedSendServer.Common;
using Microsoft.AspNet.SignalR.Client;

namespace LedSendServer
{
    public class LedSendServer: ISingletonDependency
    {
        //服务代理
        private IHubProxy Hub { get; set; }
        //服务连接对象
        private HubConnection hubConnection { get; set; }
        //广播地址
        private readonly string _serverUrl;
        //日志
        private readonly ILog _log;
        //服务定时
        private readonly Timer _timer;
        //Redis缓存
        private readonly IRedisManager _redis;
        //数据解析
        private readonly IResolution _resolution;
        //led字库
        private List<FontLibrary> fontList;
        //系统缓存
        private IDTCache _cache;

        public LedSendServer(IRedisManager redis, IResolution resolution, IDTCache cache)
        {
            _serverUrl = ConfigurationManager.ConnectionStrings["Signalr"].ConnectionString;

            _log = LogManager.GetLogger(typeof(LedSendServer));

            _timer = new Timer(10000) { Enabled = true };

            _redis = redis;

            _resolution = resolution;

            fontList = _redis.Get<List<FontLibrary>>("Default:Kylin:LED:FontLibrary");

            _cache = cache;
        }

        public void Start()
        {
            
            _timer.Elapsed += Time_Elapsed;
            

            ConnectionServer();
        }
        
        private void Time_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                //if(hubConnection==null) return;
                if (hubConnection.State == ConnectionState.Connected)
                    return;
                ConnectionServer();
            }
            catch (Exception exception)
            {
                _log.Error(exception);
            }
           
        }

        private async void ConnectionServer()
        {
            hubConnection = new HubConnection(_serverUrl);
            hubConnection.Closed += HubConnection_Closed;
            Hub = hubConnection.CreateHubProxy("MyHub");
            try
            {
                Hub.On<string, ReceiveDataModel>("GetRealData", Receive);
                await hubConnection.Start();
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
            }
        }
        private void Receive(string identify, ReceiveDataModel model)
        {
            try
            {
                //进行数据解析
                var list = _resolution.GetLedModel(model);
                if (list.Count>0)
                {
                    foreach (var data in list)
                    {
                        //正常信息
                        if (data.IsNormal)
                        {
                            //如果属于正常发送，则需要存储第一次发送的时间，若超过指定时间段则重新随机发送,每一个控制卡存储一个时间点
                            var lasttime = _cache.Get<object>(data.CardCode);

                            if (lasttime != null)
                            {
                                //超过半小时进行发送
                                if ((DateTime.Now - (DateTime)lasttime).TotalMinutes > 30)
                                {
                                    data.Content = GetContent(data.Level);
                                }
                                //data.Content = GetContent(data.Level);
                                // todo 执行发送
                            }
                            else
                            {
                                //保存发送时间
                                _cache.Add(data.CardCode, DateTime.Now);

                                data.Content = GetContent(data.Level);

                                // todo 执行发送
                            }
                        }
                        else
                        {
                            //有降雨的情况
                            var content = GetContent(data.Level);
                            data.Content = @"当前积水:" + model.TagValue + model.Units + "," + content;

                            // todo 执行发送

                            //todo  预留的功能，需要发送短信息，待讨论
                            if (data.IsSendMsg)
                            {
                                //给运维人员发送短信，通知当前led的内容

                                var msg = @"系统服务自动发送了Led信息【" + data.Content + "】，请确认";

                                //_resolution.SendMsg()
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
            }
        }

        //执行发送
   
        public  string GetContent(int level)
        {
            var content = string.Empty;
            switch (level)
            {
                case 0:
                    //随机发送一条
                    var reg = new Random().Next(fontList.Count(a => a.WEATHERTYPE == 0));
                    content = fontList.Where(a => a.WEATHERTYPE == 0).ToList()[reg].CONTENT;
                    break;
                case 1:
                    content = fontList.FirstOrDefault(a => a.WEATHERTYPE == 1 && a.WATERLEVEL == "1")?.CONTENT;
                    break;
                case 2:
                    content = fontList.FirstOrDefault(a => a.WEATHERTYPE == 1 && a.WATERLEVEL == "2")?.CONTENT;
                    break;
                case 3:
                    content = fontList.FirstOrDefault(a => a.WEATHERTYPE == 1 && a.WATERLEVEL == "3")?.CONTENT;
                    break;
            }

            return content;
        }



        private void HubConnection_Closed()
        {
            //定时
            _timer.Start();
        }
        public void Stop()
        {
            _timer.Stop();
        }
    }
}
