/***************************************************
*
* Data Access Layer Of Easy Framework
* FileName : Tdms_Admin.generate.cs 
* CreateTime : 2014-10-09 10:00:14 
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
    /// Data Access Layer Object Of Tdms_Admin
    /// </summary>
    public partial class Tdms_Admin : DataAccessBase
    {
        /// <summary>
        /// 根据管理员编号查询信息
        /// </summary>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public bool SelectById(int id)
        {
            string sql = @"
SELECT TA.ID,
       TA.INFO_ID,
       TA.NODE_ID,
       VR.NODECODE,
       VR.NODENAME,
       TA.REMARK,
       TA.CREATE_TIME
  FROM TDMS_ADMIN TA
  JOIN VNET_REGINFO VR
    ON TA.NODE_ID = VR.NODEID
 WHERE 1 = 1 ";
            sql += " AND ID=:ID";
            AddParameter(_ID, id);
            return SelectBySql(sql);
        }
        /// <summary>
        /// 验证该管理员是否已经对改sql具有管理权限
        /// </summary>
        /// <returns></returns>
        public bool ListByInfoAndNodeId(int InfoId, int NodeId)
        {
            string sql = " INFO_ID=:INFO_ID AND NODE_ID=:NODE_ID";
            AddParameter(Tdms_Admin._INFO_ID, InfoId);
            AddParameter(Tdms_Admin._NODE_ID, NodeId);
            return SelectByCondition(sql);
        }
        public bool SelectByInfoId(int InfoId)
        {
            string sql = @"SELECT 
* 
FROM TDMS_ADMIN  
WHERE INFO_ID=:INFO_ID";
            AddParameter(Tdms_Admin._INFO_ID, InfoId);
            return SelectBySql(sql);
        }
    }

    /// <summary>
    /// Data Access Layer Object Collection Of Tdms_Admin
    /// </summary>
    public partial class Tdms_AdminCollection : DataAccessCollectionBase
    {
        /// <summary>
        /// 查询管理员表的所有信息
        /// </summary>
        /// <returns></returns>
        public bool ListByInfoIdAndNodeName(int? InfoId, string NodeName)
        {
            string sql = @"
SELECT TA.ID,
       TA.INFO_ID,
       TI.TITLE,
       TA.NODE_ID,
       VR.NODECODE,
       VR.NODENAME,
       TA.REMARK,
       TA.IS_SEND,
       TA.SEND_TYPE,
       TA.CREATE_TIME
  FROM TDMS_ADMIN TA
  JOIN VNET_REGINFO VR
    ON TA.NODE_ID = VR.NODEID
  JOIN TDMS_INFO TI
    ON TA.INFO_ID = TI.ID
 WHERE 1 = 1 ";
            if (InfoId.HasValue)
            {
                sql += " AND INFO_ID =:INFO_ID";
                AddParameter(Tdms_Admin._INFO_ID, InfoId);
            }
            if (!string.IsNullOrEmpty(NodeName))
            {
                sql += " AND  NODENAME=:NODENAME";
                AddParameter(Vnet_Reginfo._NODENAME, NodeName);
            }

            return ListBySql(sql);
        }
        /// <summary>
        /// 验证该管理员是否已经对改sql具有管理权限
        /// </summary>
        /// <returns></returns>
        public bool ListByInfoAndNodeId(int InfoId, int NodeId)
        {
            string sql = @"SELECT 
*  
FROM TDMS_ADMIN  
WHERE INFO_ID=:INFO_ID AND NODE_ID=:NODE_ID";
            AddParameter(Tdms_Admin._INFO_ID, InfoId);
            AddParameter(Tdms_Admin._NODE_ID, NodeId);
            return ListBySql(sql);
        }
        public bool ListByInfoId(int InfoId)
        {
            string sql = @"SELECT 
* 
FROM TDMS_ADMIN  
WHERE INFO_ID=:INFO_ID";
            AddParameter(Tdms_Admin._INFO_ID, InfoId);
            return ListBySql(sql);
        }
       
    }
}