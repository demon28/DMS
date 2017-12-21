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
using Winner.YXH.DataMonitorSystem.Entities;

namespace Winner.YXH.DataMonitorSystem.DataAccess
{
    /// <summary>
    /// Data Access Layer Object Of Tdms_Winservice
    /// </summary>
    public partial class Tdms_Winservice : DataAccessBase
    {
        #region 默认构造

        public Tdms_Winservice() : base() { }

        public Tdms_Winservice(DataRow dataRow)
            : base(dataRow) { }

        #endregion 默认构造

        #region 对应表结构的常量属性
        
		public const string _ID="ID";
		public const string _CYCLE="CYCLE";
		public const string _NEXT_RUN_TIME="NEXT_RUN_TIME";
		public const string _STATUS="STATUS";
		public const string _RUN_RESULT="RUN_RESULT";
		public const string _CREATE_TIME="CREATE_TIME";
		public const string _REAMRKS="REAMRKS";

    
        public const string _TABLENAME="Tdms_Winservice";
        #endregion 对应表结构的常量属性

        #region 公开属性
        
		/// <summary>
		/// 主键
		/// [default:0]
		/// </summary>
		public int Id
		{
			get { return Convert.ToInt32(DataRow[_ID]); }
			set { DataRow[_ID] = value; }
		}
		/// <summary>
		/// 周期,DeductCycle:Hour=0,Day=1,Week=2,Monthly=4,Year=8,
		/// [default:0]
		/// </summary>
		public DeductCycle Cycle
		{
            get { return (DeductCycle)int.Parse(DataRow[_CYCLE].ToString()); }
			set { DataRow[_CYCLE] = (int)value; }
		}
		/// <summary>
		/// 下次运行时间
		/// [default:new DateTime()]
		/// </summary>
		public DateTime NextRunTime
		{
			get { return Convert.ToDateTime(DataRow[_NEXT_RUN_TIME]); }
			set { DataRow[_NEXT_RUN_TIME] = value; }
		}
		/// <summary>
		/// 状态:成功 = 1,失败 = 2,运行中 = 3,
		/// [default:0]
		/// </summary>
		public RunStatus Status
		{
			get { return (RunStatus)int.Parse(DataRow[_STATUS].ToString()); }
			set { DataRow[_STATUS] = (int)value; }
		}
		/// <summary>
		/// 运行结果：失败=-1,成功但是没有数据=0,成功监控到n条数据=n
		/// [default:0]
		/// </summary>
		public int? RunResult
		{
			get { return Convert.ToInt32(DataRow[_RUN_RESULT]); }
			set { DataRow[_RUN_RESULT] = value; }
		}
		/// <summary>
		/// 创建时间
		/// [default:new DateTime()]
		/// </summary>
		public DateTime CreateTime
		{
			get { return Convert.ToDateTime(DataRow[_CREATE_TIME].ToString()); }
		}
		/// <summary>
		/// 备注
		/// [default:string.Empty]
		/// </summary>
		public string Reamrks
		{
			get { return DataRow[_REAMRKS].ToString(); }
			set { DataRow[_REAMRKS] = value; }
		}

        #endregion 公开属性
        
        #region 私有成员
        
        protected override string TableName
        {
            get { return _TABLENAME; }
        }

        protected override DataRow BuildRow()
        {
            DataTable dt = new DataTable(_TABLENAME);
			dt.Columns.Add(_ID, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_CYCLE, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_NEXT_RUN_TIME, typeof(DateTime)).DefaultValue = new DateTime();
			dt.Columns.Add(_STATUS, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_RUN_RESULT, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_CREATE_TIME, typeof(DateTime)).DefaultValue = new DateTime();
			dt.Columns.Add(_REAMRKS, typeof(string)).DefaultValue = string.Empty;

            return dt.NewRow();
        }
        
        #endregion 私有成员
        
        #region 常用方法
        
		protected bool DeleteByCondition(string condition)
        {
            string sql = @"DELETE FROM TDMS_WINSERVICE WHERE " + condition;
            return base.DeleteBySql(sql);
        }
		
        public bool Delete(int id)
        {
            string condition = "ID=:ID";
            AddParameter(_ID, id);
            return DeleteByCondition(condition);
        }
		
        public bool Delete()
        {
            string condition = "ID=:ID";
            AddParameter(_ID, Id);
            return DeleteByCondition(condition);
        }

