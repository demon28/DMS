using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Winner.YXH.DataMonitorSystem.Entites
{
    public class SendMessageModel
    {
        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateNo { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<string, string> Parameter { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
    }

    public class messageModel
    {
        public string Receiver { get; set; }
        public string Title { get; set; }
        public string msg { get; set; }
        public string TemplateNo { get; set; }
    }

}
