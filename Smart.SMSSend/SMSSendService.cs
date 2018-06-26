using Abp.Dependency;
using Abp.Domain.Repositories;
using Microsoft.AspNet.SignalR.Client;
using Smart.SMSSend.Interface;
using Smart.SMSSend.Model;
using System;
using System.Configuration;
using System.Linq;
using System.Timers;

namespace Smart.SMSSend
{
    /// <inheritdoc />
    /// <summary>
    /// 短信发送服务
    /// </summary>
    public class SmsSendService : ISingletonDependency
    {

        //服务代理
        private IHubProxy Hub { get; set; }

        private HubConnection hubConnection { get; set; }

        private readonly string _serverUrl;

        private readonly Timer _timer;

        private readonly IRedis _redis;

        private readonly ISendMessage _send;

        private readonly IRepository<SmsConfigs> _resp;

        public SmsSendService(IRedis redis, ISendMessage send, IRepository<SmsConfigs> resp)
        {
            _serverUrl = ConfigurationManager.ConnectionStrings["Signalr"].ConnectionString;

            _timer = new Timer(30000) { Enabled = true };

            _timer.Elapsed += _timer_Elapsed;

            _redis = redis;

            _send = send;

            _resp = resp;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            if (hubConnection.State == ConnectionState.Connected)
                return;
            ConnectionServer();
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
                Console.WriteLine(e);
                //throw;
            }
        }

        private void Receive(string identify, ReceiveDataModel model)
        {
            //解析model
            var sendModel = _redis.GetSmsSendModel(model);

            if (sendModel == null) return;
            //调用服务发送短信
            if (!sendModel.IsSend) return;
            //短信成功后更新oracle表
            if (_send.SendMsg(sendModel) == "000000")
            {
                //更新数据库表
                var configModel = _resp.FirstOrDefault(a => a.Monitor.BMID == sendModel.StationKey);
                if (configModel != null)
                {
                    configModel.LASTTIME = sendModel.SaveTime;
                    configModel.LASTVALUE = sendModel.Value;
                    _resp.Update(configModel);
                }
                //更新redis缓存
            };
        }



        private void HubConnection_Closed()
        {
            //定时
            _timer.Start();
        }

        public void Start()
        {
            ConnectionServer();
        }

        public void Stop()
        {

        }
    }
}
