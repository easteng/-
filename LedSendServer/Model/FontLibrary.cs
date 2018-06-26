/*********************************************
* 命名空间:LedSendServer.Model
* 功 能： redis 模块
* 类 名： FontLibrary
* 作 者:  东腾
* 时 间： 2018/6/25 11:48:29 
**********************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedSendServer.Model
{
    public class FontLibrary
    {
        /// <summary>
        /// 天气类型，0,：正常，1：雨天，2：雪天
        /// </summary>
        public virtual int WEATHERTYPE { get; set; }

        public virtual string WEATHERDESC { get; set; }
        /// <summary>
        /// 水位级别，范围之间用小短线分割，如1-10,2-13
        /// </summary>
        public virtual string WATERLEVEL { get; set; }
        /// <summary>
        /// 信息内容
        /// </summary>
        public virtual string CONTENT { get; set; }
        /// <summary>
        /// 备用1
        /// </summary>
        public virtual string EXTEND { get; set; }

        public virtual string EXTEND2 { get; set; }

        public virtual string EXTEND3 { get; set; }
    }
}
