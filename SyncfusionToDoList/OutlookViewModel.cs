using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.UI.Xaml.Schedule;
using Syncfusion.Windows.Shared;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Media;
using Syncfusion.Windows.Tools.Controls;

namespace SyncfusionToDoList
{
    public class OutlookViewModel : INotifyPropertyChanged
    {
        public OutlookViewModel()
        {
         
            InitializeCalendar();
           
        }


        #region Calendar

        private DateTime firstCalender = DateTime.Now;

        public DateTime FirstCalender
        {
            get { return firstCalender; }
            set
            {
                firstCalender = value;
                if (SecondCalender != null)
                {
                    SecondCalender = value.AddMonths(1);
                }
                OnPropertyChanged("FirstCalender");
            }
        }

        private DateTime secondCalender;
        public DateTime SecondCalender
        {
            get { return secondCalender; }
            set
            {
                secondCalender = value;
                OnPropertyChanged("SecondCalender");
            }
        }

        private ScheduleType scheduleType;

        public ScheduleType ScheduleType
        {
            get { return scheduleType; }
            set
            {
                scheduleType = value;
                OnPropertyChanged("ScheduleType");
            }
        }
        public void InitializeCalendar()
        {
            FirstCalender = DateTime.Today;
            ScheduleType = ScheduleType.Month;
        }

        #endregion

     

        #region PropertyHandler
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #endregion
    }
}
