using Winner.YXH.DataMonitorSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMonitorSystem.Models
{
    public class VReginfoModel
    {
        #region  公开属性
        /// <summary>
        /// 用户ID
        /// </summary>
        public int NodeId { get; set; }
        /// <summary>
        /// 用户级别
        /// </summary>
        public int NodeLevel { get; set; }
        /// <summary>
        /// 推荐人
        /// </summary>
        public int Introducer { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string NodeCode { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否认证
        /// </summary>
        public int IsConfirmed { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 是否企业用户
        /// </summary>
        public int IsEnterprise { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileNo { get; set; }

        #endregion 公开属性
    }
}