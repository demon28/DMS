/***************************************************
*
* Data Access Layer Of Winner Framework
* FileName : Tdms_Info.generate.cs 
* Version : V 1.1.0
* Author：架构师 曾杰(Jie)
* E_Mail : 6e9e@163.com
* Tencent QQ：554044818
* Blog : http://www.cnblogs.com/fineblog/
* Company ：深圳市乾海盛世移动支付有限公司
* Copyright (C) Winner研发中心
* CreateTime : 2016-08-22 10:33:55  
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
using Winner.YXH.DataMonitorSystem.Entities;
namespace Winner.YXH.DataMonitorSystem.DataAccess
{
    /// <summary>
    /// Data Access Layer Object Of Tdms_Info
    /// </summary>
    public partial class Tdms_Info : DataAccessBase
    {
        #region 默认构造

        public Tdms_Info() : base() { }

        public Tdms_Info(DataRow dataRow)
            : base(dataRow) { }

        #endregion 默认构造

        #region 对应表结构的常量属性
        
		public const string _ID="ID";
		public const string _TITLE="TITLE";
		public const string _CHANNEL_ID="CHANNEL_ID";
		public const string _SQL_CONTEXT="SQL_CONTEXT";
		public const string _STATE="STATE";
		public const string _OPERAOT_ID="OPERAOT_ID";
		public const string _REMARK="REMARK";
		public const string _CREATE_TIME="CREATE_TIME";

    
        public const string _TABLENAME="Tdms_Info";
        #endregion 对应表结构的常量属性

        #region 公开属性
        
		/// <summary>
		/// 主键
		/// [default:0]
		/// </summary>
		public int Id
		{
			get { return Convert.ToInt32(DataRow[_ID]); }
			set { setProperty(_ID,value); }
		}
		/// <summary>
		/// 标题
		/// [default:string.Empty]
		/// </summary>
		public string Title
		{
			get { return DataRow[_TITLE].ToString(); }
			set { setProperty(_TITLE,value); }
		}
		/// <summary>
		/// 栏目ID
		/// [default:0]
		/// </summary>
		public int ChannelId
		{
			get { return Convert.ToInt32(DataRow[_CHANNEL_ID]); }
			set { setProperty(_CHANNEL_ID,value); }
		}
		/// <summary>
		/// SQL语句
		/// [default:string.Empty]
		/// </summary>
		public string SqlContext
		{
			get { return DataRow[_SQL_CONTEXT].ToString(); }
			set { setProperty(_SQL_CONTEXT,value); }
		}
		/// <summary>
		/// 状态
		/// [default:0]
		/// </summary>
		public SQLStatus State
		{
			get { return (SQLStatus)int.Parse(DataRow[_STATE].ToString()); }
			set { setProperty(_STATE,(int)value); }
		}
		/// <summary>
		/// 操作员
		/// [default:0]
		/// </summary>
		public int OperaotId
		{
			get { return Convert.ToInt32(DataRow[_OPERAOT_ID]); }
			set { setProperty(_OPERAOT_ID,value); }
		}
		/// <summary>
		/// 备注
		/// [default:string.Empty]
		/// </summary>
		public string Remark
		{
			get { return DataRow[_REMARK].ToString(); }
			set { setProperty(_REMARK,value); }
		}
		/// <summary>
		/// 录入时间
		/// [default:new DateTime()]
		/// </summary>
		public DateTime CreateTime
		{
			get { return Convert.ToDateTime(DataRow[_CREATE_TIME].ToString()); }
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
			dt.Columns.Add(_TITLE, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_CHANNEL_ID, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_SQL_CONTEXT, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_STATE, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_OPERAOT_ID, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_REMARK, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_CREATE_TIME, typeof(DateTime)).DefaultValue = new DateTime();

            return dt.NewRow();
        }
        
        #endregion 私有成员
        
        #region 常用方法
        
		protected bool DeleteByCondition(string condition)
        {
            string sql = @"DELETE FROM TDMS_INFO WHERE " + condition;
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
			Id=base.GetSequence("SELECT SEQ_TDMS_INFO.NEXTVAL FROM DUAL");
string sql=@"INSERT INTO
TDMS_INFO(
  ID,
  TITLE,
  CHANNEL_ID,
  SQL_CONTEXT,
  STATE,
  OPERAOT_ID,
  REMARK)
VALUES(
  :ID,
  :TITLE,
  :CHANNEL_ID,
  :SQL_CONTEXT,
  :STATE,
  :OPERAOT_ID,
  :REMARK)";
			AddParameter(_ID,DataRow[_ID]);
			AddParameter(_TITLE,DataRow[_TITLE]);
			AddParameter(_CHANNEL_ID,DataRow[_CHANNEL_ID]);
			AddParameter(_SQL_CONTEXT,DataRow[_SQL_CONTEXT]);
			AddParameter(_STATE,DataRow[_STATE]);
			AddParameter(_OPERAOT_ID,DataRow[_OPERAOT_ID]);
			AddParameter(_REMARK,DataRow[_REMARK]);
            return base.InsertBySql(sql);
        }
		
		public bool Update()
        {
			return UpdateByCondition(string.Empty);
        }
		
        protected bool UpdateByCondition(string condition)
        {
            //移除主键标记
            ChangePropertys.Remove(_ID);
            
            if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TDMS_INFO SET");
            while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
            sql.AppendLine(" WHERE ID=:ID");
            AddParameter(_ID, DataRow[_ID]);
            if (!string.IsNullOrEmpty(condition))
                sql.AppendLine(" AND " + condition);
                
            bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
        }

        protected bool SelectByCondition(string condition)
        {
            string sql = @"
SELECT
  ID,
  TITLE,
  CHANNEL_ID,
  SQL_CONTEXT,
  STATE,
  OPERAOT_ID,
  REMARK,
  CREATE_TIME
FROM TDMS_INFO
WHERE " + condition;
            return base.SelectBySql(sql);
        }

        public bool SelectByPK(int id)
        {
            string condition = "ID=:ID";
            AddParameter(_ID, id);
            return SelectByCondition(condition);
        }
        //public Tnet_Reginfo Get_Tnet_Reginfo_ByOperaotId()
        //{
        //    Tnet_Reginfo da=new Tnet_Reginfo();
        //    da.SelectByPK(OperaotId);
        //    return da;
        //}



        #endregion 常用方法
        
        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
    
    /// <summary>
    /// Data Access Layer Object Collection Of Tdms_Info
    /// </summary>
    public partial class Tdms_InfoCollection : DataAccessCollectionBase
    {
        #region 默认构造
 
        public Tdms_InfoCollection() { }

        public Tdms_InfoCollection(DataTable table)
            : base(table) { }
            
        #endregion 默认构造
        
        #region 私有成员
        protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tdms_Info(DataTable.Rows[index]);
        }
        
        protected override DataTable BuildTable()
        {
            return new  Tdms_Info().CloneSchemaOfTable();
        }
        
        protected override string TableName
        {
            get { return Tdms_Info._TABLENAME; }
        }
        
        protected bool ListByCondition(string condition)
        {
            string sql = @"
SELECT
  ID,
  TITLE,
  CHANNEL_ID,
  SQL_CONTEXT,
  STATE,
  OPERAOT_ID,
  REMARK,
  CREATE_TIME
FROM TDMS_INFO
WHERE " + condition;
            return base.ListBySql(sql);
        }

        public bool ListById(int id)
        {
            string condition = "ID=:ID";
            AddParameter(Tdms_Info._ID, id);
            return ListByCondition(condition);
        }

        public bool ListAll()
        {
            string condition = "1=1";
            return ListByCondition(condition);
        }
        
        public bool DeleteByCondition(string condition)
        {
            string sql = "DELETE FROM TDMS_INFO WHERE " + condition;
            return DeleteBySql(sql);
        }
        #endregion
        
        #region 公开成员
        public Tdms_Info this[int index]
        {
            get
            {
                return new Tdms_Info(DataTable.Rows[index]);
            }
        }

        public bool DeleteAll()
        {
            return this.DeleteByCondition(string.Empty);
        }
        
        #region Linq
        
        public Tdms_Info Find(Predicate<Tdms_Info> match)
        {
            foreach (Tdms_Info item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tdms_InfoCollection FindAll(Predicate<Tdms_Info> match)
        {
            Tdms_InfoCollection list = new Tdms_InfoCollection();
            foreach (Tdms_Info item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tdms_Info> match)
        {
            foreach (Tdms_Info item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }

        public bool DeleteAt(Predicate<Tdms_Info> match)
        {
            BeginTransaction();
            foreach (Tdms_Info item in this)
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