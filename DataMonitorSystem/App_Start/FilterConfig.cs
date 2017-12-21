using System.Web;
using System.Web.Mvc;

namespace DataMonitorSystem
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Winner.Framework.MVC.GlobalErrorAttribute());
        }
    }
}