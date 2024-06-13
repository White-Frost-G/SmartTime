using System;
using System.Drawing;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Forms = System.Windows.Forms;

namespace SmartTime
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Forms.NotifyIcon notify;
        public App()
        {
            notify = new Forms.NotifyIcon();
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            notify.Icon = new Icon("resources/taskBarIcon.ico");
            notify.Visible = true;
            notify.ContextMenuStrip = new Forms.ContextMenuStrip();
            notify.ContextMenuStrip.Items.Add("Status",null,onClick:OnStatusClick);
            notify.ContextMenuStrip.Items.Add("Exit",null,onClick:OnExitClick);
            //notify.ShowBalloonTip(3000,"SmartTime","Started",Forms.ToolTipIcon.Info);
            base.OnStartup(e);
        }

        protected void OnExitClick(object sender, EventArgs e)
        {
            notify.Dispose();
            base.Shutdown();
        }

        private void OnStatusClick(object sender, EventArgs e)
        {
            MainWindow.WindowState = WindowState.Normal;
            MainWindow.Activate();
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notify.Dispose();
            base.OnExit(e);
        }
    }
}
