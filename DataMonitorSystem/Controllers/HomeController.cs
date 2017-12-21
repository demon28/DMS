using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC.Controllers;
using Winner.Framework.MVC;
using DataMonitorSystem.App_Start;
using DataMonitorSystem.Models;

namespace DataMonitorSystem.Controllers
{

    public class HomeController : TopControllerBase
    {
        [AuthRight]
        public ActionResult Index()
        {
            ViewBag.SystemName = SystemConfig.SystemName;
            ViewBag.NodeName = ApplicationContext.Current.User.UserName;

            return View();
        }
        public ActionResult Logout()
        {
            base.Logout();
            return null;
        }

        [AuthRight]
        public ActionResult Wellcome()
        {
            ViewBag.SystemName = SystemConfig.SystemName;
            ViewBag.NodeName = ApplicationContext.Current.User.UserName;
            ViewBag.NodeCode = ApplicationContext.Current.User.UserCode;
            ViewBag.UserHostName = ApplicationContext.UserHostName;
            ViewBag.UserHostAddress = ApplicationContext.UserHostAddress;

            return View();
        }
        //显示无权限
        public ActionResult Right()
        {
            ViewBag.SystemName = SystemConfig.SystemName;
            ViewBag.NodeName = ApplicationContext.Current.User.UserName;
            ViewBag.NodeCode = ApplicationContext.Current.User.UserCode;
            return View();
        }

        [HttpPost]
        public ActionResult WebSiteMap()
        {
            var provider = SiteMap.Provider;
            SiteNode list = new SiteNode();
            list.children = new List<SiteNode>();
            foreach (SiteMapNode item in provider.RootNode.ChildNodes)
            {
                SiteNode node = new SiteNode { text = item.Title, iconCls = item.Description, url = item.Url };
                foreach (SiteMapNode childItem in item.ChildNodes)
                {
                    if (node.children == null)
                    {
                        node.children = new List<SiteNode>();
                    }
                    SiteNode childNode = new SiteNode { text = childItem.Title, iconCls = childItem.Description, url = childItem.Url };
                    node.children.Add(childNode);
                }
                list.children.Add(node);
            }
            return Json(list);
        }
    }
}
