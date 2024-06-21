using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartTime.Services
{
    static class TaskManagerController
    {

       


        public static List<string> blockedProcesses = new List<string>();
        static int intesityNormalUpdates = 0;
        
        public static void StartAsync()
        {
            var timer = new System.Threading.Timer(
            e => NormalUpdateEvent(),
            null,
            TimeSpan.Zero,
            TimeSpan.FromMinutes(15));
        }

        private static void VerifyViolation()
        {
            if (intesityNormalUpdates > 0)
            {
                //Start more intesive closing apps
                IntesUpdateEvent();

            }
        }

        private static void NormalUpdateEvent()
        {

            while (true)
            {
                //MessageBox.Show("");
                if (AppStats.finishedWork)
                {
                    return;
                }
                bool someClosed = false;
                var processes = Process.GetProcesses().ToList();
                foreach (Process process in processes)
                {
                    if (blockedProcesses.Contains(process.ProcessName))
                    {
                        process.Kill();
                        someClosed = true;
                    }
                }
                intesityNormalUpdates = someClosed ? +1 : -1;
                VerifyViolation();
                Thread.Sleep(ConfigManager.Config.VerificationFrequency * 1000);
            }
            
        }
        private static void IntesUpdateEvent()
        {
            while (true)
            {
                if (AppStats.finishedWork)
                {
                    return;
                }
                Thread.Sleep(ConfigManager.Config.VerificationFrequency * 2000 /3000);
                bool someClosed = false;
                var processes = Process.GetProcesses().ToList();
                foreach (Process process in processes)
                {
                    if (blockedProcesses.Contains(process.ProcessName))
                    {
                        process.Kill();
                        someClosed = true;
                    }
                }
                intesityNormalUpdates = someClosed ? +1 : -1;
                VerifyViolation();
            }

        }
    }
}
