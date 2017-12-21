/***************************************************
*
* Data Access Layer Of Easy Framework
* FileName : Tdms_Info.generate.cs 
* CreateTime : 2014-10-09 10:00:23 
* Version : V 1.01
* E_Mail : 6e9e@163.com
* Blog : http://www.cnblogs.com/fineblog/
* Copyright (C) Winner(深圳-乾海盛世)
* 
***************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Winner.Framework.Core;
using Winner.Framework.Core.DataAccess;
using Winner.Framework.Core.DataAccess.Oracle;
namespace Winner.YXH.DataMonitorSystem.DataAccess
{
    /// <summary>
    /// Data Access Layer Object Of Tdms_Info
    /// </summary>
    public partial class Tdms_Info : DataAccessBase
    {
        /// <summary>
        /// 根据栏目ID查询栏目信息
        /// </summary>
        /// <returns></returns>
        public bool SelectByChannelId(int channelId)
        {
            string sql = "SELECT * FROM TDMS_INFO  WHERE CHANNEL_ID = :CHANNEL_ID";
            AddParameter("CHANNEL_ID", channelId);
            return SelectBySql(sql);
        }
    }

    /// <summary>
    /// Data Access Layer Object Collection Of Tdms_Info
    /// </summary>
    public partial class Tdms_InfoCollection : DataAccessCollectionBase
    {
        /// <summary>
        /// 条件查询SQL信息表的信息
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool ListByAppId(string title, int channelId)
        {
            string sql = @"SELECT S1.ID,
       S1.TITLE,
       S1.CHANNEL_ID,
       S1.SQL_CONTEXT,
       S1.STATE,
       S1.OPERAOT_ID,
       S3.CHANNEL_NAME,
       S2.NODECODE,
       S2.NODENAME,
       S1.REMARK,
       S1.CREATE_TIME
  FROM TDMS_INFO S1
  LEFT JOIN VNET_REGINFO S2
    ON S2.NODEID = S1.OPERAOT_ID
  LEFT JOIN TDMS_CHANNEL S3
    ON S3.CHANNEL_ID = S1.CHANNEL_ID
 WHERE 1 = 1 ";

            if (title != "")
            {
                sql += " AND S1.CHANNEL_ID=:CHANNEL_ID";
                AddParameter(Tdms_Info._CHANNEL_ID, channelId);
            }
            else
            {
                sql += " AND S1.CHANNEL_ID=:CHANNEL_ID OR  S3.FATHER_ID=:CHANNEL_ID";
                AddParameter(Tdms_Info._CHANNEL_ID, channelId);
            }
            if (!string.IsNullOrEmpty(title))
            {
                sql += " AND TITLE LIKE'%'||:TITLE||'%'";
                AddParameter(Tdms_Info._TITLE, title);

            }
            return ListBySql(sql);
        }
        //执行SQL语句
        public int ExecuteSQL(string sql)
        {
            sql = "SELECT COUNT(0) FROM (" + sql + ")";
            return base.GetIntValue(sql).Value;
        }
        public bool CheckSQLCommand(string sql)
        {
            sql = "SELECT 0 FROM (" + sql + ") WHERE ROWNUM=1";
            return base.ListBySql(sql);
        }
    }
}