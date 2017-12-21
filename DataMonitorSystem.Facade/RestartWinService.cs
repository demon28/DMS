using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Winner.Framework.Core;
using Winner.Framework.Core.Facade;
using Winner.Framework.Utils;

namespace Winner.YXH.DataMonitorSystem.Facade
{
    public class RestartWinService : FacadeBase
    {
        /// <summary>  
        /// 执行DOS命令，返回DOS命令的输出  
        /// </summary>  
        public bool Execute()
        {
            try
            {
                Process p = new Process();  // 初始化新的进程
                p.StartInfo.FileName = "CMD.EXE"; //创建CMD.EXE 进程
                p.StartInfo.RedirectStandardInput = true; //重定向输入   
                p.StartInfo.RedirectStandardOutput = true;//重定向输出
                p.StartInfo.UseShellExecute = false; // 不调用系统的Shell
                p.StartInfo.RedirectStandardError = true; // 重定向Error
                p.StartInfo.CreateNoWindow = false; //不创建窗口
                p.Start(); // 启动进程
                p.StandardInput.WriteLine("net stop Winner.YXH.DMS.WinService"); // 输入Cmd 命令
                p.StandardInput.WriteLine("net start Winner.YXH.DMS.WinService"); // 输入Cmd 命令
                p.StandardInput.WriteLine("exit"); // 退出
                string str = p.StandardOutput.ReadToEnd(); //将输出赋值给 S
                p.WaitForExit();  // 等待退出
                Log.Info(str);
            }
            catch (Exception ex)
            {
                Log.Info("重启服务失败异常:" + ex);
                Alert("重启服务失败：" + ex.Message);
                return false;
            }
            return true;
        }
    }
}
