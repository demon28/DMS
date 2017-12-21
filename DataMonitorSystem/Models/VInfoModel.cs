using Winner.YXH.DataMonitorSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMonitorSystem.Models
{
    public class VInfoModel
    {
        #region 公开属性
        /// <summary>
        /// SQL信息ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 系统id
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// sql语句
        /// </summary>
        public string SqlContext { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public SQLStatus State { get; set; }
        /// <summary>
        /// 操作员ID
        /// </summary>
        public int OperaotId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        #endregion 公开属性
    }
}