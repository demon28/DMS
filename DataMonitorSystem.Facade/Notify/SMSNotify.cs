using SmsCenter.SmsServiceClient;
/***************************************************
*
* Create Time : 2014/11/7 15:14:08 
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
using Winner.Framework.Core;
using Winner.Framework.Utils;
using Winner.YXH.DataMonitorSystem.Entites;

namespace Winner.YXH.DataMonitorSystem.Facade.Notify
{
    //短信通知
    public class SMSNotify : NotifyAdmin
    {
        NotifyAdmin Notify { get; set; }
        public SMSNotify(NotifyAdmin _notify)
        {
            Notify = _notify;
            base.Address = _notify.Address;
            base.Content = _notify.Content;
        }
        public static SMSNotify New(NotifyAdmin notify)
        {
            return new SMSNotify(notify);
        }

        public override void Send()
        {
            Notify.Send();
            SendSms(Address, Content);
        }

        private void SendSms(string Mobileno, string msg)
        {
            if (Debuger.IsDebug
                || string.IsNullOrEmpty(msg)
                || string.IsNullOrEmpty(Mobileno))
            {
                return;
            }

            try
            {
                using (SmsClient client = new SmsClient(AppConfig.SMS_UserCode, AppConfig.SMS_UserPwd))
                {
                    client.SendSms(Mobileno, msg, null);
                    Log.Info("给手机号[" + Mobileno + "]发送短信成功");
                }
                //TODO:接入消息中心
                //messageModel model = new messageModel()
                //{
                //    Receiver = Mobileno,
                //    msg = msg,
                //    TemplateNo = AppConfig.SMSTemplateNo
                //};
                //MessageCenter message = new MessageCenter();
                //if (message.sendMessage(model))
                //{
                //    return;
                //}

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }
}
