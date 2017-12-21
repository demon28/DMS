using DataMonitorSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.Core;
using Winner.Framework.MVC;
using Winner.Framework.MVC.Controllers;
using Winner.Framework.Utils;
using Winner.YXH.DataMonitorSystem.DataAccess;
using Winner.YXH.DataMonitorSystem.Entities;
using Winner.YXH.DataMonitorSystem.Facade;

namespace DataMonitorSystem.Controllers
{
    [AuthRight]
    public class SQLController : TopControllerBase
    {
        #region Index

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string title = form["NodeTitle"];
            int fatherId = form["fatherId"].Safe().ToInt32();
            Tdms_InfoCollection daInfoColl = new Tdms_InfoCollection();
            daInfoColl.ChangePage = this.ChangePage();
            daInfoColl.ListByAppId(title, fatherId);
            return ListViewResult(daInfoColl, ValueFormat);
        }
        //显示格式转换
        private void ValueFormat(Dictionary<string, object> item)
        {
            item[Tdms_Info._STATE] = ((SQLStatus)item[Tdms_Info._STATE].Safe().ToInt32()).ToString();
        }

        public ActionResult Member(int? InfoId)
        {
            Tdms_AdminCollection daAdminColl = new Tdms_AdminCollection();
            daAdminColl.ChangePage = this.ChangePage();
            daAdminColl.ListByInfoIdAndNodeName(InfoId, string.Empty);
            ViewBag.InfoId = InfoId;
            return ListViewResult(daAdminColl, ValueFormatAdmin);
        }
        //显示格式转换
        private void ValueFormatAdmin(Dictionary<string, object> item)
        {
            item[Tdms_Admin._IS_SEND] = ((SqlIsSend)item[Tdms_Admin._IS_SEND].Safe().ToInt32()).ToString();
            item[Tdms_Admin._SEND_TYPE] = ((SqlSendType)item[Tdms_Admin._SEND_TYPE].Safe().ToInt32()).ToString();
        }
        //绑定服务列表

        public ActionResult WinService(int Id)
        {
            Tdms_WinserviceCollection daWinServiceColl = new Tdms_WinserviceCollection();
            daWinServiceColl.ChangePage = this.ChangePage();
            daWinServiceColl.ListById(Id);
            return ListViewResult(daWinServiceColl, ValueFormatWinService);
        }
        //显示格式转换
        private void ValueFormatWinService(Dictionary<string, object> item)
        {
            item[Tdms_Winservice._STATUS] = ((RunStatus)item[Tdms_Winservice._STATUS].Safe().ToInt32()).ToString();
            item[Tdms_Winservice._CYCLE] = ((DeductCycle)item[Tdms_Winservice._CYCLE].Safe().ToInt32()).ToString();
        }

        #endregion

        #region 绑定系统
        [HttpPost]
        public ActionResult SystemAppId()
        {
            string keyword = Request.Form["q"].Safe().ToString().Trim();
            Tapp_SystemCollection daSystemColl = new Tapp_SystemCollection();
            daSystemColl.ListByX(keyword);
            var arrayList = ToArrayList(daSystemColl.DataTable);
            return Json(arrayList);
        }

        #endregion

        #region Add

        public ActionResult Add(int id)
        {
            VInfoModel vModel = new VInfoModel();
            vModel.ChannelId = id;
            return View(vModel);
        }

        [HttpPost]
        public ActionResult Add(VInfoModel vModel)
        {
            if (vModel.ChannelId == 0)
            {
                return FailResult("栏目分类不能为空！");
            }
            try
            {
                MSQL infoFacade = new MSQL();
                infoFacade.DAL.Title = vModel.Title;
                infoFacade.DAL.ChannelId = vModel.ChannelId;
                infoFacade.DAL.SqlContext = vModel.SqlContext;
                infoFacade.DAL.State = vModel.State;
                infoFacade.DAL.OperaotId = SysUser.UserId;
                infoFacade.DAL.Remark = vModel.Remark;
                if (!infoFacade.Add())
                {
                    return FailResult(infoFacade.PromptInfo.MessageStack);
                }
                return SuccessResult();
            }
            catch (Exception ex)
            {
                return FailResult("SQL出现异常：" + ex.Message);
            }
        }


        #endregion

        #region Update

