/***************************************************
*
* Create Time : 2014/8/22 14:01:44 
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

namespace Winner.YXH.DataMonitorSystem.Entities
{
    public enum SQLStatus
    {
        禁用 = 0,
        启用 = 1,
    }
    public enum ReginfoIsconfirmed
    {
        认证 = 0,
        未认证 = 1,
    }
    public enum RunStatus
    {
        成功 = 1,
        失败 = 2,
        运行中 = 3,
        没执行 = 0
    }
    public enum DeductCycle
    {
        小时 = 0,
        天 = 1,
        周 = 2,
        月 = 4,
        年 = 8
    }
    public enum SqlIsSend
    {
        发送 = 0,
        不发送 = 1
    }
    [Flags]
    public enum SqlSendType
    {
        邮件 = 1,
        短信 = 2
    }

    public enum ChannelStatus
    {
        禁用 = 0,
        启用 = 1,
        删除 = 2
    }
}
