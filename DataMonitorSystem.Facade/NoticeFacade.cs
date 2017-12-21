/***************************************************
*
* Create Time : 2014/11/6 16:14:55 
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
using Winner.Framework.Core;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;
using Winner.YXH.DataMonitorSystem.DataAccess;
using Winner.YXH.DataMonitorSystem.Entities;
using Winner.YXH.DataMonitorSystem.Facade.Notify;

namespace Winner.YXH.DataMonitorSystem.Facade
{
    public class NoticeFacade : FacadeBase
    {
        #region 判断是否有监控到数据
        public void IsEixt(Tdms_Winservice item, Tdms_Info daInfo)
        {
            if (item.RunResult > 0)
            {
                IsSend(item, daInfo);
            }

        }
        #endregion

        #region 判断休息时间是否发送通知
        public bool IsSend(Tdms_Winservice item, Tdms_Info daInfo)
        {
            //Tdms_Admin daAdmin = new Tdms_Admin();
            //if (!daAdmin.SelectByInfoId(item.Id))
            //{
            //    Log.Error("[" + daInfo.Title + "]没有管理员！");
            //    return false;
            //}
            Tdms_AdminCollection daAdminColl = new Tdms_AdminCollection();
            daAdminColl.ListByInfoId(item.Id);
            foreach (Tdms_Admin taAdmin in daAdminColl)
            {
                SendType(taAdmin, daInfo, item);
            }
            return true;

        }
        #endregion

        #region 判断通知形式
        public void SendType(Tdms_Admin taAdmin, Tdms_Info daInfo, Tdms_Winservice item)
        {
            Vnet_Reginfo daVinfo = new Vnet_Reginfo();
            if (!daVinfo.SelectByPK(taAdmin.NodeId))
            {
                return;
            }
            //TODO:接入消息中心，此方法屏蔽掉，直接新建一个类似的方法
            //装饰者
            NotifyAdmin notify = new NotifyAdmin();

            //装饰EMail发送功能
            notify = EMailNotify.New(notify);
            notify.Address = taAdmin.Email;
            notify.RunResult = item.RunResult;
            notify.Content = string.Format("[{0}]{2}监控到{1}条非法数据。", daInfo.Title, item.RunResult, DateTime.Now.ToString("MM月dd日"));

            if ((taAdmin.SendType & SqlSendType.短信) == SqlSendType.短信)
            {
                if ((taAdmin.IsSend == SqlIsSend.发送) ||
                    (DateTime.Now.Hour >= 8))
                {
                    //装饰短信发送功能
                    notify = SMSNotify.New(notify);
                    notify.Address = daVinfo.Mobileno;
                }
            }
            notify.Send();
        }
        #endregion
    }
}
