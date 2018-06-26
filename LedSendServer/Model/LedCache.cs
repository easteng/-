/*********************************************
* 命名空间:LedSendServer.Model
* 功 能： Led缓存信息实体
* 类 名： LedCache
* 作 者:  东腾
* 时 间： 2018/6/25 11:46:08 
**********************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedSendServer.Model
{
    public class LedCache
    {
        public virtual int Id { get; set; }
        /// <summary>
        /// LED Id
        /// </summary>
        public virtual LedModel Led { get; set; }
        /// <summary>
        ///监测点ID
        /// </summary>
        public virtual BasicMonitorRecord MonitorRecord { get; set; }
    }


    public class LedModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        //public virtual int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string MC { get; set; }
        /// <summary>
        /// 通讯方式：网络通讯、串口通讯、短信
        /// </summary>
        public virtual string TXFSCODE { get; set; }
        /// <summary>
        /// 通讯方式：网络通讯、串口通讯
        /// </summary>
        public virtual string TXFS { get; set; }
        /// <summary>
        /// 通讯号码
        /// </summary>
        public virtual string PHONE { get; set; }
        /// <summary>
        /// 串口号
        /// </summary>
        public virtual string CKH { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public virtual string BTL { get; set; }
        /// <summary>
        /// 控制卡地址
        /// </summary>
        public virtual string KZKDZ { get; set; }
        /// <summary>
        /// 控制卡IP
        /// </summary>
        public virtual string KZKIP { get; set; }
        /// <summary>
        /// 本地端口
        /// </summary>
        public virtual int BDDK { get; set; }
        /// <summary>
        /// 坐标X
        /// </summary>
        public virtual double ZBX { get; set; }
        /// <summary>
        /// 坐标Y
        /// </summary>
        public virtual double ZBY { get; set; }
        /// <summary>
        /// 短信发送格式
        /// </summary>
        public virtual string MESSAGEFOMAT { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string BZ { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public virtual string EXTENDCODE { get; set; }
        /// <summary>
        /// 备用字段2
        /// </summary>
        public virtual string EXTENDCODE2 { get; set; }
        /// <summary>
        /// 备用字段3
        /// </summary>
        public virtual string EXTENDCODE3 { get; set; }
        /// <summary>
        /// 备用字段4
        /// </summary>
        public virtual string EXTENDCODE4 { get; set; }
        /// <summary>
        /// 备用字段5
        /// </summary>
        public virtual string EXTENDCODE5 { get; set; }
    }
}
