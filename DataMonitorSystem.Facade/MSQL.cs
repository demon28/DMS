/***************************************************
*
* Create Time : 2014/10/9 15:52:30 
* Version : V 1.01
* Create User:Jie
* E_Mail : 6e9e@163.com
* Blog : http://www.cnblogs.com/fineblog/
* Copyright (C) Winner(深圳-乾海盛世)
* 
***************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Winner.Framework.Core;
using Winner.Framework.Core.Facade;
using Winner.YXH.DataMonitorSystem.DataAccess;
using Winner.YXH.DataMonitorSystem.Entities;
using System.Text.RegularExpressions;
using Winner.Framework.Utils;

namespace Winner.YXH.DataMonitorSystem.Facade
{
    public class MSQL : FacadeBase
    {
        public MSQL(int? id = null)
            : base()
        {
            _id = id;
        }
        #region Field

        private int? _id;
        private Tdms_Info _dal;
        #endregion
        /// <summary>
        /// 是否存在
        /// </summary>
        public bool IsExist
        {
            get
            {
                return DAL.Id > 0;
            }
        }
        public Tdms_Info DAL
        {
            get
            {
                if (_dal == null)
                {
                    _dal = new Tdms_Info();
                    _dal.ReferenceTransactionFrom(this.Transaction);
                    if (_id.HasValue)
                    {
                        _dal.SelectByPK(_id.Value);
                    }
                }
                return _dal;
            }
        }
        /// <summary>
        /// 添加SQL信息表
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            //添加SQL信息表数据
            BeginTransaction();
            if (!SaveVerify())
            {
                Rollback();
                return false;
            }
            if (!DAL.Insert())
            {
                Rollback();
                Alert("保存SQL信息失败:" + DAL.PromptInfo.Message);
                return false;
            }
            MAdmin admin = new MAdmin();
            admin.DAL.InfoId = DAL.Id;
            admin.DAL.NodeId = 5;
            admin.DAL.Remark = string.Empty;
            admin.DAL.IsSend = SqlIsSend.不发送;
            admin.DAL.SendType = SqlSendType.邮件;
            admin.DAL.Email = "hsj@fk18g.com";
            DAL.ReferenceTransactionFrom(this.Transaction);
            if (!admin.Add())
            {
                Rollback();
                Alert("添加默认管理员失败：" + admin.PromptInfo.Message);
                return false;
            }
            Commit();
            return true;
        }
        /// <summary>
        /// 根据id修改SQL信息表的信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sql_context"></param>
        /// <returns></returns>
        public bool Update()
        {
            //先查询此条信息是否存在
            DAL.ReferenceTransactionFrom(this.Transaction);
            if (!SaveVerify())
            {
                return false;
            }
            if (!IsExist)
            {
                Alert("此条信息不存在，修改失败");
                return false;
            }
            if (!DAL.Update())
            {
                Alert("保存SQL信息失败");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool SaveVerify()
        {
            if (string.IsNullOrEmpty(DAL.SqlContext))
            {
                Alert("SQL语句不能为空");
                return false;
            }
            if (DAL.OperaotId <= 0)
            {
                Alert(ResultType.非法数值, "操作员无效");
                return false;
            }
            Regex regex = new Regex("DELETE |UPDATE |INSERT |CREATE |DROP |ALTER |TRUNCATE ");
            var match = regex.Match(DAL.SqlContext.ToUpper());
            if (match.Success)
            {
                Alert("SQL语句出现非法字符：\"" + match.Value + "\"");
                return false;
            }
            Tdms_InfoCollection daInfo = new Tdms_InfoCollection();
            if (!daInfo.CheckSQLCommand(DAL.SqlContext))
            {
                Alert("SQL有误：" + daInfo.PromptInfo);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <returns></returns>
        public bool ExecuteSQL(string sql, out int count)
        {
            Tdms_InfoCollection daInfo = new Tdms_InfoCollection();
            count = daInfo.ExecuteSQL(sql);
            return true;
        }
    }
}
