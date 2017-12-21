using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winner.YXH.DataMonitorSystem.Entities;

namespace DataMonitorSystem.Models
{
    public class TreeModel
    {
        // <summary>
        /// 前端ui分类表Id
        /// </summary>
        public int id
        {
            get;
            set;
        }



        /// <summary>
        /// 前端UI框架分类名称必用text
        /// [default:string.Empty]
        /// </summary>
        public string text
        {
            get;
            set;
        }


        public string state { get; set; } //是否选中
        public List<TreeModel> children { get; set; } //子节点
        public override string ToString()
        {
            return string.Format("id:{0},text:{1},state:{2},children:{3}", id, text, state, children);
        }

        public string iconCls { get; set; }

        public ChannelStatus Status { get; set; }
    }
}
