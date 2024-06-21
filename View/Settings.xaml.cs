using SmartTime.Models;
using SmartTime.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SmartTime.View
{

    public partial class Settings : UserControl
    {
        List<string> blockedProcesses = new List<string>();

        SettingsViewModel viewModel;

        public Settings()
        {
            InitializeComponent();
            taskViewer.SelectionChanged += taskViewerSelectionChanged;
            Load();

            base.DataContext = viewModel;
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



            //Combo box convert

            if (VerifyFrequencyComboBox.SelectedIndex != -1)
            {
                ConfigManager.Config.VerifComboBoxIndex = VerifyFrequencyComboBox.SelectedIndex;
                string comboBoxText = VerifyFrequencyComboBox.Text;
                string value = comboBoxText.Substring(0, comboBoxText.IndexOf(' ')).Replace('.', ',');
                string type = comboBoxText.Substring(comboBoxText.IndexOf(' ') + 1);

                if (float.TryParse(value, out float result))
                {
                    if (type == "sec")
                    {
                        ConfigManager.Config.VerificationFrequency = (int)result;
                    }
                    else if (type == "min")
                    {
                        ConfigManager.Config.VerificationFrequency = (int)result * 60;
                    }
                }
            }

            GetSchedule();
            ConfigManager.Config.WorkTimeSchedule = viewModel.WeekWorkTimeValue;
            ConfigManager.Config.weekWorkSchedule = viewModel.WorkDays;
            
            ConfigManager.Save();
        }
        //Temp
        private void Load()
        {
            WakaTimeTokenTB.Text = ConfigManager.Config.Token;
            blockedProcesses = ConfigManager.Config.AppsForBlock;

            VerifyFrequencyComboBox.SelectedIndex = ConfigManager.Config.VerifComboBoxIndex;

            viewModel = new SettingsViewModel
            {
                WorkDays = ConfigManager.Config.weekWorkSchedule,
                WeekWorkTimeValue = ConfigManager.Config.WorkTimeSchedule
            };



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

        private DateTime ConvertToDate(float value)
        {
            DateTime dateTime = default;
            dateTime = dateTime.AddSeconds(value);
            return dateTime;
        }
        private void GetSchedule()
        {
            List<float> output = new List<float>();
            foreach (TextBox x in Shedule.Children.OfType<TextBox>())
            {
                var value = x.Text;
                if (int.TryParse(value.Substring(0, 2), out int hour) && int.TryParse(value.Substring(3), out int minets))
                {
                    output.Add((hour * 60 + minets) * 60);
                }
                else
                {
                    output.Add(float.NaN);
                }
            }
            List<bool> outputBools = new List<bool>();
            foreach (CheckBox x in Shedule.Children.OfType<CheckBox>())
            {
                bool newBool = x.IsChecked==true ? true : false;
                outputBools.Add(newBool);
                
            }


            viewModel.WorkDays = outputBools.ToArray();
            viewModel.WeekWorkTimeValue = output.ToArray();


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
