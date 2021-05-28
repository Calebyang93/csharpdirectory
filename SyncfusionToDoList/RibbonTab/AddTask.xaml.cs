using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Syncfusion.Windows.Shared;

namespace TD.SFWPF.RibbonTab
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        public AddTask()
        {
            CalendarEdit = new CalendarEdit();
            CalendarEdit.Height = 200;
            CalendarEdit.Width = 400;

        }

        public CalendarEdit CalendarEdit { get; private set; }
    }
}
