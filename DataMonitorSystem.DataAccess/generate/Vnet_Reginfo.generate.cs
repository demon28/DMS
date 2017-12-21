/***************************************************
*
* Data Access Layer Of Winner Framework
* FileName : Vnet_Reginfo.generate.cs 
* Version : V 1.1.0
* Author：架构师 曾杰(Jie)
* E_Mail : 6e9e@163.com
* Tencent QQ：554044818
* Blog : http://www.cnblogs.com/fineblog/
* Company ：深圳市乾海盛世移动支付有限公司
* Copyright (C) Winner研发中心
* CreateTime : 2016-08-30 14:38:56  
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
    /// Data Access Layer Object Of Vnet_Reginfo
    /// </summary>
    public partial class Vnet_Reginfo : DataAccessBase
    {
        #region 默认构造

        public Vnet_Reginfo() : base() { }

        public Vnet_Reginfo(DataRow dataRow)
            : base(dataRow) { }

        #endregion 默认构造

        #region 对应表结构的常量属性
        
		public const string _NODEID="NODEID";
		public const string _NODECODE="NODECODE";
		public const string _NODENAME="NODENAME";
		public const string _ISCONFIRMED="ISCONFIRMED";
		public const string _INTRODUCER="INTRODUCER";
		public const string _CREATETIME="CREATETIME";
		public const string _STATUS="STATUS";
		public const string _AUTHTIME="AUTHTIME";
		public const string _PHOTOURL="PHOTOURL";
		public const string _NODELEVEL="NODELEVEL";
		public const string _EMAIL="EMAIL";
		public const string _MOBILENO="MOBILENO";
		public const string _REMARKS="REMARKS";
		public const string _DATA_SOURCE="DATA_SOURCE";

    
        public const string _TABLENAME="Vnet_Reginfo";
        #endregion 对应表结构的常量属性

        #region 公开属性
        
		/// <summary>
		/// [default:0]
		/// </summary>
		public int Nodeid
		{
			get { return Convert.ToInt32(DataRow[_NODEID]); }
			set { setProperty(_NODEID,value); }
		}
		/// <summary>
		/// [default:string.Empty]
		/// </summary>
		public string Nodecode
		{
			get { return DataRow[_NODECODE].ToString(); }
			set { setProperty(_NODECODE,value); }
		}
		/// <summary>
		/// [default:string.Empty]
		/// </summary>
		public string Nodename
		{
			get { return DataRow[_NODENAME].ToString(); }
			set { setProperty(_NODENAME,value); }
		}
		/// <summary>
		/// [default:0]
		/// </summary>
		public int Isconfirmed
		{
			get { return Convert.ToInt32(DataRow[_ISCONFIRMED]); }
			set { setProperty(_ISCONFIRMED,value); }
		}
		/// <summary>
		/// [default:DBNull.Value]
		/// </summary>
		public int? Introducer
		{
			get { return Helper.ToInt32(DataRow[_INTRODUCER]); }
			set { setProperty(_INTRODUCER,Helper.FromInt32(value)); }
		}
		/// <summary>
		/// [default:new DateTime()]
		/// </summary>
		public DateTime Createtime
		{
			get { return Convert.ToDateTime(DataRow[_CREATETIME].ToString()); }
		}
		/// <summary>
		/// [default:0]
		/// </summary>
		public int Status
		{
			get { return Convert.ToInt32(DataRow[_STATUS]); }
			set { setProperty(_STATUS,value); }
		}
		/// <summary>
		/// [default:DBNull.Value]
		/// </summary>
		public DateTime? Authtime
		{
			get { return Helper.ToDateTime(DataRow[_AUTHTIME]); }
			set { setProperty(_AUTHTIME,Helper.FromDateTime(value)); }
		}
		/// <summary>
		/// [default:string.Empty]
		/// </summary>
		public string Photourl
		{
			get { return DataRow[_PHOTOURL].ToString(); }
			set { setProperty(_PHOTOURL,value); }
		}
		/// <summary>
		/// [default:0]
		/// </summary>
		public int Nodelevel
		{
			get { return Convert.ToInt32(DataRow[_NODELEVEL]); }
			set { setProperty(_NODELEVEL,value); }
		}
		/// <summary>
		/// [default:string.Empty]
		/// </summary>
		public string Email
		{
			get { return DataRow[_EMAIL].ToString(); }
			set { setProperty(_EMAIL,value); }
		}
		/// <summary>
		/// [default:string.Empty]
		/// </summary>
		public string Mobileno
		{
			get { return DataRow[_MOBILENO].ToString(); }
			set { setProperty(_MOBILENO,value); }
		}
		/// <summary>
		/// [default:string.Empty]
		/// </summary>
		public string Remarks
		{
			get { return DataRow[_REMARKS].ToString(); }
			set { setProperty(_REMARKS,value); }
		}
		/// <summary>
		/// [default:DBNull.Value]
		/// </summary>
		public int? DataSource
		{
			get { return Helper.ToInt32(DataRow[_DATA_SOURCE]); }
			set { setProperty(_DATA_SOURCE,Helper.FromInt32(value)); }
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
			dt.Columns.Add(_NODEID, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_NODECODE, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_NODENAME, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_ISCONFIRMED, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_INTRODUCER, typeof(int)).DefaultValue = DBNull.Value;
			dt.Columns.Add(_CREATETIME, typeof(DateTime)).DefaultValue = new DateTime();
			dt.Columns.Add(_STATUS, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_AUTHTIME, typeof(DateTime)).DefaultValue = DBNull.Value;
			dt.Columns.Add(_PHOTOURL, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_NODELEVEL, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_EMAIL, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_MOBILENO, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_REMARKS, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_DATA_SOURCE, typeof(int)).DefaultValue = DBNull.Value;

            return dt.NewRow();
        }
        
        #endregion 私有成员
        
        #region 常用方法
        
		protected bool DeleteByCondition(string condition)
        {
            string sql = @"DELETE FROM VNET_REGINFO WHERE " + condition;
            return base.DeleteBySql(sql);
        }
		
        public bool Delete(int nodeid)
        {
            string condition = "NODEID=:NODEID";
            AddParameter(_NODEID, nodeid);
            return DeleteByCondition(condition);
        }
		
        public bool Delete()
        {
            string condition = "NODEID=:NODEID";
            AddParameter(_NODEID, Nodeid);
            return DeleteByCondition(condition);
        }

        public bool Insert()
        {
			Nodeid=base.GetSequence("SELECT SEQ_VNET_REGINFO.NEXTVAL FROM DUAL");
string sql=@"INSERT INTO
VNET_REGINFO(
  NODEID,
  NODECODE,
  NODENAME,
  ISCONFIRMED,
  INTRODUCER,
  STATUS,
  AUTHTIME,
  PHOTOURL,
  NODELEVEL,
  EMAIL,
  MOBILENO,
  REMARKS,
  DATA_SOURCE)
VALUES(
  :NODEID,
  :NODECODE,
  :NODENAME,
  :ISCONFIRMED,
  :INTRODUCER,
  :STATUS,
  :AUTHTIME,
  :PHOTOURL,
  :NODELEVEL,
  :EMAIL,
  :MOBILENO,
  :REMARKS,
  :DATA_SOURCE)";
			AddParameter(_NODEID,DataRow[_NODEID]);
			AddParameter(_NODECODE,DataRow[_NODECODE]);
			AddParameter(_NODENAME,DataRow[_NODENAME]);
			AddParameter(_ISCONFIRMED,DataRow[_ISCONFIRMED]);
			AddParameter(_INTRODUCER,DataRow[_INTRODUCER]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			AddParameter(_AUTHTIME,DataRow[_AUTHTIME]);
			AddParameter(_PHOTOURL,DataRow[_PHOTOURL]);
			AddParameter(_NODELEVEL,DataRow[_NODELEVEL]);
			AddParameter(_EMAIL,DataRow[_EMAIL]);
			AddParameter(_MOBILENO,DataRow[_MOBILENO]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			AddParameter(_DATA_SOURCE,DataRow[_DATA_SOURCE]);
            return base.InsertBySql(sql);
        }
		
		public bool Update()
        {
			return UpdateByCondition(string.Empty);
        }
		
        protected bool UpdateByCondition(string condition)
        {
            //移除主键标记
            ChangePropertys.Remove(_NODEID);
            
            if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE VNET_REGINFO SET");
            while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
            sql.AppendLine(" WHERE NODEID=:NODEID");
            AddParameter(_NODEID, DataRow[_NODEID]);
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
  NODEID,
  NODECODE,
  NODENAME,
  ISCONFIRMED,
  INTRODUCER,
  CREATETIME,
  STATUS,
  AUTHTIME,
  PHOTOURL,
  NODELEVEL,
  EMAIL,
  MOBILENO,
  REMARKS,
  DATA_SOURCE
FROM VNET_REGINFO
WHERE " + condition;
            return base.SelectBySql(sql);
        }

        public bool SelectByPK(int nodeid)
        {
            string condition = "NODEID=:NODEID";
            AddParameter(_NODEID, nodeid);
            return SelectByCondition(condition);
        }



        #endregion 常用方法
        
        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
    
    /// <summary>
    /// Data Access Layer Object Collection Of Vnet_Reginfo
    /// </summary>
    public partial class Vnet_ReginfoCollection : DataAccessCollectionBase
    {
        #region 默认构造
 
        public Vnet_ReginfoCollection() { }

        public Vnet_ReginfoCollection(DataTable table)
            : base(table) { }
            
        #endregion 默认构造
        
        #region 私有成员
        protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Vnet_Reginfo(DataTable.Rows[index]);
        }
        
        protected override DataTable BuildTable()
        {
            return new  Vnet_Reginfo().CloneSchemaOfTable();
        }
        
        protected override string TableName
        {
            get { return Vnet_Reginfo._TABLENAME; }
        }
        
        protected bool ListByCondition(string condition)
        {
            string sql = @"
SELECT
  NODEID,
  NODECODE,
  NODENAME,
  ISCONFIRMED,
  INTRODUCER,
  CREATETIME,
  STATUS,
  AUTHTIME,
  PHOTOURL,
  NODELEVEL,
  EMAIL,
  MOBILENO,
  REMARKS,
  DATA_SOURCE
FROM VNET_REGINFO
WHERE " + condition;
            return base.ListBySql(sql);
        }

        public bool ListByNodeid(int nodeid)
        {
            string condition = "NODEID=:NODEID";
            AddParameter(Vnet_Reginfo._NODEID, nodeid);
            return ListByCondition(condition);
        }

        public bool ListAll()
        {
            string condition = "1=1";
            return ListByCondition(condition);
        }
        
        public bool DeleteByCondition(string condition)
        {
            string sql = "DELETE FROM VNET_REGINFO WHERE " + condition;
            return DeleteBySql(sql);
        }
        #endregion
        
        #region 公开成员
        public Vnet_Reginfo this[int index]
        {
            get
            {
                return new Vnet_Reginfo(DataTable.Rows[index]);
            }
        }

        public bool DeleteAll()
        {
            return this.DeleteByCondition(string.Empty);
        }
        
        #region Linq
        
        public Vnet_Reginfo Find(Predicate<Vnet_Reginfo> match)
        {
            foreach (Vnet_Reginfo item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Vnet_ReginfoCollection FindAll(Predicate<Vnet_Reginfo> match)
        {
            Vnet_ReginfoCollection list = new Vnet_ReginfoCollection();
            foreach (Vnet_Reginfo item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Vnet_Reginfo> match)
        {
            foreach (Vnet_Reginfo item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }

        public bool DeleteAt(Predicate<Vnet_Reginfo> match)
        {
            BeginTransaction();
            foreach (Vnet_Reginfo item in this)
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