using DataMonitorSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC;
using Winner.Framework.MVC.Controllers;
using Winner.Framework.Utils;
using Winner.Project.Facade;
using Winner.YXH.DataMonitorSystem.DataAccess;
using Winner.YXH.DataMonitorSystem.Entities;
using Winner.YXH.DataMonitorSystem.Facade;

namespace DataMonitorSystem.Controllers
{
    [AuthRight]
    public class ChannelController : TopControllerBase
    {
        // GET: /ChannelController/

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection Form)
        {
            int? channelId = Form["ChannelId"].Safe().ToNullableInt32();
            string channelName = Form["ChannelName"].Safe().ToString();
            Tdms_ChannelCollection daChannelColl = new Tdms_ChannelCollection();
            daChannelColl.ChangePage = this.ChangePage();
            daChannelColl.ListByChaneel(channelId, channelName);
            return ListViewResult(daChannelColl, ValueFormat);
        }

        //显示格式转换
        private void ValueFormat(Dictionary<string, object> item)
        {
            item[Tdms_Channel._STATUS] = ((ChannelStatus)item[Tdms_Channel._STATUS].Safe().ToInt32()).ToString();
        }
        #endregion

        #region Add
        public ActionResult Add(int TreeNodeId)
        {
            ViewBag.TreeNodeId = TreeNodeId;
            return View();
        }

        [HttpPost]
        public ActionResult Add(VchannelModel vModel)
        {
            Tdms_Channel datdms = new Tdms_Channel();
            if (!datdms.SelectByPK(vModel.TreeNodeId))
            {
                return FailResult("没有该条记录！" + datdms.PromptInfo.MessageStack);
            }
            try
            {
                Channel channel = new Channel();
                channel.ChannelName = vModel.ChannelName;
                channel.FatherId = vModel.TreeNodeId;
                channel.Status = vModel.Status;
                channel.Remarks = vModel.Remarks;
                if (!channel.Add())
                {
                    return FailResult(channel.PromptInfo.MessageStack);
                }
                return SuccessResult();
            }
            catch (Exception ex)
            {
                return FailResult("添加栏目信息：" + ex.Message);
            }
        }
        #endregion

        #region Update
        public ActionResult Update(int id)
        {
            Channel channel = new Channel(id);
            if (!channel.IsExist)
            {
                return FailResult("栏目不存在");
            }
            VchannelModel model = new VchannelModel
            {
                ChannelId = channel.ChannelId,
                ChannelName = channel.ChannelName,
                FatherId = channel.FatherId,
                Status = channel.Status,
                Remarks = channel.Remarks,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(VchannelModel vModel)
        {
            try
            {
                Channel channel = new Channel(vModel.ChannelId);
                channel.ChannelName = vModel.ChannelName;
                channel.FatherId = vModel.FatherId;
                channel.Status = vModel.Status;
                channel.Remarks = vModel.Remarks;
                if (!channel.Update())
                {
                    return FailResult(channel.PromptInfo.MessageStack);
                }
                TreeModel treeModel = BindTreeModel(channel);
                return SuccessResult(treeModel);
            }
            catch (Exception ex)
            {
                return FailResult("SQL出现异常：" + ex.Message);
            }
        }
        #endregion

        #region Delete
        public ActionResult Delete(int ChannelId)
        {
            Tdms_Channel dachannel = new Tdms_Channel();
            if (!dachannel.SelectByPK(ChannelId))
            {
                return FailResult("此记录已不存在");
            }
            ViewBag.ChannelId = ChannelId;
            return View();
        }

        [HttpPost]
        public ActionResult Delete()
        {
            int channelId = Request.Form["channelId"].Safe().ToInt32();
            Tdms_Info daInfo = new Tdms_Info();
            if (daInfo.SelectByChannelId(channelId))
            {
                return FailResult("该栏目下有监控数据无法删除！");
            }
            Channel dachannel = new Channel(channelId);
            if (!dachannel.Delete())
            {
                return FailResult("删除失败！原因：" + dachannel.PromptInfo.MessageStack);
            }
            return SuccessResult();
        }
        #endregion

        #region 获取所有栏目

        [HttpPost]
        public ActionResult FarthChannel()
        {
            Tdms_ChannelCollection daChannelColl = new Tdms_ChannelCollection();
            daChannelColl.listByChaneelName();
            var arrayList = ToArrayList(daChannelColl.DataTable);
            return Json(arrayList);
        }
        #endregion

        #region 禁用和启用
        [HttpPost]
        public JsonResult Operate(int channelId, string status)
        {
            Channel channel = new Channel();
            if (!channel.Operate(channelId, status))
            {
                return FailResult(channel.PromptInfo.MessageStack);
            }
            return SuccessResult();
        }

        #endregion


        #region 绑定树形返回参数

        public TreeModel BindTreeModel(Channel channel)
        {
            TreeModel model = new TreeModel();
            model.id = channel.DAL.ChannelId;
            model.Status = channel.DAL.Status;
            model.text = string.Format("[{0}]-{1} ({2})", channel.DAL.ChannelId, channel.DAL.ChannelName, channel.DAL.Status);
            switch (channel.DAL.Status)
            {
                case ChannelStatus.禁用:
                    model.text = model.text;
                    break;

                case ChannelStatus.删除:
                    model.text = "<span style='color:red'>" + model.text + "</span>";
                    break;

                default:
                    model.text = model.text;
                    break;
            }
            return model;
        }

        #endregion 绑定返回参数
    }

}
