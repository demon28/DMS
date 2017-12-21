/***************************************************
*
* Create Time : 2014/10/30 9:12:33 
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
using Winner.Framework.Core.Facade;
using Winner.YXH.DataMonitorSystem.DataAccess;

namespace Winner.YXH.DataMonitorSystem.Facade
{
    public class MWinService : FacadeBase
    {
        #region Field
        public MWinService(int? id = null)
            : base()
        {
            _id = id;
        }

        private int? _id;
        private Tdms_Winservice _dal;

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
        public Tdms_Winservice DAL
        {
            get
            {
                if (_dal == null)
                {
                    _dal = new Tdms_Winservice();
                    _dal.ReferenceTransactionFrom(this.Transaction);
                    if (_id.HasValue)
                    {
                        _dal.SelectByPK(_id.Value);
                    }
                }
                return _dal;
            }
        }
        #endregion

        public bool Add()
        {
            if (!SaveVerify())
            {
                return false;
            }
            if (DAL.SelectByPK(this.DAL.Id))
            {
                Alert("对不起，每条SQL语句只能绑定一条服务!");
                return false;
            }
            if (!DAL.Insert())
            {
                Alert("添加失败");
                return false;
            }
            return true;
        }
        public bool Update()
        {
            if (!IsExist) 
            {
                Alert("此条信息不存在");
                return false;
            }
            if(!DAL.Update())
            {
                Alert("修改失败");
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
            Tdms_Info daIndo = new Tdms_Info();
            if (!daIndo.SelectByPK(this.DAL.Id))
            {
                Alert("此条SQL语句不存在！");
                return false;
            }
            return true;
        }
    }
}
