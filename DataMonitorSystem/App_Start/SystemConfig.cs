using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMonitorSystem.App_Start
{
     public class SystemConfig
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public static string SystemName
        {
            get
            {
                string temp = Winner.Framework.Utils.ConfigProvider.GetAppSetting("SystemName");
                if (string.IsNullOrEmpty(temp))
                {
                    return "*****管理系统";
                }
                return temp;
            }
        }
    }
}
