using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace WPFSimple
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

    private void btnHello_Click(object sender, RoutedEventArgs e)
    {
      tb1.Text += " Hello";
      //tb1.Foreground = GetRandomBrush(true);
      gridMain.Background = GetRandomBrush(false);
    }

    private Brush GetRandomBrush(Boolean niceColoursOnly)
    {
      Random rnd = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));  // without this Guid stuff, the number wd prob be the same within a loop

      Brush result;

      if (niceColoursOnly)
      {
        string[] niceColourNames = new string[]
          {
          "IndianRed",
          "SaddleBrown",
          "SlateGray",
          "LightSlateGray",
          "DarkSlateGray",
          "DarkTurquoise",
          "Firebrick",
          "CadetBlue",
          "LemonChiffon",
          "Maroon",
          "Gold",
          "Tan",
          "SandyBrown",
          "NavajoWhite",
          "DimGray",
          "LightSkyBlue",
          "DarkCyan",
          "Salmon",
          "Orange",
          "Peru",
          "Moccasin"
          };
        int random = rnd.Next(niceColourNames.Length);
        string randomColourName = niceColourNames[random];
        //WindowTooltip = randomColourName;
        try
        {
          Color c = (Color)ColorConverter.ConvertFromString(randomColourName);
          result = new SolidColorBrush(c);
        }
        catch (Exception ex)
        {
          result = new SolidColorBrush(Colors.Black);
        }
        return result;
      }
      else
      {
        // http://stackoverflow.com/questions/6084398/pick-a-random-brush
        result = Brushes.Transparent;
        Type brushesType = typeof(Brushes);
        PropertyInfo[] properties = brushesType.GetProperties();
        int random = rnd.Next(properties.Length);
        result = (Brush)properties[random].GetValue(null, null);
        //WindowTooltip = properties[random].Name;
        return result;
      }
    }

  }
}
