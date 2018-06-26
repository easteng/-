/*********************************************
* 命名空间:Smart.SMSSend.Interface
* 功 能： redis 接口
* 接 口： IRedis
* 作 者:  东腾
* 时 间： 2018/6/14 14:39:52 
**********************************************
*/
using Smart.SMSSend.Model;
using System;
using System.Linq;

namespace Smart.SMSSend.Interface
{
    public interface IRedis
    {
        /// <summary>
        /// 根据当前监测数据获取短信发送信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        SmsSendModel GetSmsSendModel(ReceiveDataModel model);
    }
}
