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
    public class WinServiceController : TopControllerBase
    {
        //
        // GET: /WinService/
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            int? Cycle = form["Cycle"].Safe().ToNullableInt32();
            Tdms_WinserviceCollection daWinServiceColl = new Tdms_WinserviceCollection();
            daWinServiceColl.ChangePage = this.ChangePage();
            daWinServiceColl.ListByCycle(Cycle);
            return ListViewResult(daWinServiceColl, ValueFormat);
        }
        private void ValueFormat(Dictionary<string, object> item)
        {
            item[Tdms_Info._STATE] = ((SQLStatus)item[Tdms_Info._STATE].Safe().ToInt32()).ToString();
            item[Tdms_Winservice._STATUS] = ((RunStatus)item[Tdms_Winservice._STATUS].Safe().ToInt32()).ToString();
            item[Tdms_Winservice._CYCLE] = ((DeductCycle)item[Tdms_Winservice._CYCLE].Safe().ToInt32()).ToString();
        }
        #endregion

        #region Add
        public ActionResult Add(int Id)
        {
            VWinServiceModel vModel = new VWinServiceModel();
            vModel.Id = Id;
            return View(vModel);
        }
        [HttpPost]
        public ActionResult Add(VWinServiceModel vModel)
        {
            MWinService winService = new MWinService();
            winService.DAL.Id = vModel.Id;
            winService.DAL.Cycle = vModel.Cycle;
            winService.DAL.NextRunTime = vModel.NextRunTime;
            winService.DAL.Reamrks = vModel.Reamrks;
            if (!winService.Add())
            {
                return FailResult(winService.PromptInfo.MessageStack);
            }
            return SuccessResult();
        }
        #endregion

        #region Update
        public ActionResult Update(int Id)
        {
            MWinService winService = new MWinService(Id);
            if (!winService.IsExist)
            {
                return FailResult("此记录不存在");
            }
            VWinServiceModel vModel = new VWinServiceModel()
            {
                Id = winService.DAL.Id,
                Cycle = winService.DAL.Cycle,
                NextRunTime = winService.DAL.NextRunTime,
                Reamrks = winService.DAL.Reamrks
            };
            return View(vModel);
        }
        [HttpPost]
        public ActionResult Update(VWinServiceModel vModel)
        {
            MWinService winService = new MWinService(vModel.Id);
            winService.DAL.Cycle = vModel.Cycle;
            winService.DAL.NextRunTime = vModel.NextRunTime;
            winService.DAL.Reamrks = vModel.Reamrks;
            if (!winService.Update())
            {
                return FailResult(winService.PromptInfo.MessageStack);
            }
            return SuccessResult();
        }
        #endregion

        #region Delete
        public ActionResult Delete(int Id)
        {
            VWinServiceModel vModel = new VWinServiceModel()
            {
                Id = Id
            };
            return View(vModel);
        }
        [HttpPost]
        public ActionResult Delete(VWinServiceModel vModel)
        {
            Tdms_Winservice daWinService = new Tdms_Winservice();
            if (!daWinService.SelectByPK(vModel.Id))
            {
                return FailResult("此记录不存在");
            }
            if (!daWinService.Delete(vModel.Id))
            {
                return FailResult("删除失败");
            }
            return SuccessResult();
        }
        #endregion

    }
}
