/*********************************************
* 命名空间:LedSendServer.Common
* 功 能： 数据解析接口
* 接 口： IResolution
* 作 者:  东腾
* 时 间： 2018/6/25 11:40:36 
**********************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LedSendServer.Model;
using Smart.SMSSend.Model;

namespace LedSendServer.Common
{
    public interface IResolution
    {
        List<SendModel> GetLedModel(Model.ReceiveDataModel model);

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SendMsg(SmsSendModel model);

        /// <summary>
        /// 发送Led信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SendLed(SendModel model);
    }
}
