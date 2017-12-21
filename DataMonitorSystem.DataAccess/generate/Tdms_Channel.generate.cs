/***************************************************
*
* Data Access Layer Of Winner Framework
* FileName : Tdms_Channel.generate.cs 
* Version : V 1.1.0
* Author：架构师 曾杰(Jie)
* E_Mail : 6e9e@163.com
* Tencent QQ：554044818
* Blog : http://www.cnblogs.com/fineblog/
* Company ：深圳市乾海盛世移动支付有限公司
* Copyright (C) Winner研发中心
* CreateTime : 2016-08-29 15:47:31  
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
    /// Data Access Layer Object Of Tdms_Channel
    /// </summary>
    public partial class Tdms_Channel : DataAccessBase
    {
        #region 默认构造

        public Tdms_Channel() : base() { }

        public Tdms_Channel(DataRow dataRow)
            : base(dataRow) { }

        #endregion 默认构造

        #region 对应表结构的常量属性

        public const string _CHANNEL_ID = "CHANNEL_ID";
        public const string _CHANNEL_NAME = "CHANNEL_NAME";
        public const string _FATHER_ID = "FATHER_ID";
        public const string _STATUS = "STATUS";
        public const string _REMARKS = "REMARKS";
        public const string _CREATE_TIME = "CREATE_TIME";


        public const string _TABLENAME = "Tdms_Channel";
        #endregion 对应表结构的常量属性

        #region 公开属性

        /// <summary>
        /// 栏目编号
        /// [default:0]
        /// </summary>
        public int ChannelId
        {
            get { return Convert.ToInt32(DataRow[_CHANNEL_ID]); }
            set { setProperty(_CHANNEL_ID, value); }
        }
        /// <summary>
        /// 栏目名称
        /// [default:string.Empty]
        /// </summary>
        public string ChannelName
        {
            get { return DataRow[_CHANNEL_NAME].ToString(); }
            set { setProperty(_CHANNEL_NAME, value); }
        }
        /// <summary>
        /// 父级ID
        /// [default:DBNull.Value]
        /// </summary>
        public int FatherId
        {

            get { return Convert.ToInt32(DataRow[_FATHER_ID]); }
            set { setProperty(_FATHER_ID, value); }

        }
        /// <summary>
        /// 状态{禁用=0,启用=1,删除=2}
        /// [default:0]
        /// </summary>
        public ChannelStatus Status
        {
            get { return (ChannelStatus)int.Parse(DataRow[_STATUS].ToString()); }
            set { setProperty(_STATUS, (int)value); }
        }
        /// <summary>
        /// 栏目备注
        /// [default:string.Empty]
        /// </summary>
        public string Remarks
        {
            get { return DataRow[_REMARKS].ToString(); }
            set { setProperty(_REMARKS, value); }
        }
        /// <summary>
        /// 创建时间
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
            dt.Columns.Add(_CHANNEL_ID, typeof(int)).DefaultValue = 0;
            dt.Columns.Add(_CHANNEL_NAME, typeof(string)).DefaultValue = string.Empty;
            dt.Columns.Add(_FATHER_ID, typeof(int)).DefaultValue = DBNull.Value;
            dt.Columns.Add(_STATUS, typeof(int)).DefaultValue = 0;
            dt.Columns.Add(_REMARKS, typeof(string)).DefaultValue = string.Empty;
            dt.Columns.Add(_CREATE_TIME, typeof(DateTime)).DefaultValue = new DateTime();

            return dt.NewRow();
        }

        #endregion 私有成员

        #region 常用方法

        protected bool DeleteByCondition(string condition)
        {
            string sql = @"DELETE FROM TDMS_CHANNEL WHERE " + condition;
            return base.DeleteBySql(sql);
        }

        public bool Delete(int channelId)
        {
            string condition = "CHANNEL_ID=:CHANNEL_ID";
            AddParameter(_CHANNEL_ID, channelId);
            return DeleteByCondition(condition);
        }

        public bool Delete()
        {
            string condition = "CHANNEL_ID=:CHANNEL_ID";
            AddParameter(_CHANNEL_ID, ChannelId);
            return DeleteByCondition(condition);
        }

        public bool Insert()
        {
            ChannelId = base.GetSequence("SELECT SEQ_TDMS_CHANNEL.NEXTVAL FROM DUAL");
            string sql = @"INSERT INTO
TDMS_CHANNEL(
  CHANNEL_ID,
  CHANNEL_NAME,
  FATHER_ID,
  STATUS,
  REMARKS)
VALUES(
  :CHANNEL_ID,
  :CHANNEL_NAME,
  :FATHER_ID,
  :STATUS,
  :REMARKS)";
            AddParameter(_CHANNEL_ID, DataRow[_CHANNEL_ID]);
            AddParameter(_CHANNEL_NAME, DataRow[_CHANNEL_NAME]);
            AddParameter(_FATHER_ID, DataRow[_FATHER_ID]);
            AddParameter(_STATUS, DataRow[_STATUS]);
            AddParameter(_REMARKS, DataRow[_REMARKS]);
            return base.InsertBySql(sql);
        }

        public bool Update()
        {
            return UpdateByCondition(string.Empty);
        }

        protected bool UpdateByCondition(string condition)
        {
            //移除主键标记
            ChangePropertys.Remove(_CHANNEL_ID);

            if (ChangePropertys.Count == 0)
            {
                return true;
            }

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TDMS_CHANNEL SET");
            while (ChangePropertys.MoveNext())
            {
                sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
            sql.AppendLine(" WHERE CHANNEL_ID=:CHANNEL_ID");
            AddParameter(_CHANNEL_ID, DataRow[_CHANNEL_ID]);
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
  CHANNEL_ID,
  CHANNEL_NAME,
  FATHER_ID,
  STATUS,
  REMARKS,
  CREATE_TIME
FROM TDMS_CHANNEL
WHERE " + condition;
            return base.SelectBySql(sql);
        }

        public bool SelectByPK(int channelId)
        {
            string condition = "CHANNEL_ID=:CHANNEL_ID";
            AddParameter(_CHANNEL_ID, channelId);
            return SelectByCondition(condition);
        }



        #endregion 常用方法

        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }

    /// <summary>
    /// Data Access Layer Object Collection Of Tdms_Channel
    /// </summary>
    public partial class Tdms_ChannelCollection : DataAccessCollectionBase
    {
        #region 默认构造

        public Tdms_ChannelCollection() { }

        public Tdms_ChannelCollection(DataTable table)
            : base(table) { }

        #endregion 默认构造

        #region 私有成员
        protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tdms_Channel(DataTable.Rows[index]);
        }

        protected override DataTable BuildTable()
        {
            return new Tdms_Channel().CloneSchemaOfTable();
        }

        protected override string TableName
        {
            get { return Tdms_Channel._TABLENAME; }
        }

        protected bool ListByCondition(string condition)
        {
            string sql = @"
SELECT
  CHANNEL_ID,
  CHANNEL_NAME,
  FATHER_ID,
  STATUS,
  REMARKS,
  CREATE_TIME
FROM TDMS_CHANNEL
WHERE " + condition;
            return base.ListBySql(sql);
        }

        public bool ListByChannelId(int channelId)
        {
            string condition = "CHANNEL_ID=:CHANNEL_ID";
            AddParameter(Tdms_Channel._CHANNEL_ID, channelId);
            return ListByCondition(condition);
        }

        public bool ListAll()
        {
            string condition = "1=1";
            return ListByCondition(condition);
        }

        public bool DeleteByCondition(string condition)
        {
            string sql = "DELETE FROM TDMS_CHANNEL WHERE " + condition;
            return DeleteBySql(sql);
        }
        #endregion

        #region 公开成员
        public Tdms_Channel this[int index]
        {
            get
            {
                return new Tdms_Channel(DataTable.Rows[index]);
            }
        }

        public bool DeleteAll()
        {
            return this.DeleteByCondition(string.Empty);
        }

        #region Linq

        public Tdms_Channel Find(Predicate<Tdms_Channel> match)
        {
            foreach (Tdms_Channel item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tdms_ChannelCollection FindAll(Predicate<Tdms_Channel> match)
        {
            Tdms_ChannelCollection list = new Tdms_ChannelCollection();
            foreach (Tdms_Channel item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tdms_Channel> match)
        {
            foreach (Tdms_Channel item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }

        public bool DeleteAt(Predicate<Tdms_Channel> match)
        {
            BeginTransaction();
            foreach (Tdms_Channel item in this)
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