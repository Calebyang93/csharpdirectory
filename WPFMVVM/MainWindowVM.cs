using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace WPFMVVM
{
    public class MainWindowVM : ViewModelBase
    {
        public string TestText { get { return GetProperty(() => TestText); } set { SetProperty(() => TestText, value); } }
        public string CountryName { get { return GetProperty(() => CountryName); } set { SetProperty(() => CountryName, value); } }
        public string Info { get { return GetProperty(() => Info); } set { SetProperty(() => Info, value); } }

        public List<string> Countries { get; set; }
        public Brush GridMainBackground { get { return GetProperty(() => GridMainBackground); } set { SetProperty(() => GridMainBackground, value); } }

        public MainWindowVM()
        {
          Countries = new List<string>(new string[] { "Russia", "France", "Italy" }); 
        }

        public Boolean CanClickMe()
        {
            return !string.IsNullOrEmpty(CountryName);
        }

        [Command]
        public void ClickMe()
        {
            //TestText += " Hello";
            TestText +=  CountryName;
            GridMainBackground = GetRandomBrush(false);
        }

        public Boolean CanSave()
        {
            return string.IsNullOrEmpty(TestText);
        }

        [Command]
        public void Save()
        {
            Info = "Saved";
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
