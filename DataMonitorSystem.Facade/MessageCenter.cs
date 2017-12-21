using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winner.Framework.Core.Facade;
using Winner.Framework.Encrypt;
using Winner.Framework.MVC;
using Winner.Framework.Utils;
using Winner.YXH.DataMonitorSystem.Entites;

namespace Winner.YXH.DataMonitorSystem.Facade
{

    /// <summary>
    /// 接入消息发送中心
    /// </summary>
    public class MessageCenter : FacadeBase
    {
        public bool sendMessage(messageModel msgModel)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Title", msgModel.Title);
            dict.Add("Content", msgModel.msg);
            SendMessageModel model = new SendMessageModel();
            model.TemplateNo = msgModel.TemplateNo;
            model.Receiver = msgModel.Receiver;
            model.Parameter = dict;
            model.SendTime = DateTime.Now;
            //组装JOSN 数据
            List<SendMessageModel> list = new List<SendMessageModel>();
            list.Add(model);
            //组装JOSN数据
            var jsonModel = new
            {
                Title = msgModel.Title,
                Body = list,
                MerchaanNo = AppConfig.MessageMerchantNo,
                ClientSource = "PC",
                ClientSystem = "DM-web",
                Version = 1,
                TimeStamp = DateTime.Now
            };
            string json = JsonProvider.ToJson(jsonModel);
            string sign = MD5Provider.Encode(json + AppConfig.MessageKey);
            var request = new Winner.Framework.Utils.Network.HttpRequestProvider();

            request.SetUrl(AppConfig.MessageUrl)//请求接口地址
                   .AddParameter("Json", json)
                   .AddParameter("Sign", sign);

            var jsonResult = request.POST();

            if (!jsonResult.Success)
            {
                return false;
            }
            return true;
        }
    }


}
