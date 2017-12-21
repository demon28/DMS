/***************************************************
*
* Data Access Layer Of Winner Framework
* FileName : Tdms_Winservice.generate.cs 
* CreateTime : 2014-10-28 15:36:00 
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
using Winner.Framework.Utils;
namespace Winner.YXH.DataMonitorSystem.DataAccess
{
    /// <summary>
    /// Data Access Layer Object Of Tdms_Winservice
    /// </summary>
    public partial class Tdms_Winservice : DataAccessBase
    {
        public DateTime? GetMinTime()
        {
            string sql = @"SELECT MIN(TW.NEXT_RUN_TIME) NEXT_RUN_TIME
  FROM TDMS_WINSERVICE TW
  JOIN TDMS_INFO TI
    ON TI.ID = TW.ID
 WHERE TI.STATE = 1";
            return base.GetStringValue(sql).Safe().ToNullableDateTime();
        }
        //Custom Extension Class
    }

    /// <summary>
    /// Data Access Layer Object Collection Of Tdms_Winservice
    /// </summary>
    public partial class Tdms_WinserviceCollection : DataAccessCollectionBase
    {
        public bool ListByNextTime()
        {
            string sql = @"SELECT TW.*, TI.TITLE, TI.SQL_CONTEXT
  FROM TDMS_WINSERVICE TW
  JOIN TDMS_INFO TI
    ON TI.ID = TW.ID
 WHERE TI.STATE = 1
   AND TW.NEXT_RUN_TIME <= SYSDATE
 ORDER BY TW.NEXT_RUN_TIME";
            return ListBySql(sql);
        }
        public bool ListByCycle(int? cycle)
        {
            string sql = @"SELECT TI.*,
       TW.CYCLE,
       TW.NEXT_RUN_TIME,
       TW.STATUS,
       TW.RUN_RESULT,
       TW.REAMRKS WREAMRKS,
       TC.CHANNEL_NAME
  FROM TDMS_INFO TI
   JOIN TDMS_WINSERVICE TW
    ON TI.ID = TW.ID
   JOIN TDMS_CHANNEL TC
    ON TC.CHANNEL_ID = TI.CHANNEL_ID
 WHERE 1 = 1";
            if (cycle.HasValue)
            {
                sql += " AND CYCLE=:CYCLE";
                AddParameter(Tdms_Winservice._CYCLE, cycle);
            }
            return ListBySql(sql);
        }
        //Custom Extension Class
    }
}