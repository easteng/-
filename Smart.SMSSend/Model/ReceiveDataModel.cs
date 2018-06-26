/*********************************************
* 命名空间:Smart.SMSSend.Model
* 功 能： Signalr推送的数据实体
* 类 名： ReceiveDataModel
* 作 者:  东腾
* 时 间： 2018/6/14 13:52:33 
**********************************************
*/
using System;
using System.Linq;

namespace Smart.SMSSend.Model
{
    public class ReceiveDataModel
    {
        /// <summary>
        /// 监测点编码
        /// </summary>
        public string StationKey { get; set; }
        /// <summary>
        /// 监测点名称
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 监测项编码
        /// </summary>
        public string TagKey { get; set; }
        /// <summary>
        /// 监测项名称
        /// </summary>
        public string TagName { get; set; }
        /// <summary>
        /// 监测项值
        /// </summary>
        public string TagValue { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Message;

        /// <summary>
        /// 督办时间
        /// </summary>
        public string DBTime;
        /// <summary>
        /// 报警级别
        /// </summary>
        public string Level;
        /// <summary>
        /// 检测项单位
        /// </summary>
        public string Units;

        /// <summary>
        /// 检测项时间
        /// </summary>
        public DateTime SaveTime;

        /// <summary>
        /// 监测点类型
        /// </summary>
        public string StationType { get; set; }

        /// <summary>
        /// 自动处置的报警id集合
        /// </summary>
        public string AlertIds { get; set; }

        /// <summary>
        /// 如果是报警，报警主键ID
        /// </summary>
        public string AlertId { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }

}
