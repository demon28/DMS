using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Winner.Framework.Core;
using Winner.Framework.Utils;
using Winner.YXH.DataMonitorSystem.Facade;

namespace DMS.WinService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        Thread jobThread { get; set; }
        protected override void OnStart(string[] args)
        {
            Log.Info("WinService OnStart");
            MobitorJob winService = new MobitorJob();
            jobThread = new Thread(winService.Start);
            jobThread.Start();
        }



        protected override void OnStop()
        {
            if (jobThread != null)
            {
                jobThread.Abort();
            }
            Log.Info("WinService OnStop");
        }
    }
}
