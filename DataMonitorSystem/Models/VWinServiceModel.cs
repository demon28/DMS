using Winner.YXH.DataMonitorSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMonitorSystem.Models
{
    public class VWinServiceModel
    {
        #region 公开属性

        /// <summary>
        /// 主键
        /// [default:0]
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// 周期,DeductCycle:Hour=0,Day=1,Week=2,Monthly=4,Year=8,
        /// [default:0]
        /// </summary>
        public DeductCycle Cycle
        {
            get;
            set;
        }
        /// <summary>
        /// 下次运行时间
        /// [default:new DateTime()]
        /// </summary>
        public DateTime NextRunTime
        {
            get;
            set;
        }
        /// <summary>
        /// 状态:成功 = 1,失败 = 2,运行中 = 3,
        /// [default:0]
        /// </summary>
        public RunStatus Status
        {
            get;
            set;
        }
        /// <summary>
        /// 运行结果：失败=-1,成功但是没有数据=0,成功监控到n条数据=n
        /// [default:0]
        /// </summary>
        public int? RunResult
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// [default:new DateTime()]
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// [default:string.Empty]
        /// </summary>
        public string Reamrks
        {
            get;
            set;
        }

        #endregion 公开属性
    }
}