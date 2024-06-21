using SmartTime.Models;
using SmartTime.Services;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Windows.Media.Color;


namespace SmartTime.View
{
    


    public partial class MainView : UserControl
    {
        MainViewModel viewModel = new MainViewModel();

        public MainView()
        {
            InitializeComponent();
            LoadStatsAsync();
            base.DataContext = viewModel;
        }
        private void UpdateStats(object sender, RoutedEventArgs e)
        {
            LoadStatsAsync();
        }
        public async void LoadStatsAsync()
        {
            WakaAuth WakaAuth = new WakaAuth(ConfigManager.Config.Token);
            float time = await WakaAuth.GetTodayTimeAsync();
            if(time.Equals(float.NaN))
            {
                viewModel.NowWorkTimeValue = 0;
                return;
            }
        }
    }
}
