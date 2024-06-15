using SmartTime.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SmartTime.View
{

    public partial class Settings : UserControl
    {
        List<string> blockedProcesses = new List<string>();
        public Settings()
        {
            InitializeComponent();
            taskViewer.SelectionChanged += taskViewerSelectionChanged;

            Load();
        }

        private void taskViewerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (taskViewer.SelectedItem!=null && blockedProcesses.Contains(taskViewer.SelectedItem.ToString()))
            {
                RemoveAppBtn.IsEnabled = true;
            }
            else
            {
                RemoveAppBtn.IsEnabled = false;
            }
        }

        private void SaveConfigBtn(object sender, RoutedEventArgs e)
        {
            ConfigManager.Config.Token = WakaTimeTokenTB.Text;
            ConfigManager.Config.AppsForBlock = blockedProcesses;

            float workTimeSec = ConvertToSeconds(WorkTimeL.Text);
            ConfigManager.Config.WorkTime = workTimeSec != float.NaN ? workTimeSec : 3600;
            
            ConfigManager.Save();
        }
        //Temp
        private void Load()
        {
            WakaTimeTokenTB.Text = ConfigManager.Config.Token;
            WorkTimeL.Text = ConvertToDate(ConfigManager.Config.WorkTime).ToString("HH:mm");
            blockedProcesses = ConfigManager.Config.AppsForBlock;

            UpdateTaskList();
        }
        



        private void UpdateTaskList()
        {
            taskViewer.Items.Clear();
            foreach (string process in blockedProcesses)
            {
                taskViewer.Items.Add(process);
            }
        }

        public DateTime ConvertToDate(float value)
        {
            DateTime dateTime = default;
            dateTime = dateTime.AddSeconds(value);
            return dateTime;
        }
        public float ConvertToSeconds(string value)
        {
            if (int.TryParse(WorkTimeL.Text.Substring(0, 2), out int hour) && int.TryParse(WorkTimeL.Text.Substring(3), out int minets))
            {
                return (hour * 60 + minets) * 60;
            }
            else
            {
                MessageBox.Show("Invalid time formating");
                return float.NaN;
            }
            
        }

        private void AddAppActionBtn(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


            // Display OpenFileDialog by calling ShowDialog method 
            dlg.ShowDialog();

            if (dlg.FileName.Length <= 0)
                return;
            // Open document 
            string filename = dlg.FileName.Substring(dlg.FileName.LastIndexOf("\\")+1);
            filename = filename.Substring(0,filename.IndexOf("."));
            blockedProcesses.Add(filename);
            UpdateTaskList();
        }

        private void RemoveAppActionBtn(object sender, RoutedEventArgs e)
        {
            var item = taskViewer.SelectedItem;
            if (item != null)
            {
                blockedProcesses.Remove(item.ToString());
                UpdateTaskList();
                RemoveAppBtn.IsEnabled = false;
            }
        }
    }
}
