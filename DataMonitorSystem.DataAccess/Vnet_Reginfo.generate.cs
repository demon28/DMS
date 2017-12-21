/***************************************************
*
* Data Access Layer Of Winner Framework
* FileName : Vnet_Reginfo.generate.cs 
* CreateTime : 2014-10-09 13:57:17 
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
    /// Data Access Layer Object Of Vnet_Reginfo
    /// </summary>
    public partial class Vnet_Reginfo : DataAccessBase
    {


    }

    /// <summary>
    /// Data Access Layer Object Collection Of Vnet_Reginfo
    /// </summary>
    public partial class Vnet_ReginfoCollection : DataAccessCollectionBase
    {
        /// <summary>
        /// 条件查询用户信息表的信息
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool ListByNodeCodeAndNodeName(string nodecode, string nodename)
        {
            string  condition = " 1=1";
            if (!string.IsNullOrEmpty(nodecode))
            {
                condition+=" AND NODECODE=:NODECODE";
                AddParameter(Vnet_Reginfo._NODECODE, nodecode);
            }
            if (!string.IsNullOrEmpty(nodename))
            {
                condition+=" AND NODENAME=:NODENAME";
                AddParameter(Vnet_Reginfo._NODENAME, nodename);
            }
            return ListByCondition(condition);
        }
        /// <summary>
        /// 返回有效的管理员
        /// </summary>
        /// <returns></returns>
        public bool ListByLikeValue(int? nodeId, string likeValue)
        {
            string sql = @"SELECT 
VR.NODEID,
VR.NODECODE,
VR.NODENAME,
VR.NODENAME || '[' || VR.NODECODE || '] ' TEXT
  FROM VNET_REGINFO VR
 WHERE 1=1 ";
            if (nodeId.HasValue)
            {
                sql += " AND NODEID=:NODEID";
                AddParameter("NODEID", nodeId.Value);
            }
            if (!string.IsNullOrEmpty(likeValue))
            {
                sql += " AND VR.NODECODE || VR.NODENAME LIKE '%'||:NODENAME||'%'";
                AddParameter("NODENAME", likeValue);
            }
            return ListBySql(sql);
        }
    }
}