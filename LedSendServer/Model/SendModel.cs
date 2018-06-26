/*********************************************
* 命名空间:LedSendServer.Model
* 功 能： Led内容发送实体
* 类 名： SendModel
* 作 者:  东腾
* 时 间： 2018/6/25 13:30:41 
**********************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace LedSendServer.Model
{
    public class SendModel
    {
        /// <summary>
        /// 控制卡号
        /// </summary>
        public string CardCode { get; set; }
        /// <summary>
        /// led内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否正常，正常情况进行随机获取正的字库
        /// </summary>
        public bool IsNormal { get; set; }
        /// <summary>
        /// 是否发送短信
        /// </summary>
        public bool IsSendMsg { get; set; }
        /// <summary>
        /// 监测点编号
        /// </summary>
        public string StationKey { get; set; }
        /// <summary>
        /// 监测点名称
        /// </summary>
        public string StationName { get; set; }

        public int Level { get; set; }
    }
}
