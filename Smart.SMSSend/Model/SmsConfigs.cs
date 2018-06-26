using Abp.Domain.Entities;
using Abp.NHibernate.EntityMappings;
using System;
using System.Linq;

namespace Smart.SMSSend.Model
{
    public class SmsConfigMap : EntityMap<SmsConfigs>
    {
        public SmsConfigMap()
        : base("smart_sms_config")
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.INTERVAL);
            Map(x => x.CHANGEDIFF);
            Map(x => x.MSGTYPE);
            Map(x => x.TEMPLATEID);
            Map(x => x.EXTENDCODE);
            Map(x => x.LASTTIME);
            Map(x => x.ISENABLED);
            Map(x => x.LASTVALUE);
            References<BasicMonitorRecord>(o => o.Monitor).Not.LazyLoad().Column("monitor_id");
        }
    }
    public class SmsConfigs : Entity<int>
    {
        /// <summary>
        /// 监测点信息
        /// </summary>
        public virtual BasicMonitorRecord Monitor { get; set; }
        /// <summary>
        /// 发送频率
        /// </summary>
        public virtual decimal INTERVAL { get; set; }
        /// <summary>
        /// 变化差值
        /// </summary>
        public virtual decimal CHANGEDIFF { get; set; }
        /// <summary>
        /// 短信类型
        /// </summary>
        public virtual string MSGTYPE { get; set; }
        /// <summary>
        /// 模板编号
        /// </summary>
        public virtual string TEMPLATEID { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public virtual string EXTENDCODE { get; set; }
        /// <summary>
        /// 最后一次发送时间
        /// </summary>
        public virtual DateTime LASTTIME { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual int ISENABLED { get; set; }
        /// <summary>
        /// 最后一次发送值
        /// </summary>
        public virtual decimal LASTVALUE { get; set; }
    }
}
