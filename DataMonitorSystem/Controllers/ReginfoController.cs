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
using Winner.Framework.MVC;
using Winner.Framework.MVC.Controllers;

namespace DataMonitorSystem.Controllers
{
    [AuthRight]
    public class ReginfoController : TopControllerBase
    {

        #region Index

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string NodeCode = form["NodeCode"];
            string NodeName = form["NodeName"];
            Vnet_ReginfoCollection daReginfoColl = new Vnet_ReginfoCollection();
            daReginfoColl.ChangePage = this.ChangePage();
            daReginfoColl.ListByNodeCodeAndNodeName(NodeCode, NodeName);
            return ListViewResult(daReginfoColl, ValueFormat);
        }
        //显示格式转换
        private void ValueFormat(Dictionary<string, object> item)
        {
            item[Vnet_Reginfo._ISCONFIRMED] = ((ReginfoIsconfirmed)item[Vnet_Reginfo._ISCONFIRMED].Safe().ToInt32()).ToString();
        }
        #endregion

    }
}
