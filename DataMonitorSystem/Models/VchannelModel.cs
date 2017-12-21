using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winner.YXH.DataMonitorSystem.Entities;
using Winner.Framework.Utils;

namespace DataMonitorSystem.Models
{
    /// <summary>
    /// 栏目Model
    /// </summary>
    public class VchannelModel
    {
        /// <summary>
        /// 栏目编号
        /// [default:0]
        /// </summary>
        public int ChannelId { get; set; }


        /// <summary>
        /// 栏目名称
        /// [default:string.Empty]
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 父级ID
        /// [default:0]
        /// </summary>
        public int FatherId { get; set; }

        /// <summary>
        /// 状态{禁用=0,启用=1,删除=2}
        /// [default:0]
        /// </summary>
        public ChannelStatus Status { get; set; }

        /// <summary>
        /// 栏目描述
        /// [default:string.Empty]
        /// </summary>
        public string Depict { get; set; }

        /// <summary>
        /// 栏目备注
        /// [default:string.Empty]
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 创建时间
        /// [default:new DateTime()]
        /// </summary>
        public DateTime CreateTime { get; set; }


        public int TreeNodeId { get; set; }
    }

    public class VchannelTreeModel
    {
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }

        public int id { get; set; }

        public string text { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateTimeText
        {

            get
            {
                return CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {

                CreateTimeText = value;
            }
        }

        public int FatherId { get; set; }

        public string Remarks { get; set; }
        public string Depict { get; set; }

        public ChannelStatus Status { get; set; }

        public string StatusText
        {

            get
            {
                return Status.ToString();
            }
            set
            {

                StatusText = value;
            }
        }

        public string state { get; set; }


    }

}
