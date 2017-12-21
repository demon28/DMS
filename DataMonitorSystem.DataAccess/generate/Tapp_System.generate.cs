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
        #region 默认构造

        public Tapp_System() : base() { }

        public Tapp_System(DataRow dataRow)
            : base(dataRow) { }

        #endregion 默认构造

        #region 对应表结构的常量属性
        
		public const string _APPID="APPID";
		public const string _APPCODE="APPCODE";
		public const string _APPNAME="APPNAME";
		public const string _DISPLAYNAME="DISPLAYNAME";
		public const string _REMARKS="REMARKS";
		public const string _CREATETIME="CREATETIME";
		public const string _DLLADDRESS="DLLADDRESS";
		public const string _ISDEL="ISDEL";

    
        public const string _TABLENAME="Tapp_System";
        #endregion 对应表结构的常量属性

        #region 公开属性
        
		/// <summary>
		/// 行业ID，主键，由SEQ_TAPP_SYSTEM产生
		/// [default:0]
		/// </summary>
		public int Appid
		{
			get { return Convert.ToInt32(DataRow[_APPID]); }
			set { DataRow[_APPID] = value; }
		}
		/// <summary>
		/// 用于给用户区分不同行业的唯一代码，放GUID
		/// [default:string.Empty]
		/// </summary>
		public string Appcode
		{
			get { return DataRow[_APPCODE].ToString(); }
			set { DataRow[_APPCODE] = value; }
		}
		/// <summary>
		/// 行业名称，不可重复
		/// [default:string.Empty]
		/// </summary>
		public string Appname
		{
			get { return DataRow[_APPNAME].ToString(); }
			set { DataRow[_APPNAME] = value; }
		}
		/// <summary>
		/// 专用于显示的行业名称
		/// [default:string.Empty]
		/// </summary>
		public string Displayname
		{
			get { return DataRow[_DISPLAYNAME].ToString(); }
			set { DataRow[_DISPLAYNAME] = value; }
		}
		/// <summary>
		/// 行业简介
		/// [default:string.Empty]
		/// </summary>
		public string Remarks
		{
			get { return DataRow[_REMARKS].ToString(); }
			set { DataRow[_REMARKS] = value; }
		}
		/// <summary>
		/// 记录创建时间
		/// [default:DBNull.Value]
		/// </summary>
		public DateTime? Createtime
		{
			get { return Convert.ToDateTime(DataRow[_CREATETIME].ToString()); }
		}
		/// <summary>
		/// 记录该项目的bin文件所在位置
		/// [default:string.Empty]
		/// </summary>
		public string Dlladdress
		{
			get { return DataRow[_DLLADDRESS].ToString(); }
			set { DataRow[_DLLADDRESS] = value; }
		}
		/// <summary>
		/// 是否被删除 0不删除、1已删除
		/// [default:0]
		/// </summary>
		public int Isdel
		{
			get { return Convert.ToInt32(DataRow[_ISDEL]); }
			set { DataRow[_ISDEL] = value; }
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
			dt.Columns.Add(_APPID, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_APPCODE, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_APPNAME, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_DISPLAYNAME, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_REMARKS, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_CREATETIME, typeof(DateTime)).DefaultValue = DBNull.Value;
			dt.Columns.Add(_DLLADDRESS, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_ISDEL, typeof(int)).DefaultValue = 0;

            return dt.NewRow();
        }
        
        #endregion 私有成员
        
        #region 常用方法
        
		protected bool DeleteByCondition(string condition)
        {
            string sql = @"DELETE FROM TAPP_SYSTEM WHERE " + condition;
            return base.DeleteBySql(sql);
        }
		
        public bool Delete(int appid)
        {
            string condition = "APPID=:APPID";
            AddParameter(_APPID, appid);
            return DeleteByCondition(condition);
        }
		
        public bool Delete()
        {
            string condition = "APPID=:APPID";
            AddParameter(_APPID, Appid);
            return DeleteByCondition(condition);
        }

        public bool Insert()
        {
			Appid=base.GetSequence("SELECT SEQ_TAPP_SYSTEM.NEXTVAL FROM DUAL");
string sql=@"INSERT INTO
TAPP_SYSTEM(
  APPID,
  APPCODE,
  APPNAME,
  DISPLAYNAME,
  REMARKS,
  DLLADDRESS,
  ISDEL)
VALUES(
  :APPID,
  :APPCODE,
  :APPNAME,
  :DISPLAYNAME,
  :REMARKS,
  :DLLADDRESS,
  :ISDEL)";
			AddParameter(_APPID,DataRow[_APPID]);
			AddParameter(_APPCODE,DataRow[_APPCODE]);
			AddParameter(_APPNAME,DataRow[_APPNAME]);
			AddParameter(_DISPLAYNAME,DataRow[_DISPLAYNAME]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			AddParameter(_DLLADDRESS,DataRow[_DLLADDRESS]);
			AddParameter(_ISDEL,DataRow[_ISDEL]);
            return base.InsertBySql(sql);
        }
		
		public bool Update()
        {
			return UpdateByCondition(string.Empty);
        }
		
        protected bool UpdateByCondition(string condition)
        {
            string sql = @"UPDATE Tapp_System SET
 APPCODE=:APPCODE,
 APPNAME=:APPNAME,
 DISPLAYNAME=:DISPLAYNAME,
 REMARKS=:REMARKS,
 DLLADDRESS=:DLLADDRESS,
 ISDEL=:ISDEL
WHERE APPID=:APPID ";
			AddParameter(_APPID,DataRow[_APPID]);
			AddParameter(_APPCODE,DataRow[_APPCODE]);
			AddParameter(_APPNAME,DataRow[_APPNAME]);
			AddParameter(_DISPLAYNAME,DataRow[_DISPLAYNAME]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			AddParameter(_DLLADDRESS,DataRow[_DLLADDRESS]);
			AddParameter(_ISDEL,DataRow[_ISDEL]);

            if (!string.IsNullOrEmpty(condition))
                sql += " AND " + condition;
            return base.UpdateBySql(sql);
        }

        protected bool SelectByCondition(string condition)
        {
            string sql = @"
SELECT
  APPID,
  APPCODE,
  APPNAME,
  DISPLAYNAME,
  REMARKS,
  CREATETIME,
  DLLADDRESS,
  ISDEL
FROM TAPP_SYSTEM
WHERE " + condition;
            return base.SelectBySql(sql);
        }

        public bool SelectByPK(int appid)
        {
            string condition = "APPID=:APPID";
            AddParameter(_APPID, appid);
            return SelectByCondition(condition);
        }



        #endregion 常用方法
        
        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
    
    /// <summary>
    /// Data Access Layer Object Collection Of Tapp_System
    /// </summary>
    public partial class Tapp_SystemCollection : DataAccessCollectionBase
    {
        #region 默认构造
 
        public Tapp_SystemCollection() { }

        public Tapp_SystemCollection(DataTable table)
            : base(table) { }
            
        #endregion 默认构造
        
        #region 私有成员
        protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tapp_System(DataTable.Rows[index]);
        }
        
        protected override DataTable BuildTable()
        {
            return new  Tapp_System().CloneSchemaOfTable();
        }
        
        protected override string TableName
        {
            get { return Tapp_System._TABLENAME; }
        }
        
        protected bool ListByCondition(string condition)
        {
            string sql = @"
SELECT
  APPID,
  APPCODE,
  APPNAME,
  DISPLAYNAME,
  REMARKS,
  CREATETIME,
  DLLADDRESS,
  ISDEL
FROM TAPP_SYSTEM
WHERE " + condition;
            return base.ListBySql(sql);
        }

        public bool ListByAppid(int appid)
        {
            string condition = "APPID=:APPID";
            AddParameter(Tapp_System._APPID, appid);
            return ListByCondition(condition);
        }

        public bool ListAll()
        {
            string condition = "1=1";
            return ListByCondition(condition);
        }
        
        public bool DeleteByCondition(string condition)
        {
            string sql = "DELETE FROM TAPP_SYSTEM WHERE " + condition;
            return DeleteBySql(sql);
        }
        #endregion
        
        #region 公开成员
        public Tapp_System this[int index]
        {
            get
            {
                return new Tapp_System(DataTable.Rows[index]);
            }
        }

        public bool DeleteAll()
        {
            return this.DeleteByCondition(string.Empty);
        }
        
        #region Linq
        
        public Tapp_System Find(Predicate<Tapp_System> match)
        {
            foreach (Tapp_System item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tapp_SystemCollection FindAll(Predicate<Tapp_System> match)
        {
            Tapp_SystemCollection list = new Tapp_SystemCollection();
            foreach (Tapp_System item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tapp_System> match)
        {
            foreach (Tapp_System item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }

        public bool DeleteAt(Predicate<Tapp_System> match)
        {
            BeginTransaction();
            foreach (Tapp_System item in this)
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