using Winner.YXH.DataMonitorSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMonitorSystem.Models
{
    public class VSystemModel
    {
        #region 公开属性
        /// <summary>
        /// 行业ID
        /// </summary>
        public int AppId { get; set; }
        
        public string AppCode { get; set; }
        /// <summary>
        /// 行业名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 行业显示名称
        /// </summary>
        public string DisPlayName { get; set; }
        /// <summary>
        /// 行业简介
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 文件所在地址
        /// </summary>
        public string DllAddress { get; set; }
        /// <summary>
        /// 是否被删除
        /// </summary>
        public int Isdel { get; set; }
 
        #endregion 公开属性
    }
}