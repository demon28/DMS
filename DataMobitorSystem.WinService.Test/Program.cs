using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.YXH.DataMonitorSystem.Facade;

namespace DataMobitorSystem.WinService.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                MobitorJob job = new MobitorJob();
                job.Start();
            }
        }
    }
}
