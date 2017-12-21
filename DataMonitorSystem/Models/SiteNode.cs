using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMonitorSystem.Models
{
    public class SiteNode
    {
        public string url { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }

        public List<SiteNode> children { get; set; }

        public override string ToString()
        {
            return string.Format("text:{0},url:{1},iconCls:{2}", text, url, iconCls);
        }
    }
}