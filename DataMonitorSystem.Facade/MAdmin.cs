/***************************************************
*
* Create Time : 2014/10/10 14:12:54 
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

namespace Winner.YXH.DataMonitorSystem.Facade
{
    public class MAdmin : FacadeBase
    {
        public MAdmin(int? id = null)
            : base()
        {
            _id = id;
        }
        #region Field

        private int? _id;
        private Tdms_Admin _dal;
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

        public Tdms_Admin DAL
        {
            get
            {
                if (_dal == null)
                {
                    _dal = new Tdms_Admin();
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
        /// 修改管理员表
        /// </summary>
        /// <param name="id">表主键id</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public bool Update()
        {
            if (!SaveVerify()) 
            {
                return false;
            }
            //查询此信息是否存在
            if (!IsExist)
            {
                Alert("数据不存在");
                return false;
            }
            if (!DAL.Update())
            {
                Alert("修改失败");
                return false;
            }
            return true;
        }
        public bool Add()
        {
            if (!SaveVerify()) 
            {
                return false;
            }
            //验证该管理员是否已经具有这条SQL的权限
            Tdms_Admin daAdmin = new Tdms_Admin();
            if (daAdmin.ListByInfoAndNodeId(this.DAL.InfoId, this.DAL.NodeId))
            {
                Alert("该管理员已经具有该条sql的权限");
                return false;
            }
            //添加插入此条SQL的管理员
            if (!DAL.Insert())
            {
                Alert("保存SQL管理信息失败",DAL.PromptInfo);
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
            
            //查看此管理员是否存在
            Vnet_Reginfo daReginfo = new Vnet_Reginfo();
            if (!daReginfo.SelectByPK(this.DAL.NodeId))
            {
                Alert("该管理员不存在");
                return false;
            }
            if(string.IsNullOrEmpty(DAL.Email))
            {
                Alert("请输入邮箱");
                return false;
            }
            if(DAL.SendType==SqlSendType.短信)
            {
                Alert("邮件通知为必选项");
                return false;
            }
            return true;
        }
    }
}
