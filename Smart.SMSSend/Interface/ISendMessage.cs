/*********************************************
* 命名空间:Smart.SMSSend.Interface
* 功 能： rest服务操作接口
* 接 口： ISendMessage
* 作 者:  东腾
* 时 间： 2018/6/14 15:14:20 
**********************************************
*/
using Smart.SMSSend.Model;
using System;
using System.Linq;

namespace Smart.SMSSend.Interface
{
    public interface ISendMessage
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否发送成功</returns>
        string  SendMsg(SmsSendModel model);
    }
}
