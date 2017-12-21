/***************************************************
 *
 * Business Logic Layer Of Winner Framework
 * CreateTime :  
 * Version : V 1.0.0.0
 * E_Mail : 6e9e@163.com
 * Blog : http://www.cnblogs.com/fineblog/
 * Copyright (C) Winner(深圳-乾海盛世)
 * 
 * 温馨提示：本文件有代码段生成，只需要修改和扩展TODO的部分
 ***************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;
using Winner.YXH.DataMonitorSystem.DataAccess;
using Winner.YXH.DataMonitorSystem.Entities;
namespace Winner.Project.Facade
{
    /// <summary>
    ///  栏目 实体业务对象
    /// </summary>
    public class Channel : FacadeBase
    {
        public Channel(int? id = null)
        {
            _id = id;
        }
        #region 私有字段

        private int? _id;
        private Tdms_Channel _dal;

        #endregion

        #region 公开属性

        /// <summary>
        /// DAL实体
        /// </summary>
        public Tdms_Channel DAL
        {
            get
            {
                if (_dal == null)
                {
                    _dal = new Tdms_Channel();
                    _dal.ReferenceTransactionFrom(this.Transaction);
                    if (_id.HasValue)
                    {
                        _dal.SelectByPK(_id.Value);
                    }
                }
                return _dal;
            }
            private set
            {
                _dal = value;
            }
        }

        /// <summary>
        /// 数据库是否当前 栏目 对象
        /// </summary>
        public bool IsExist
        {
            get
            {
                return DAL.ChannelId > 0;
            }
        }

        /// <summary>
        ///  栏目 主键
        /// </summary>
        [Range(1, 1000000, ErrorMessage = "{0}无效，必须在{1}至{2}范围内")]
        [Display(Name = "栏目", GroupName = "Update|Delete|Select")]
        public int ChannelId
        {
            get { return DAL.ChannelId; }
            set { DAL.ChannelId = value; }
        }

        /// <summary>
        /// 栏目名称
        /// [default:string.Empty]
        /// </summary>
        public string ChannelName
        {
            get { return DAL.ChannelName; }
            set { DAL.ChannelName = value; }
        }
        /// <summary>
        /// 父级ID
        /// [default:0]
        /// </summary>
        public int FatherId
        {
            get { return DAL.FatherId; }
            set { DAL.FatherId = value; }
        }
        /// <summary>
        /// 状态{禁用=0,启用=1,删除=2}
        /// [default:0]
        /// </summary>
        public ChannelStatus Status
        {
            get { return DAL.Status; }
            set { DAL.Status = value; }
        }

        /// <summary>
        /// 栏目备注
        /// [default:string.Empty]
        /// </summary>
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}无效,必须在{1}至{2}个字符")]
        [Required(ErrorMessage = "{0}不可为空")]
        [Display(Name = "备注信息")]
        public string Remarks
        {
            get { return DAL.Remarks; }
            set { DAL.Remarks = value; }
        }
        /// <summary>
        /// 创建时间
        /// [default:new DateTime()]
        /// </summary>
        public DateTime CreateTime
        {
            get { return DAL.CreateTime; }
        }


        #endregion

        #region 公开方法

        /// <summary>
        /// 新增 栏目 到数据库
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            if (!SaveVerify("Add"))
            {
                return false;
            }
            if (!DAL.Insert())
            {
                Alert("录入 栏目 数据失败！", DAL.PromptInfo);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 编辑 栏目 到数据库
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            if (!IsExist)
            {
                Alert(ResultType.数据库查不到数据, "数据库不存在 栏目 数据，修改失败！");
                return false;
            }
            if (!SaveVerify("Update"))
            {
                return false;
            }
            if (!DAL.Update())
            {
                Alert("修改 栏目 数据失败！", DAL.PromptInfo);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 从数据库删除当前 栏目 对象
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            if (!DAL.Delete())
            {
                Alert("删除 栏目 数据失败！", DAL.PromptInfo);
                return false;
            }
            //DAL.ReferenceTransactionFrom(this.Transaction);
            //Tdms_ChannelCollection dachnnelCool = new Tdms_ChannelCollection();
            //if (!dachnnelCool.listByFatherId(DAL.ChannelId))
            //{
            //    return true;
            //}
            //else
            //{
            //    Tdms_Channel dachannel = new Tdms_Channel();
            //    if (!dachannel.ChannelUpdate(DAL.ChannelId))
            //    {
            //        Alert("删除 栏目 数据失败！", DAL.PromptInfo);
            //        return false;
            //    }
            //}

            return true;
        }

        /// <summary>
        /// 禁用和启用
        /// </summary>
        /// <param name="channelId">栏目编号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public bool Operate(int channelId, string status)
        {
            Tdms_Channel daChannel = new Tdms_Channel();
            if (!daChannel.SelectByPK(channelId))
            {
                Alert("查询失败" + daChannel.PromptInfo.MessageStack);
                return false;
            }
            if (status == "Open")
            {
                if (daChannel.Status == ChannelStatus.禁用)
                {
                    daChannel.Status = ChannelStatus.启用;
                    if (!daChannel.Update())
                    {
                        Alert("启用失败" + daChannel.PromptInfo.MessageStack);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            if (status == "Close")
            {
                if (daChannel.Status == ChannelStatus.启用)
                {
                    daChannel.Status = ChannelStatus.禁用;
                    if (!daChannel.Update())
                    {
                        Alert("禁用失败" + daChannel.PromptInfo.MessageStack);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 验证表单提交数据合法性
        /// </summary>
        /// <returns></returns>
        private bool SaveVerify(string groupName)
        {
            var verifyResult = ModelVerify.First(this, groupName);
            if (verifyResult != null)
            {
                Alert(ResultType.非法数值, verifyResult.ErrorMessage);
                return false;
            }
            //TODO:其他的检查
            return true;
        }

        #endregion

        /// <summary>
        /// Tdms_Channel 隐式转换 Channel
        /// </summary>
        /// <param name="dal"></param>
        /// <returns></returns>
        public static implicit operator Channel(Tdms_Channel dal)
        {
            Channel obj = new Channel();
            obj.DAL = dal;
            return obj;
        }
    }
}
