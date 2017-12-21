 /***************************************************
 *
 * Data Access Layer Of Easy Framework
 * FileName : Tapp_System.generate.cs 
 * CreateTime : 2014-10-09 10:00:34 
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
    /// Data Access Layer Object Of Tapp_System
    /// </summary>
    public partial class Tapp_System : DataAccessBase
    {
        
    }

    /// <summary>
    /// Data Access Layer Object Collection Of Tapp_System
    /// </summary>
    public partial class Tapp_SystemCollection : DataAccessCollectionBase
    {
        /// <summary>
        /// 条件查询系统表的信息
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool ListByAppName(string AppName)
        {
            string sql=" 1=1";
            if (!string.IsNullOrEmpty(AppName))
            {
                sql+=" AND APPNAME=:APPNAME";
                AddParameter(Tapp_System._APPNAME, AppName);
            }
            sql+=" ORDER BY APPID ASC";
            return ListByCondition(sql);
        }
        public bool ListByX(string keyword)
        {
            string sql = @"SELECT APPID,DISPLAYNAME||'['||APPID||']' TEXT FROM TAPP_SYSTEM WHERE 1=1";
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND APPID||DISPLAYNAME LIKE '%'||:KEYWORD||'%'";
                AddParameter("KEYWORD", keyword);
            }
            return ListBySql(sql);
        }
    }
}