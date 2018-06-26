/*********************************************
* 命名空间:Smart.SMSSend.Model
* 功 能： 短信发送实体
* 类 名： SmsSendModel
* 作 者:  东腾
* 时 间： 2018/6/14 14:41:23 
**********************************************
*/
using System;
using System.Linq;

namespace Smart.SMSSend.Model
{
    public class SmsSendModel
    {
        /// <summary>
        /// 监测点编号
        /// </summary>
        public string StationKey { get; set; }
        /// <summary>
        /// 是否发送
        /// </summary>
        public bool IsSend { get; set; }
        /// <summary>
        /// 电话号码，多个号码用逗号隔开
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string MsgString { get; set; }
        /// <summary>
        /// 短信模板编号
        /// </summary>
        public string MsgTempId { get; set; }
        /// <summary>
        /// 系统编号，用户记录发送系统
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// 当前值
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// 监测时间
        /// </summary>
        public DateTime SaveTime { get; set; }
    }
}
