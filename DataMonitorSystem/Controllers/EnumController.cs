using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC.Controllers;
using Winner.YXH.DataMonitorSystem.Entities;

namespace DataMonitorSystem.Controllers
{
    public class EnumController : TopControllerBase
    {
        [HttpPost]
        public ActionResult SQLStatus(string all)
        {
            return EnumsResult<SQLStatus>(all=="1","==请选择==");
        }
        [HttpPost]
        public ActionResult RunStatus(string all)
        {
            return EnumsResult<RunStatus>(all == "1", "==请选择==");
        }
        [HttpPost]
        public ActionResult CYCLE(string all)
        {
            return EnumsResult<DeductCycle>(all == "1", "==请选择==");
        }
        [HttpPost]
        public ActionResult IsSend(string all)
        {
            return EnumsResult<SqlIsSend>(all == "1", "==请选择==");
        }
        [HttpPost]
        public ActionResult SendType(string all)
        {
            return EnumsResult<SqlSendType>(all == "1", "==请选择==");
        }

        [HttpPost]
        public ActionResult ChannelStatus(string all)
        {
            return EnumsResult<ChannelStatus>(all == "1", "==请选择==");
        }
    }
}