        public bool Insert()
        {
string sql=@"INSERT INTO
TDMS_WINSERVICE(
  ID,
  CYCLE,
  NEXT_RUN_TIME,
  STATUS,
  RUN_RESULT,
  REAMRKS)
VALUES(
  :ID,
  :CYCLE,
  :NEXT_RUN_TIME,
  :STATUS,
  :RUN_RESULT,
  :REAMRKS)";
			AddParameter(_ID,DataRow[_ID]);
			AddParameter(_CYCLE,DataRow[_CYCLE]);
			AddParameter(_NEXT_RUN_TIME,DataRow[_NEXT_RUN_TIME]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			AddParameter(_RUN_RESULT,DataRow[_RUN_RESULT]);
			AddParameter(_REAMRKS,DataRow[_REAMRKS]);
            return base.InsertBySql(sql);
        }
		
		public bool Update()
        {
			return UpdateByCondition(string.Empty);
        }
		
        protected bool UpdateByCondition(string condition)
        {
            string sql = @"UPDATE Tdms_Winservice SET
 CYCLE=:CYCLE,
 NEXT_RUN_TIME=:NEXT_RUN_TIME,
 STATUS=:STATUS,
 RUN_RESULT=:RUN_RESULT,
 REAMRKS=:REAMRKS
WHERE ID=:ID ";
			AddParameter(_ID,DataRow[_ID]);
			AddParameter(_CYCLE,DataRow[_CYCLE]);
			AddParameter(_NEXT_RUN_TIME,DataRow[_NEXT_RUN_TIME]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			AddParameter(_RUN_RESULT,DataRow[_RUN_RESULT]);
			AddParameter(_REAMRKS,DataRow[_REAMRKS]);

            if (!string.IsNullOrEmpty(condition))
                sql += " AND " + condition;
            return base.UpdateBySql(sql);
        }

        protected bool SelectByCondition(string condition)
        {
            string sql = @"
SELECT
  ID,
  CYCLE,
  NEXT_RUN_TIME,
  STATUS,
  RUN_RESULT,
  CREATE_TIME,
  REAMRKS
FROM TDMS_WINSERVICE
WHERE " + condition;
            return base.SelectBySql(sql);
        }

        public bool SelectByPK(int id)
        {
            string condition = "ID=:ID";
            AddParameter(_ID, id);
            return SelectByCondition(condition);
        }
		public Tdms_Info Get_Tdms_Info_ById()
		{
			Tdms_Info da=new Tdms_Info();
			da.SelectByPK(Id);
			return da;
		}



        #endregion 常用方法
        
        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
    
    /// <summary>
    /// Data Access Layer Object Collection Of Tdms_Winservice
    /// </summary>
    public partial class Tdms_WinserviceCollection : DataAccessCollectionBase
    {
        #region 默认构造
 
        public Tdms_WinserviceCollection() { }

        public Tdms_WinserviceCollection(DataTable table)
            : base(table) { }
            
        #endregion 默认构造
        
        #region 私有成员
        protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tdms_Winservice(DataTable.Rows[index]);
        }
        
        protected override DataTable BuildTable()
        {
            return new  Tdms_Winservice().CloneSchemaOfTable();
        }
        
        protected override string TableName
        {
            get { return Tdms_Winservice._TABLENAME; }
        }
        
        protected bool ListByCondition(string condition)
        {
            string sql = @"
SELECT
  ID,
  CYCLE,
  NEXT_RUN_TIME,
  STATUS,
  RUN_RESULT,
  CREATE_TIME,
  REAMRKS
FROM TDMS_WINSERVICE
WHERE " + condition;
            return base.ListBySql(sql);
        }

        public bool ListById(int id)
        {
            string condition = "ID=:ID";
            AddParameter(Tdms_Winservice._ID, id);
            return ListByCondition(condition);
        }

        public bool ListAll()
        {
            string condition = "1=1";
            return ListByCondition(condition);
        }
        
        public bool DeleteByCondition(string condition)
        {
            string sql = "DELETE FROM TDMS_WINSERVICE WHERE " + condition;
            return DeleteBySql(sql);
        }
        #endregion
        
        #region 公开成员
        public Tdms_Winservice this[int index]
        {
            get
            {
                return new Tdms_Winservice(DataTable.Rows[index]);
            }
        }

        public bool DeleteAll()
        {
            return this.DeleteByCondition(string.Empty);
        }
        
        #region Linq
        
        public Tdms_Winservice Find(Predicate<Tdms_Winservice> match)
        {
            foreach (Tdms_Winservice item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tdms_WinserviceCollection FindAll(Predicate<Tdms_Winservice> match)
        {
            Tdms_WinserviceCollection list = new Tdms_WinserviceCollection();
            foreach (Tdms_Winservice item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tdms_Winservice> match)
        {
            foreach (Tdms_Winservice item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }

        public bool DeleteAt(Predicate<Tdms_Winservice> match)
        {
            BeginTransaction();
            foreach (Tdms_Winservice item in this)
            {
                item.ReferenceTransactionFrom(Transaction);
                if (!match(item))
                    continue;
                if (!item.Delete())
                {
                    Rollback();
                    return false;
                }
            }
            Commit();
            return true;
        }
        #endregion Linq
        #endregion
        
        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
} 