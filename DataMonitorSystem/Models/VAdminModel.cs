using Winner.YXH.DataMonitorSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMonitorSystem.Models
{
    public class VAdminModel
    {
        #region 公开属性
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// SQL语句ID
        /// </summary>
        public int InfoId { get; set; }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int NodeId { get; set; }
        /// <summary>
        /// 管理员
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        public SqlIsSend IsSend
        {
            get;
            set;
        }
        /// <summary>
        /// 发送方式:邮件=1,短信=2,
        /// [default:0]
        /// </summary>
        public SqlSendType SendType
        {
            get;
            set;
        }
        public SqlSendType NotifySMS
        {
            get;
            set;
        }
        public SqlSendType NotifyEmail
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }

        #endregion 公开属性
    }
}