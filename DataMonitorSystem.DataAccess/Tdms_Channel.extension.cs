/***************************************************
*
* Data Access Layer Of Winner Framework
* FileName : Tdms_Channel.extension.cs 
* Version : V 1.1.0
* Author：架构师 曾杰(Jie)
* E_Mail : 6e9e@163.com
* Tencent QQ：554044818
* Blog : http://www.cnblogs.com/fineblog/
* Company ：深圳市乾海盛世移动支付有限公司
* Copyright (C) Winner研发中心
* CreateTime : 2016-08-22 10:23:23  
* 
***************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Winner.Framework.Core;
using Winner.Framework.Core.DataAccess;
using Winner.Framework.Core.DataAccess.Oracle;
using Winner.Framework.Utils;
namespace Winner.YXH.DataMonitorSystem.DataAccess
{
    /// <summary>
    /// Data Access Layer Object Of Tdms_Channel
    /// </summary>
    public partial class Tdms_Channel : DataAccessBase
    {
        //Custom Extension Class

    }

    /// <summary>
    /// Data Access Layer Object Collection Of Tdms_Channel
    /// </summary>
    public partial class Tdms_ChannelCollection : DataAccessCollectionBase
    {
        //Custom Extension Class

        /// <summary>
        /// 获取栏目信息
        /// </summary>
        /// <returns></returns>
        public bool ListByChaneel(int? channelId, string channelName)
        {
            string sql = "SELECT * FROM TDMS_CHANNEL WHERE 1=1";

            if (channelId.HasValue)
            {
                sql += " AND CHANNEL_ID=:CHANNEL_ID";
                AddParameter("CHANNEL_ID", channelId);
            }
            if (!string.IsNullOrEmpty(channelName))
            {
                sql += " AND  CHANNEL_NAME LIKE '%'||:CHANNEL_NAME||'%'";
                AddParameter("CHANNEL_NAME", channelName);
            }
            return ListBySql(sql);
        }

        /// <summary>
        /// 获取所有栏目名称
        /// </summary>
        /// <returns></returns>
        public bool listByChaneelName()
        {
            string sql = "SELECT T.CHANNEL_ID, T.CHANNEL_NAME FROM TDMS_CHANNEL T ";
            return ListBySql(sql);
        }

        /// <summary>
        /// 获取父级编号
        /// </summary>
        /// <returns></returns>
        public bool listByFatherId(int fatherId)
        {
            string condtion = "FATHER_ID=:FATHER_ID";
            AddParameter("FATHER_ID", fatherId);
            return ListByCondition(condtion);

        }

        /// <summary>
        /// 构造栏目树形
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public bool ListTreeByParentid(int? parentId)
        {
            string sql = @"SELECT T.*,
        (SELECT COUNT(0) FROM TDMS_CHANNEL WHERE FATHER_ID = T.CHANNEL_ID) CHILD_COUNT
   FROM TDMS_CHANNEL T
  WHERE 1 = 1 ";

            if (parentId.HasValue)
            {
                sql += " AND FATHER_ID=:FATHER_ID";
                AddParameter("FATHER_ID", parentId);
            }
            else
            {
                sql += " AND FATHER_ID =0";
            }
            sql += " ORDER BY T.CHANNEL_ID";
            return ListBySql(sql);

        }

        //        /// <summary>
        //        /// 构造栏目管理树形
        //        /// </summary>
        //        /// <param name="parentId"></param>
        //        /// <returns></returns>
        //        public bool ListTreeByChannelId(int? channelId)
        //        {
        //            string sql = @"SELECT T.*,
        //        (SELECT COUNT(0) FROM TDMS_CHANNEL WHERE FATHER_ID = T.CHANNEL_ID) CHILD_COUNT
        //   FROM TDMS_CHANNEL T
        //  WHERE 1 = 1 ";

        //            if (channelId.HasValue)
        //            {
        //                sql += " AND CHANNEL_ID=:CHANNEL_ID";
        //                AddParameter("CHANNEL_ID", channelId);
        //            }
        //            else
        //            {
        //                sql += " AND FATHER_ID =0";
        //            }
        //            sql += " ORDER BY T.CHANNEL_ID";
        //            return ListBySql(sql);

        //        }

    }
}