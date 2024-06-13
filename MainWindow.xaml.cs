using SmartTime.Services;
using SmartTime.View;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;


namespace SmartTime
{

    public partial class MainWindow : Window
    {
        public MainView mainView;
        Settings settingsView;

        public MainWindow()
        {


            ConfigManager.Load();

            TaskManagerController.blockedProcesses = ConfigManager.Config.AppsForBlock;
            TaskManagerController.StartAsync();

            InitializeComponent();

            mainView = new MainView();
            settingsView = new Settings();

            MainFrame.Content = mainView;



            // Retrieve the command-line arguments
            string[] args = Environment.GetCommandLineArgs();

            // Display the arguments
            if (args.Length > 1 && args[1] == "withoutUI")
            {
                MessageBox.Show($"Argument : {args[1]}", "Command-line Argument");
                this.Hide();
            }
            else
            {
                MessageBox.Show($"Argument : {args[0]}", "Command-line Argument");

                this.Show();
            }
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.Hide();
            e.Cancel = true;

        }

        private void SettingsWindowBtn(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = settingsView;
        }
        private void MainWindowBtn(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = mainView;
        }
        private void GitHubPageBtn(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.w3schools.com/howto/tryit.asp?filename=tryhow_js_tab_img_gallery");
        }
    }

}
