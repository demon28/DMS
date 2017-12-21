using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.YXH.DataMonitorSystem.DataAccess;
using Winner.Framework.Core;
using Winner.Framework.Utils;
using Winner.YXH.DataMonitorSystem.Entities;
using Winner.YXH.DataMonitorSystem.Facade;
using DataMonitorSystem.Models;
using Winner.Framework.MVC.Controllers;
using Winner.Framework.MVC;

namespace DataMonitorSystem.Controllers
{
    [AuthRight]
    public class AdminController : TopControllerBase
    {
        #region Index

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            int? InfoId = form["InfoId"].Safe().ToNullableInt32();
            string NodeName = form["NodeName"];
            Tdms_AdminCollection daAdminColl = new Tdms_AdminCollection();
            daAdminColl.ChangePage = this.ChangePage();
            daAdminColl.ListByInfoIdAndNodeName(InfoId,NodeName);
            return ListViewResult(daAdminColl);
        }
        #endregion

        #region Delete

        public ActionResult Delete(int Id)
        {
            ViewBag.InfoId = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Delete()
        {
            int id = Request.Form["Id"].Safe().ToInt32();
            Tdms_Admin daAdmin = new Tdms_Admin();
            if (!daAdmin.Delete(id))
            {
                return FailResult("删除失败！原因：" + daAdmin.PromptInfo.MessageStack);
            }

            return SuccessResult();
        }

        #endregion

        #region Update

        public ActionResult Update(int Id)
        {
            MAdmin admin = new MAdmin(Id);
            if (!admin.IsExist)
            {
                return FailResult("不存在此记录");
            }
            VAdminModel model = new VAdminModel
            {
                Id = admin.DAL.Id,
                InfoId = admin.DAL.InfoId,
                NodeId = admin.DAL.NodeId,
                Remark = admin.DAL.Remark,
                IsSend = admin.DAL.IsSend,
                SendType = admin.DAL.SendType,
                Email=admin.DAL.Email
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(VAdminModel vModel)
        {
            //SendType? notifyEmail = (SendType?)Request.Form["Notify_EMail"].Safe().ToNullableInt32();
            //SendType? notifySMS = (SendType?)Request.Form["Notify_SMS"].Safe().ToNullableInt32();
            //if (notifyEmail.HasValue)
            //{
            //    vModel.SendType = vModel.SendType | notifyEmail.Value;
            //}
            //if (notifySMS.HasValue)
            //{
            //    vModel.SendType = vModel.SendType | notifySMS.Value;
            //}
            MAdmin admin = new MAdmin(vModel.Id);
            admin.DAL.InfoId = vModel.InfoId;
            admin.DAL.NodeId = vModel.NodeId;
            admin.DAL.Remark = vModel.Remark;
            admin.DAL.IsSend = vModel.IsSend;
            admin.DAL.SendType = vModel.NotifyEmail|vModel.NotifySMS;
            admin.DAL.Email = vModel.Email;
            if (!admin.Update())
            {
                return FailResult(admin.PromptInfo.MessageStack);
            }
            return SuccessResult();
        }
        #endregion

        #region  绑定SQL标题
        [HttpPost]
        public ActionResult SQLTitle()
        {
            Tdms_InfoCollection daInfoColl = new Tdms_InfoCollection();
            daInfoColl.ListAll();
            var arrayList = ToArrayList(daInfoColl.DataTable);
            return Json(arrayList);
        }
        #endregion

        #region Add
  
        public ActionResult Add(int Id)
        {
            VAdminModel vModel = new VAdminModel();
            vModel.InfoId = Id;
            return View(vModel);

        }

        [HttpPost]
        public ActionResult Add(VAdminModel vModel)
        {
            SqlSendType? notifyEmail = (SqlSendType?)Request.Form["Notify_EMail"].Safe().ToNullableInt32();
            SqlSendType? notifySMS = (SqlSendType?)Request.Form["Notify_SMS"].Safe().ToNullableInt32();
            if (notifyEmail.HasValue)
            {
                vModel.SendType = vModel.SendType | notifyEmail.Value;
            }
            if (notifySMS.HasValue)
            {
                vModel.SendType = vModel.SendType | notifySMS.Value;
            }
            MAdmin admin = new MAdmin();
            admin.DAL.InfoId = vModel.InfoId;
            admin.DAL.NodeId = vModel.NodeId;
            admin.DAL.Remark = vModel.Remark;
            admin.DAL.IsSend = vModel.IsSend;
            admin.DAL.SendType = vModel.SendType;
            admin.DAL.Email = vModel.Email;
            if (!admin.Add())
            {
                return FailResult(admin.PromptInfo.MessageStack);
            }
            return SuccessResult();
        }


        #endregion
    }
}
