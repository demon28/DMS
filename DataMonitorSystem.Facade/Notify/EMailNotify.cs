/***************************************************
*
* Create Time : 2014/11/7 15:18:36 
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
using Winner.Framework.Encrypt;
using Winner.Framework.MVC;
using Winner.YXH.DataMonitorSystem.Entites;
using System.Net.Mail;

namespace Winner.YXH.DataMonitorSystem.Facade.Notify
{
    public class EMailNotify : NotifyAdmin
    {
        NotifyAdmin Notify { get; set; }
        public EMailNotify(NotifyAdmin _notify)
        {
            Notify = _notify;
            base.Address = _notify.Address;
            base.Content = _notify.Content;
            base.RunResult = _notify.RunResult;
        }
        public static EMailNotify New(NotifyAdmin notify)
        {
            return new EMailNotify(notify);
        }

        public override void Send()
        {
            Notify.Send();
            SendEmail(Address, "数据监听系统通知", Content, RunResult);
        }

        public virtual void SendEmail(string EmailNo, string Title, string msg, int? RunResult)
        {
            //if (Debuger.IsDebug
            //       || string.IsNullOrEmpty(msg)
            //       || string.IsNullOrEmpty(EmailNo))
            //{
            //    return;
            //}
            try
            {
                //创建邮件实例对象
                SmtpClient client = new SmtpClient("smtp.qq.com");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(AppConfig.Email_UserCode, AppConfig.Email_UserPwd);
                MailAddress from = new MailAddress(AppConfig.Email_UserCode, string.Empty, Encoding.UTF8);//初始化发件人  
                MailAddress to = new MailAddress(EmailNo, string.Empty, Encoding.UTF8);//初始化收件人  

                //设置邮件内容  
                MailMessage mamessage = new MailMessage(from, to);

                string Emailcontent = string.Format(@"邮件主题:[{1}];
                监控规则:监控用户数据非法;
                邮件内容:[{0}];
                监控时间:[{2}];
                非法数据:[{3}]条", msg, Title, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), RunResult);

                mamessage.Body = Emailcontent;//邮件内容
                mamessage.BodyEncoding = Encoding.UTF8;

                mamessage.Subject = Title;//邮件标题
                mamessage.SubjectEncoding = Encoding.UTF8;
                mamessage.IsBodyHtml = true;
                //发送邮件
                client.Send(mamessage);
                return;
                //接入消息中心
                //messageModel model = new messageModel()
                //{
                //    Receiver = EmailNo,
                //    msg = msg,
                //    TemplateNo = AppConfig.EmailTemplateNo
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