        public ActionResult Update(int Id)
        {
            MSQL sql = new MSQL(Id);
            if (!sql.IsExist)
            {
                return FailResult("会员不存在");
            }
            VInfoModel model = new VInfoModel
            {
                Id = sql.DAL.Id,
                Title = sql.DAL.Title,
                ChannelId = sql.DAL.ChannelId,
                SqlContext = sql.DAL.SqlContext,
                State = sql.DAL.State,
                Remark = sql.DAL.Remark
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(VInfoModel vModel)
        {
            try
            {
                MSQL sql = new MSQL(vModel.Id);
                sql.DAL.Title = vModel.Title;
                vModel.ChannelId = sql.DAL.ChannelId;
                sql.DAL.SqlContext = vModel.SqlContext;
                sql.DAL.State = vModel.State;
                sql.DAL.Remark = vModel.Remark;
                sql.DAL.OperaotId = SysUser.UserId;
                if (!sql.Update())
                {
                    return FailResult(sql.PromptInfo.MessageStack);
                }
                return SuccessResult();
            }
            catch (Exception ex)
            {
                return FailResult("SQL出现异常：" + ex.Message);
            }
        }
        #endregion

        #region Delete

        public ActionResult Delete(int Id)
        {
            Tdms_Info daInfo = new Tdms_Info();
            if (!daInfo.SelectByPK(Id))
            {
                return FailResult("此记录已不存在");
            }

            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete()
        {
            int id = Request.Form["Id"].Safe().ToInt32();
            Tdms_Admin daAdmin = new Tdms_Admin();
            if (daAdmin.SelectByInfoId(id))
            {
                return FailResult("该记录已有管理员监控无法删除！");
            }
            Tdms_Info daInfo = new Tdms_Info();
            if (!daInfo.SelectByPK(id))
            {
                return FailResult("此记录已不存在");
            }

            if (!daInfo.Delete(id))
            {
                return FailResult("删除失败！原因：" + daInfo.PromptInfo.MessageStack);
            }

            return SuccessResult();
        }

        #endregion

        #region Execute

        public ActionResult Execute()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Execute(int Id)
        {
            try
            {
                Tdms_Info daInfo = new Tdms_Info();
                if (!daInfo.SelectByPK(Id))
                {
                    return FailResult("该记录不存在");
                }
                string SqlContext = daInfo.SqlContext;
                if (string.IsNullOrEmpty(SqlContext))
                {
                    return FailResult("sql语句为空，无法执行");
                }
                MSQL sqlFacade = new MSQL();
                int count;
                if (!sqlFacade.ExecuteSQL(SqlContext, out count))
                {
                    return FailResult(sqlFacade.PromptInfo.MessageStack);
                }
                if (count > 0 || count == 0)
                {
                    return FailResult("监控到" + count + "条非法数据！");
                }
                return SuccessResult();
            }
            catch (Exception ex)
            {
                return FailResult("执行SQL出现异常：" + ex.Message);
            }

        }

        #endregion

        #region 绑定管理员
        [HttpPost]
        public ActionResult GetNodeId(int? nodeId)
        {
            Vnet_ReginfoCollection daReginfoColl = new Vnet_ReginfoCollection();
            string likeValue = Request.Form["q"].Safe().ToString().Trim();
            daReginfoColl.ChangePage = this.ChangePage(1, 20);
            daReginfoColl.ListByLikeValue(likeValue.Length > 0 ? null : nodeId, likeValue);
            var arrayList = ToArrayList(daReginfoColl.DataTable);
            return Json(arrayList);
        }

        #endregion

        #region Tree
        [HttpPost]
        public JsonResult Tree(int? FatherId)
        {
            Tdms_ChannelCollection dachannelColl = new Tdms_ChannelCollection();
            dachannelColl.ListTreeByParentid(FatherId);

            List<VchannelTreeModel> list = new List<VchannelTreeModel>();
            foreach (Tdms_Channel item in dachannelColl)
            {
                VchannelTreeModel model = new VchannelTreeModel();
                model.ChannelId = item.ChannelId;
                model.ChannelName = item.ChannelName;
                model.id = item.ChannelId;
                model.text = string.Format("[{0}]-{1} ({2})", item.ChannelId, item.ChannelName, item.Status);
                model.CreateTime = item.CreateTime;
                model.FatherId = item.FatherId;
                model.Remarks = item.Remarks;
                model.Status = item.Status;
                model.state = item.DataRow["CHILD_COUNT"].Safe().ToInt32() > 0 ? "closed" : "open";
                list.Add(model);
            }
            return Json(list);
        }
        #endregion Tree

        #region 启动服务
        [HttpPost]
        public ActionResult RestartWinService()
        {
            RestartWinService restartWinService = new RestartWinService();
            if (!restartWinService.Execute())
            {
                Log.Info("启动服务失败异常：" + restartWinService.PromptInfo.MessageStack);
                return FailResult("启动服务失败：" + restartWinService.PromptInfo.MessageStack);
            }
            return SuccessResult();
        }
        #endregion

    }
}
