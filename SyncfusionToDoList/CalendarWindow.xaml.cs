using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SyncfusionToDoList
{
    /// <summary>
    /// Interaction logic for CalendarWindow.xaml
    /// </summary>
    public partial class CalendarWindow : RibbonWindow
    {
		
        public CalendarWindow()
        {
            InitializeComponent();
            calendarEdit2.Loaded += CalendarEdit2_Loaded;
            RemoveGroupBarOverFlowButton();
			
        }
        private void CalendarEdit2_Loaded(object sender, RoutedEventArgs e)
        {
            RemoveCalendarNavigationButton();
        }
        public void RemoveGroupBarOverFlowButton()
        {
            foreach (ToggleButton item in VisualUtils.EnumChildrenOfType(groupBar, typeof(ToggleButton)))
            {
                if (item.Name == "PART_OverFlowButton")
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
        }

        public void RemoveCalendarNavigationButton()
        {
            foreach (StackPanel item in VisualUtils.EnumChildrenOfType(calendarEdit2, typeof(StackPanel)))
            {
                if (item.Name == "PART_NextMonthButtonPanel" || item.Name == "PART_PrevMonthButtonPanel")
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
