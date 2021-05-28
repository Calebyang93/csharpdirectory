using System;
using System.Collections.Generic;
using System.Net.Http;
using Model;

namespace ConsoleApp1
{
  class Program
  {
    static void Main(string[] args)
    {

      SimpleAPIClient();

      Console.ReadLine();
      Console.WriteLine("Press ENTER key to exit");
    }

    private static async void SimpleAPIClient()
    {
      int daysAhead = 1;
      string cityName = "Singapore";

      string error = "";
      string baseAddress = "http://localhost:7726/weatherforecast";
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
  }
}
