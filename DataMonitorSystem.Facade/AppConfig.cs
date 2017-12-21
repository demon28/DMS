/***************************************************
*
* Create Time : 2014/11/6 16:11:11 
* Version : V 1.01
* Create User:Jie
* E_Mail : 6e9e@163.com
* Blog : http://www.cnblogs.com/fineblog/
* Copyright (C) Winner(深圳-乾海盛世)
* 
***************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Winner.Framework.Core;
using Winner.Framework.Utils;

namespace Winner.YXH.DataMonitorSystem.Facade
{
    public class AppConfig
    {
        #region SMS
        /// <summary>
        /// SMS接口账户
        /// </summary>
        public static string SMS_UserCode
        {
            get
            {
                string temp = ConfigurationManager.AppSettings["SMS_UserCode"];
                if (string.IsNullOrEmpty(temp))
                {
                    //正式的
                    return "MSG";
                }
                return temp;
            }
        }
        /// <summary>
        /// SMS接口账户密码
        /// </summary>
        public static string SMS_UserPwd
        {
            get
            {
                string temp = ConfigurationManager.AppSettings["SMS_UserPwd"];
                if (string.IsNullOrEmpty(temp))
                {
                    return "1FF34A34AF899856539C67BF9B93CE97";
                }
                return temp;
            }
        }
        #endregion

        #region Email
        /// <summary>
        /// Email接口账户
        /// </summary>
        public static string Email_UserCode
        {
            get
            {
                string temp = ConfigurationManager.AppSettings["Email_UserCode"];
                if (string.IsNullOrEmpty(temp))
                {
                    //正式的
                    return "979855710@qq.com";
                }
                return temp;
            }
        }
        /// <summary>
        /// SMS接口账户密码
        /// </summary>
        public static string Email_UserPwd
        {
            get
            {
                string temp = ConfigurationManager.AppSettings["Email_UserPwd"];
                if (string.IsNullOrEmpty(temp))
                {
                    //return "ppdkproldtgnbdjd";
                    return "bblscrpsnhzsbcce";
                }
                return temp;
            }
        }
        #endregion

        /// <summary>
        /// 短信模板编号
        /// </summary>
        public static string SMSTemplateNo = "cd34175bcf339b6f752bc2bfed285563";

        /// <summary>
        /// 邮件模板编号
        /// </summary>
        public static string EmailTemplateNo = "0cd7ffbb6f660ff0d7cc8a39b2bc4de3";

        /// <summary>
        /// 退还保证金接口秘钥
        /// </summary>
        public static string MessageKey = "6EEEE1591370F1299F13A55681FDB84C";
        public static string MessageMerchantNo = "MSG1813514000000001";
        /// <summary>
        /// 退换保证金地址
        /// </summary>
        public static string MessageUrl
        {
            get
            {
                string temp = ConfigProvider.GetAppSetting("MessageUrl");
                if (temp.Empty())
                {
                    if (Debuger.IsDebug)
                    {
                        return " http://api.msg.fk18go.cn:81/Send/Index";
                    }

                    return " http://api.msg.fk18g.net/Send/Index";
                }

                return temp;
            }
        }

    }
}
