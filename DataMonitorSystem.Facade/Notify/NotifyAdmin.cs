/***************************************************
*
* Create Time : 2014/11/7 15:12:26 
* Version : V 1.01
* Create User:Jie
* E_Mail : 6e9e@163.com
* Blog : http://www.cnblogs.com/fineblog/
* Copyright (C) Winner(深圳-乾海盛世)
* 
***************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Winner.YXH.DataMonitorSystem.Facade.Notify
{
    public class NotifyAdmin
    {
        public string Address { get; set; }

        public string Content { get; set; }

        public int? RunResult { get; set; }
        public virtual void Send()
        {
        }
    }
}
