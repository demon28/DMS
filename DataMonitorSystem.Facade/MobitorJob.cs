/***************************************************
*
* Create Time : 2014/10/28 14:05:34 
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
using System.Threading;
using System.Data;
using Winner.Framework.Core.Facade;
using Winner.YXH.DataMonitorSystem.DataAccess;
using Winner.YXH.DataMonitorSystem.Entities;
using Winner.Framework.Core;
using SmsCenter.SmsServiceClient;
using Winner.Framework.Utils;

namespace Winner.YXH.DataMonitorSystem.Facade
{
    /// <summary>
    /// 轮询
    /// </summary>
    public class MobitorJob : FacadeBase
    {
        #region 开始
        public void Start()
        {
            try
            {
                Tdms_Winservice daWinServcie = new Tdms_Winservice();
                while (true)
                {
                    DateTime? minTime = daWinServcie.GetMinTime();
                    if (!minTime.HasValue)
                    {
                        return;
                    }
                    if (DateTime.Now <= minTime.Value)
                    {
                        var time = minTime.Value;
                        if ((time - DateTime.Now).TotalSeconds > 1 * 60 * 60)
                        {
                            time = DateTime.Now.AddHours(1);
                        }
                        string endInfo = string.Format("监控系统已休眠，下次执行时间为：{0}", time);
                        Log.Info(endInfo);
                        Thread.Sleep(time - DateTime.Now);
                    }
                    ForeachRun();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        private void ForeachRun()
        {
            //1.取数据
            Tdms_WinserviceCollection daWinServiceColl = new Tdms_WinserviceCollection();
            daWinServiceColl.ListByNextTime();
            Log.Info("待监控SQL[" + daWinServiceColl.Count + "]条");
            if (daWinServiceColl.Count <= 0)
            {
                Log.Info("没有需要监控的数据，进入休眠30秒");
                Thread.Sleep(millisecondsTimeout: 30000);
                return;
            }
            //2.监控
            foreach (Tdms_Winservice item in daWinServiceColl)
            {
                Tdms_Info daInfo = new Tdms_Info(item.DataRow);
                Log.Info("[" + daInfo.Title + "]开始监控");
                //调用执行方法
                Run(daInfo, item);
                //修改
                if (!item.Update())
                {
                    Log.Error("修改失败");
                }
                string endInfo = string.Format("[{0}]监控结束（监控到[{1}]行数据）", daInfo.Title, item.RunResult);
                Log.Info(endInfo);

                NoticeFacade notiFace = new NoticeFacade();
                notiFace.IsEixt(item, daInfo);
            }
            Log.Info("监控完成");
        }
        #endregion

        #region 执行SQL语句
        public bool Run(Tdms_Info daInfo, Tdms_Winservice item)
        {
            try
            {
                MSQL sql = new MSQL();
                int count;
                if (!sql.ExecuteSQL(daInfo.SqlContext, out count))
                {
                    Log.Error("SQL语句有误");
                    Failure(item);
                    return false;
                }
                Success(item, count);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Failure(item);
                return false;
            }
        }
        #endregion

        #region 执行成功
        public void Success(Tdms_Winservice item, int count)
        {
            item.RunResult = count;
            item.Status = RunStatus.成功;
            DateTimeTicks(item);
        }
        #endregion

        #region 执行失败
        public void Failure(Tdms_Winservice item)
        {
            item.RunResult = -1;
            item.Status = RunStatus.失败;
            DateTimeTicks(item);
        }
        #endregion

        #region 判断周期并计算下次执行时间
        public void DateTimeTicks(Tdms_Winservice item)
        {
            DateTime NextTime = item.NextRunTime;
            DateTime row = DateTime.Now;
            switch (item.Cycle)
            {
                case DeductCycle.小时:
                    item.NextRunTime = DateTime.Now.Date.AddHours(DateTime.Now.Hour + 1);
                    break;
                case DeductCycle.天:
                    item.NextRunTime = new DateTime(row.Year, row.Month, row.Day, NextTime.Hour, NextTime.Minute, NextTime.Second).AddDays(1);
                    break;
                case DeductCycle.周:
                    item.NextRunTime = new DateTime(row.Year, row.Month, row.Day, NextTime.Hour, NextTime.Minute, NextTime.Second).AddDays(7 - (int)NextTime.DayOfWeek);
                    break;
                case DeductCycle.月:
                    item.NextRunTime = new DateTime(row.Year, row.Month, NextTime.Day, NextTime.Hour, NextTime.Minute, NextTime.Second).AddMonths(1);
                    break;
                case DeductCycle.年:
                    item.NextRunTime = new DateTime(row.Year, row.Month, NextTime.Day, NextTime.Hour, NextTime.Minute, NextTime.Second).AddYears(1);
                    break;
                default:
                    break;
            }
            //switch (item.Cycle)
            //{
            //    case DeductCycle.小时:
            //        item.NextRunTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH")+" "+item.NextRunTime.ToString(":mm:ss")).AddHours(1);
            //        break;
            //    case DeductCycle.天:
            //        item.NextRunTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + item.NextRunTime.ToString("HH:mm:ss")).AddDays(1);
            //        break;
            //    case DeductCycle.周:
            //        item.NextRunTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + item.NextRunTime.ToString("HH:mm:ss")).AddDays(7);
            //        break;
            //    case DeductCycle.月:
            //        item.NextRunTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM") + item.NextRunTime.ToString("-dd HH:mm:ss")).AddMonths(1);
            //        break;
            //    case DeductCycle.年:
            //        item.NextRunTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy") + item.NextRunTime.ToString("-MM-dd HH:mm:ss")).AddYears(1);
            //        break;
            //    default:
            //        break;
            //}
        }
        #endregion

        #region 停止
        public void Stop()
        {

        }
        #endregion
    }
}
