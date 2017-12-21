 /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tdms_Admin.generate.cs 
 * CreateTime : 2014-11-06 16:46:54 
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
    /// Data Access Layer Object Of Tdms_Admin
    /// </summary>
    public partial class Tdms_Admin : DataAccessBase
    {
        #region 默认构造

        public Tdms_Admin() : base() { }

        public Tdms_Admin(DataRow dataRow)
            : base(dataRow) { }

        #endregion 默认构造

        #region 对应表结构的常量属性
        
		public const string _ID="ID";
		public const string _INFO_ID="INFO_ID";
		public const string _NODE_ID="NODE_ID";
		public const string _REMARK="REMARK";
		public const string _CREATE_TIME="CREATE_TIME";
		public const string _IS_SEND="IS_SEND";
		public const string _SEND_TYPE="SEND_TYPE";
		public const string _EMAIL="EMAIL";

    
        public const string _TABLENAME="Tdms_Admin";
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
		/// SQL与管理员关系表 外键
		/// [default:0]
		/// </summary>
		public int InfoId
		{
			get { return Convert.ToInt32(DataRow[_INFO_ID]); }
			set { DataRow[_INFO_ID] = value; }
		}
		/// <summary>
		/// 管理员与管理员关系表外键
		/// [default:0]
		/// </summary>
		public int NodeId
		{
			get { return Convert.ToInt32(DataRow[_NODE_ID]); }
			set { DataRow[_NODE_ID] = value; }
		}
		/// <summary>
		/// 备注
		/// [default:string.Empty]
		/// </summary>
		public string Remark
		{
			get { return DataRow[_REMARK].ToString(); }
			set { DataRow[_REMARK] = value; }
		}
		/// <summary>
		/// 录入时间
		/// [default:new DateTime()]
		/// </summary>
		public DateTime CreateTime
		{
			get { return Convert.ToDateTime(DataRow[_CREATE_TIME].ToString()); }
		}
		/// <summary>
		/// 休息时间是否发送:不发送=0,发送=1,0
		/// [default:0]
		/// </summary>
		public SqlIsSend IsSend
		{
            get { return (SqlIsSend)int.Parse(DataRow[_IS_SEND].ToString()); }
			set { DataRow[_IS_SEND] = (int)value; }
		}
		/// <summary>
		/// 发送方式:邮件=1,短信=2,
		/// [default:0]
		/// </summary>
		public SqlSendType SendType
		{
            get { return (SqlSendType)int.Parse(DataRow[_SEND_TYPE].ToString()); }
			set { DataRow[_SEND_TYPE] = (int)value; }
		}
		/// <summary>
		/// 电子邮箱
		/// [default:string.Empty]
		/// </summary>
		public string Email
		{
			get { return DataRow[_EMAIL].ToString(); }
			set { DataRow[_EMAIL] = value; }
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
			dt.Columns.Add(_INFO_ID, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_NODE_ID, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_REMARK, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_CREATE_TIME, typeof(DateTime)).DefaultValue = new DateTime();
			dt.Columns.Add(_IS_SEND, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_SEND_TYPE, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_EMAIL, typeof(string)).DefaultValue = string.Empty;

            return dt.NewRow();
        }
        
        #endregion 私有成员
        
        #region 常用方法
        
		protected bool DeleteByCondition(string condition)
        {
            string sql = @"DELETE FROM TDMS_ADMIN WHERE " + condition;
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
			Id=base.GetSequence("SELECT SEQ_TDMS_ADMIN.NEXTVAL FROM DUAL");
string sql=@"INSERT INTO
TDMS_ADMIN(
  ID,
  INFO_ID,
  NODE_ID,
  REMARK,
  IS_SEND,
  SEND_TYPE,
  EMAIL)
VALUES(
  :ID,
  :INFO_ID,
  :NODE_ID,
  :REMARK,
  :IS_SEND,
  :SEND_TYPE,
  :EMAIL)";
			AddParameter(_ID,DataRow[_ID]);
			AddParameter(_INFO_ID,DataRow[_INFO_ID]);
			AddParameter(_NODE_ID,DataRow[_NODE_ID]);
			AddParameter(_REMARK,DataRow[_REMARK]);
			AddParameter(_IS_SEND,DataRow[_IS_SEND]);
			AddParameter(_SEND_TYPE,DataRow[_SEND_TYPE]);
			AddParameter(_EMAIL,DataRow[_EMAIL]);
            return base.InsertBySql(sql);
        }
		
		public bool Update()
        {
			return UpdateByCondition(string.Empty);
        }
		
        protected bool UpdateByCondition(string condition)
        {
            string sql = @"UPDATE Tdms_Admin SET
 INFO_ID=:INFO_ID,
 NODE_ID=:NODE_ID,
 REMARK=:REMARK,
 IS_SEND=:IS_SEND,
 SEND_TYPE=:SEND_TYPE,
 EMAIL=:EMAIL
WHERE ID=:ID ";
			AddParameter(_ID,DataRow[_ID]);
			AddParameter(_INFO_ID,DataRow[_INFO_ID]);
			AddParameter(_NODE_ID,DataRow[_NODE_ID]);
			AddParameter(_REMARK,DataRow[_REMARK]);
			AddParameter(_IS_SEND,DataRow[_IS_SEND]);
			AddParameter(_SEND_TYPE,DataRow[_SEND_TYPE]);
			AddParameter(_EMAIL,DataRow[_EMAIL]);

            if (!string.IsNullOrEmpty(condition))
                sql += " AND " + condition;
            return base.UpdateBySql(sql);
        }

        protected bool SelectByCondition(string condition)
        {
            string sql = @"
SELECT
  ID,
  INFO_ID,
  NODE_ID,
  REMARK,
  CREATE_TIME,
  IS_SEND,
  SEND_TYPE,
  EMAIL
FROM TDMS_ADMIN
WHERE " + condition;
            return base.SelectBySql(sql);
        }

        public bool SelectByPK(int id)
        {
            string condition = "ID=:ID";
            AddParameter(_ID, id);
            return SelectByCondition(condition);
        }
		public Tdms_Info Get_Tdms_Info_ByInfoId()
		{
			Tdms_Info da=new Tdms_Info();
			da.SelectByPK(InfoId);
			return da;
		}
        //public Tnet_Reginfo Get_Tnet_Reginfo_ByNodeId()
        //{
        //    Tnet_Reginfo da=new Tnet_Reginfo();
        //    da.SelectByPK(NodeId);
        //    return da;
        //}



        #endregion 常用方法
        
        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
    
    /// <summary>
    /// Data Access Layer Object Collection Of Tdms_Admin
    /// </summary>
    public partial class Tdms_AdminCollection : DataAccessCollectionBase
    {
        #region 默认构造
 
        public Tdms_AdminCollection() { }

        public Tdms_AdminCollection(DataTable table)
            : base(table) { }
            
        #endregion 默认构造
        
        #region 私有成员
        protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tdms_Admin(DataTable.Rows[index]);
        }
        
        protected override DataTable BuildTable()
        {
            return new  Tdms_Admin().CloneSchemaOfTable();
        }
        
        protected override string TableName
        {
            get { return Tdms_Admin._TABLENAME; }
        }
        
        protected bool ListByCondition(string condition)
        {
            string sql = @"
SELECT
  ID,
  INFO_ID,
  NODE_ID,
  REMARK,
  CREATE_TIME,
  IS_SEND,
  SEND_TYPE,
  EMAIL
FROM TDMS_ADMIN
WHERE " + condition;
            return base.ListBySql(sql);
        }

        public bool ListById(int id)
        {
            string condition = "ID=:ID";
            AddParameter(Tdms_Admin._ID, id);
            return ListByCondition(condition);
        }

        public bool ListAll()
        {
            string condition = "1=1";
            return ListByCondition(condition);
        }
        
        public bool DeleteByCondition(string condition)
        {
            string sql = "DELETE FROM TDMS_ADMIN WHERE " + condition;
            return DeleteBySql(sql);
        }
        #endregion
        
        #region 公开成员
        public Tdms_Admin this[int index]
        {
            get
            {
                return new Tdms_Admin(DataTable.Rows[index]);
            }
        }

        public bool DeleteAll()
        {
            return this.DeleteByCondition(string.Empty);
        }
        
        #region Linq
        
        public Tdms_Admin Find(Predicate<Tdms_Admin> match)
        {
            foreach (Tdms_Admin item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tdms_AdminCollection FindAll(Predicate<Tdms_Admin> match)
        {
            Tdms_AdminCollection list = new Tdms_AdminCollection();
            foreach (Tdms_Admin item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tdms_Admin> match)
        {
            foreach (Tdms_Admin item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }

        public bool DeleteAt(Predicate<Tdms_Admin> match)
        {
            BeginTransaction();
            foreach (Tdms_Admin item in this)
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