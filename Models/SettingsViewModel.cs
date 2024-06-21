using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTime.Models
{
    internal class SettingsViewModel : INotifyPropertyChanged
    {
        float[] _weekWorkTime = new float[7];

        public string[] WeekWorkTime
        {
            get
            {
                string[] temp = new string[_weekWorkTime.Length];
                for(int i =0;i<temp.Length;i++)
                {
                    temp[i] = ConvertToDate(_weekWorkTime[i]);
                }
                return temp;
            }

        }
        public float[] WeekWorkTimeValue
        {
            get { return _weekWorkTime; } 
                set {
                    _weekWorkTime = value;
                    this.OnPropertyChanged("WeekWorkTime");
                }
        }


        bool[] _workDays = new bool[7];

        public bool[] WorkDays
        {
            get => _workDays; 
            set {
                _workDays = value;
                this.OnPropertyChanged("WorkDays");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private string ConvertToDate(float value)
        {
            DateTime dateTime = default;
            dateTime = dateTime.AddSeconds(value);
            return dateTime.ToString("HH:mm");
        }
        
    }
}
