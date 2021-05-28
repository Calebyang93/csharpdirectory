using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading;
using Model;
using Model2;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1
{
  class Program
  {
    public static object filenameTxt { get; private set; }

    //private static filenameTxt()
    //{
    //}

    static void Main(string[] args)
    {
      // SimpleAPIClient();

      WeatherAPIClient1();

      // SerialiseJSONTest1();

      //SerialiseJSONTest2();

      Console.ReadLine();
      Console.WriteLine("Press ENTER key to exit");
    }

    private static void SerialiseJSONTest2()
    {

      double x = 1.2345;
      string s1 = string.Format("The number is {0:#.00}",x);
      // string interpolation for $ sign 
      string s2 = $"The number is {x:#.00}";
      string s3 = $"{DateTime.Now.AddDays(-10):dd.MMM}";
      string s4 = $"{DateTime.Now.ToUniversalTime()}";
      Console.WriteLine(s1);
      Console.WriteLine(s2);
      Console.WriteLine(s3);
      Console.WriteLine(s4);

      /*
      var wd = JsonSerializer.Deserialize<WeatherData>(json1);

      double timestamp = 1385701110; 
      System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
      dateTime = dateTime.AddSeconds(timestamp);
      string cityName = wd.city.name;
      System.DateTime dt0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
      DateTime dtw = dt0.AddSeconds(wd.list[0].dt);
      Console.WriteLine($"The temperature in {wd.city.name} on {dtw} was {(wd.list[0].temp.day - 273):0.0}"); 
      */
    }

    private static void SerialiseJSONTest1()
    {
      Person p1 = new Person() { FirstName = "John", LastName = "Wong" };
      Console.WriteLine("p1: " + p1);

      string s = JsonSerializer.Serialize<Person>(p1);
      Console.WriteLine("Serialised: " + s);

      Person p2 = JsonSerializer.Deserialize<Person>(s);
      Console.WriteLine("p2: " + p2);
    }

    private static async void WeatherAPIClient1()
    {
      string cityName = "London";
      int citiTTemp = 12;

      // Works: api.openweathermap.org/data/2.5/weather?q=london&appid=fb9e0d8327cbfad455e3488b977c8a0f

      string error = "";
      string baseAddress = "http://api.openweathermap.org/data/2.5";
      string path = $"{baseAddress}/weather?q={cityName}&appid=fb9e0d8327cbfad455e3488b977c8a0f";
      string jsonStr = "";

      try
      {
        HttpClient c = new HttpClient();
        c.BaseAddress = new Uri(baseAddress);

        using (var response = await c.GetAsync(path).ConfigureAwait(false))
        {
          if (response.IsSuccessStatusCode)
          {
            //lst = await response.Content.ReadAsAsync<List<WeatherForecast>>().ConfigureAwait(false);
            jsonStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            //var wd = await response.Content.ReadAsAsync<WeatherData>().ConfigureAwait(false);
            var wd = JsonSerializer.Deserialize<WeatherMapData2>(jsonStr);

          }
          else
            error = response.ReasonPhrase + " [Path: " + path + "]";
        }

      }
      catch (Exception ex)
      {
        Console.WriteLine($"Exception: {ex.Message}");
      }

      // Show results
      Console.WriteLine("Weather data as string:");
      Console.WriteLine(jsonStr);

      if (!string.IsNullOrEmpty(error))
        Console.WriteLine("Error: " + error);
      else
        Console.WriteLine("No errors");
    }

    private static void PersistAsLocalStrings()
    {
      SaveLocalDatesAsString();
      RestoreLocalDatesFromString();
    }

    private static void SaveLocalDatesAsString()
    {
      DateTime[] dates = { new DateTime(2014, 6, 14, 6, 32, 0),
                   new DateTime(2014, 7, 10, 23, 49, 0),
                   new DateTime(2015, 1, 10, 1, 16, 0),
                   new DateTime(2014, 12, 20, 21, 45, 0),
                   new DateTime(2014, 6, 2, 15, 14, 0) };
      string output = null;

      Console.WriteLine($"Current Time Zone: {TimeZoneInfo.Local.DisplayName}");
      Console.WriteLine($"The dates on an {Thread.CurrentThread.CurrentCulture.Name} system:");
      for (int ctr = 0; ctr < dates.Length; ctr++)
      {
        Console.WriteLine(dates[ctr].ToString("f"));
        output += dates[ctr].ToString() + (ctr != dates.Length - 1 ? "|" : "");
      }
      var sw = new StreamWriter((Stream)filenameTxt);
      sw.Write(output);
      sw.Close();
      Console.WriteLine("Saved dates...");
    }

    private static void RestoreLocalDatesFromString()
    {
      TimeZoneInfo.ClearCachedData();
      Console.WriteLine($"Current Time Zone: {TimeZoneInfo.Local.DisplayName}");
      Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
      StreamReader sr = new StreamReader((string)filenameTxt);
      string[] inputValues = sr.ReadToEnd().Split(new char[] { '|' },
                                                  StringSplitOptions.RemoveEmptyEntries);
      sr.Close();
      Console.WriteLine("The dates on an {0} system:",
                        Thread.CurrentThread.CurrentCulture.Name);
      foreach (var inputValue in inputValues)
      {
        DateTime dateValue;
        if (DateTime.TryParse(inputValue, out dateValue))
        {
          Console.WriteLine($"'{inputValue}' --> {dateValue:f}");
        }
        else
        {
          Console.WriteLine("Cannot parse '{inputValue}'");
        }
      }
      Console.WriteLine("Restored dates...");
    }

    private static async void SimpleAPIClient()
    {
      int daysAhead = 1;
      string cityName = "Singapore";

      string error = "";
      string baseAddress = "https://localhost:44328/weatherforecast";
      string path = $"{baseAddress}/api/forecastbycity/{daysAhead}/{cityName}";
      List<WeatherForecast> lst = new List<WeatherForecast>();

      try
      {
        HttpClient c = new HttpClient();
        c.BaseAddress = new Uri(baseAddress);

        using (var response = await c.GetAsync(path).ConfigureAwait(false))
        {
          if (response.IsSuccessStatusCode)
          {
            lst = await response.Content.ReadAsAsync<List<WeatherForecast>>().ConfigureAwait(false);
          }
          else
            error = response.ReasonPhrase + " [Path: " + path + "]";
        }

      }
      catch (Exception ex)
      {
        Console.WriteLine($"Exception: {ex.Message}");
      }

      // Show results
      Console.WriteLine("Forecast:");
      foreach (var item in lst)
        Console.WriteLine(item); // NOTE - this only works as it does because ToString has been implemented in WeatherForecast

      if (!string.IsNullOrEmpty(error))
        Console.WriteLine("Error: " + error);
      else
        Console.WriteLine("No errors");
    }
    static string json1 = @"{
	""cod"": ""200"",
	""city"": {
		""id"": 2643743,
		""name"": ""London"",
		""coord"": {
			""lon"": -0.1277,
			""lat"": 51.5073
		},
		""country"": ""GB""
	},
	""message"": 0.353472054,
	""list"": [
		{
			""dt"": 1594382400,
			""sunrise"": 1594353335,
			""sunset"": 1594412149,
			""temp"": {
				""day"": 286.98,
				""min"": 285.22,
				""max"": 287.97,
				""night"": 285.22,
				""eve"": 287.97,
				""morn"": 287.29

      },
			""feels_like"": {
				""day"": 282.61,
				""night"": 283.19,
				""eve"": 284.98,
				""morn"": 282.68
			},
			""pressure"": 1016,
			""humidity"": 84,
			""weather"": [
				{
					""id"": 500,
					""main"": ""Rain"",
					""description"": ""light rain"",
					""icon"": ""10d""
				}
			],
			""speed"": 6.78,
			""deg"": 320,
			""clouds"": 81,
			""rain"": 1.96
		}
	]
}";

        // relief pattern api 
    private static void reliefMapApi()
        {
            int zl;
            float xCoord;
            float yCoord;
            string cityName = "Singapore";
            string error = "";
            string baseAddress = "http://maps.openweathermap.org/maps/2.0/relief/{z}/{x}/{y}?appid={fb9e0d8327cbfad455e3488b977c8a0f}";
            string path = $"{baseAddress}/reliefPattern?q={cityName}&appid=fb9e0d8327cbfad455e3488b977c8a0f";
            string jsonStr2 = "";
            string path = $"{baseAddress} /reliefMapApi/reliefpattern/{z1}/{xCoord}/{yCoord}";
            List<reliefPattern> lst = new List<reliefPattern>();

            try
            {
                HttpClient c = new HttpClient();
                c.BaseAddress = new Uri(baseAddress);

                using (var response = await c.GetAsync(path).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        lst = await response.Content.ReadAsAsync<List<WeatherForecast>>().ConfigureAwait(false);
                    }
                    else
                        error = response.ReasonPhrase + " [Path: " + path + "]";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            // http://maps.openweathermap.org/maps/2.0/relief/{z}/{x}/{y}?appid={api_key}
        }
        //deserialize object 
        public class Weather
    {
      public string cityName { get; set; }
      public int temperatureKelvin { get; set; }
      public DateTime date { get; set; }
      public string Summary { get; set; }

    }
  }
}


