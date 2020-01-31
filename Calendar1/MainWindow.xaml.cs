using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calendar1
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }
    /*
    private void Button_Click(object sender, RoutedEventArgs e)
    {
      if (calendarapp.App.xaml.mybutton.Content.ToString() == "Book")
      {
        private object = mybutton; 
 
        calendarapp.App.xaml.mybutton.Content = "Save";
  }
          
      }
    }
    */
    private void BtnDoSomething_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Clicked!");
    }

    private void BtnBook_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Book clicked");
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Save clicked");
    }

  }
}
