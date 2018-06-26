/*********************************************
* 命名空间:Smart.SMSSend.Provide
* 功 能： 短信发送接口实现
* 类 名： SendMessageProvide
* 作 者:  东腾
* 时 间： 2018/6/14 15:18:12 
**********************************************
*/
using Abp.Dependency;
using DT.Data.Core.HttpRequest;
using Newtonsoft.Json;
using Smart.SMSSend.Interface;
using Smart.SMSSend.Model;
using System;
using System.Configuration;
using System.Linq;
using Method = DT.Data.Core.HttpRequest.Method;

namespace Smart.SMSSend.Provide
{
    public class SendMessageProvide : ISendMessage, ISingletonDependency
    {
        private readonly DtRestClient _dtRestClient;
        private DtRestRequest _dtRestRequest;
        public SendMessageProvide()
        {
            var url = ConfigurationManager.ConnectionStrings["SmsUrl"].ConnectionString;
            _dtRestClient = new DtRestClient(url);

        }
        public string SendMsg(SmsSendModel model)
        {
            try
            {
                _dtRestRequest = new DtRestRequest(Method.POST) { Resource = "application/x-www-form-urlencoded" };
                _dtRestRequest.AddParamete("phone", model.Phone);
                _dtRestRequest.AddParamete("message", model.MsgString);
                _dtRestRequest.AddParamete("templateId", model.MsgTempId);
                _dtRestRequest.AddParamete("systemCode", model.SystemCode);
                var response = _dtRestClient.Execute(_dtRestRequest);
                dynamic content = JsonConvert.DeserializeObject(response.Content);
                return content.resp.respCode;
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}
