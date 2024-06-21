using SmartTime.Services;
using System;
using System.ComponentModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;




namespace SmartTime.Models
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        float _nowWorkTime;
        public float NeedWorkTimeValue
        {
            get { return ConfigManager.Config.GetTodayWorkTime - _nowWorkTime; }
        }
        public string NeedWorkTimeString
        {
            get
            {
                if(ConfigManager.Config.GetTodayWorkTime - _nowWorkTime >= 0)
                {
                    DateTime dateTime = new DateTime();
                    dateTime = dateTime.AddSeconds(ConfigManager.Config.GetTodayWorkTime - _nowWorkTime);
                    return dateTime.ToString("HH:mm");
                }
                else
                {
                    return "Finished";
                }
            }
        }
        public float NowWorkTimeValue
        {
            get { return _nowWorkTime; }
            set { _nowWorkTime = value;
                this.OnPropertyChanged("NowWorkTimeString");
                this.OnPropertyChanged("NeedWorkTimeString");
            }
        }
        public string NowWorkTimeString
        {
            get {
                DateTime dateTime = new DateTime();
                dateTime = dateTime.AddSeconds(_nowWorkTime);
                return "Your time: " + dateTime.ToLongTimeString(); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if(this.PropertyChanged!=null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
