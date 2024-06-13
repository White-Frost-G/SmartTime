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
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {

        public MainView()
        {
            InitializeComponent();
            LoadStatsAsync();
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
                return;
            }

            DateTime dateTime = new DateTime();
            dateTime = dateTime.AddSeconds(time);
            NowWorkTimeLabel.Content = "Your time: " + dateTime.ToLongTimeString();

            //Temp
            float needTime = ConfigManager.Config.WorkTime;
            if(needTime - time >= 0)
            {
                DateTime dateTimeNeed = new DateTime();
                dateTimeNeed = dateTimeNeed.AddSeconds(needTime - time);
                NeedTimeToWorkLabel.Content = "Your need to finish: " + dateTimeNeed.ToLongTimeString();
                NeedTimeToWorkLabel.Foreground = new SolidColorBrush(Color.FromRgb(44, 62, 80));
                AppStats.finishedWork = false;
                return;
            }
            NeedTimeToWorkLabel.Foreground = new SolidColorBrush(Color.FromRgb(46, 204, 113));
            NeedTimeToWorkLabel.Content = "Finished";
            AppStats.finishedWork = true;
        }
    }
}
