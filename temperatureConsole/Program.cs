using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading;
using Model2;
using System.Text.Json;
using System.Text.Json.Serialization;
using TD.Data;
using Model;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TD.Data.Interface;
using TD.Data.Mock;
using TD.Model;

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

            // WeatherAPIClient1();

            // SerialiseJSONTest1();

            //SerialiseJSONTest2();

            // TDDatatest_GetAll();
            // TDDataTest_GetByStatus();
            // TDDataTest_GetByDeadline();
            // TDDataTest_Add();
            //TDDataTest_GetByDescription();

            //TDDataTest_DeleteByid();
            //TDDataTest_Delete();

            //APIClientTest_ResultOk();
            //APIClientTest_ResultError();
            //APIClientTest_GetToDoItem();

            // APIClientTest_GetByStatus();

            // APIClientTest_PostAdd();
            //APIClientTest_Update();
            RepositoryTest1();
            
            Console.ReadLine();
            Console.WriteLine("Press ENTER key to exit");
        }

        private static void RepositoryTest1()
        {
            //ToDoRepositoryMock rep = new ToDoRepositoryMock();
            IToDoRepository rep = new ToDoRepositoryMock();
            var lst = rep.GetAll();
        }

        private static async void APIClientTest_Update()
        {
            // https://localhost:44328/todo/api/itemupdate
            string baseAddress = "https://localhost:44328/todo/api/itemupdate";
            string path = $"{baseAddress}/api/itemupdate";

            ToDoItem td = new ToDoItem()
            {
                ID = Guid.NewGuid(),
                Category = "Household Chores",
                CreatedOn = DateTime.Now,
                Deadline = DateTime.Now.AddDays(7),
                Description = "Vacuum and Mop the floor",
                Status = ToDoStatus.InProgress,
                UserName = Environment.UserName
            };

            HttpClient c = new HttpClient();
            c.BaseAddress = new Uri(baseAddress);

            var jsonData = JsonSerializer.Serialize<ToDoItem>(td);
            var httpData = new StringContent(jsonData, Encoding.UTF8, "application/json");

            using (var response = await c.PutAsync(path, httpData).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Status: {response.StatusCode}");
                }
                else
                {
                    string jsonStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    ProblemDetails pd = JsonSerializer.Deserialize<ProblemDetails>(jsonStr);
                    Console.WriteLine($"Error [{(int)response.StatusCode} {response.StatusCode}]: {pd.Detail}");
                }
            }
        }

        private static async void APIClientTest_PostAdd()
        {
            // https://localhost:44328/todo/api/itemadd
            string error = "";
            string baseAddress = "https://localhost:44328/todo";
            string path = $"{baseAddress}/api/itemadd";

            ToDoItem td = new ToDoItem()
            {
                ID = Guid.NewGuid(),
                Category = "Cats",
                CreatedOn = DateTime.Now,
                Deadline = DateTime.Now.AddDays(7),
                Description = "Description of added item",
                //Status = ToDoStatus.Unknown,
                Status = ToDoStatus.InProgress,
                UserName = Environment.UserName
            };

            HttpClient c = new HttpClient();
            c.BaseAddress = new Uri(baseAddress);

            var jsonData = JsonSerializer.Serialize<ToDoItem>(td);
            var httpData = new StringContent(jsonData, Encoding.UTF8, "application/json");

            using (var response = await c.PostAsync(path, httpData).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Status: {response.StatusCode}");
                }
                else
                {
                    string jsonStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    ProblemDetails pd = JsonSerializer.Deserialize<ProblemDetails>(jsonStr);
                    Console.WriteLine($"Error [{(int)response.StatusCode} {response.StatusCode}]: {pd.Detail}");
                }
            }

        }

        private static async void APIClientTest_ResultOk()
        {
            string baseAddress = "https://localhost:44328/todo";
            string path = $"{baseAddress}/api/gettest_allok/2";
            HttpClient c = new HttpClient() { BaseAddress = new Uri(baseAddress) };

            using (var response = await c.GetAsync(path).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Status: {response.StatusCode}");
                    string jsonStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var data = JsonSerializer.Deserialize<List<string>>(jsonStr);
                    Console.WriteLine($"json: {jsonStr}");
                }
                else
                    Console.WriteLine(response.ReasonPhrase + " [Path: " + path + "]");
            }
        }

        private static async void APIClientTest_ResultError()
        {
            string baseAddress = "https://localhost:44328/todo";
            string path = $"{baseAddress}/api/gettest_error/2";
            HttpClient c = new HttpClient() { BaseAddress = new Uri(baseAddress) };

            using (var response = await c.GetAsync(path).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Status: {response.StatusCode}");
                    string jsonStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var data = JsonSerializer.Deserialize<List<string>>(jsonStr);
                    Console.WriteLine($"json: {jsonStr}");
                }
                else
                {
                    string jsonStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    ProblemDetails pd = JsonSerializer.Deserialize<ProblemDetails>(jsonStr);
                    Console.WriteLine("Error: " + pd.Detail);
                }
            }
        }

        private static async void APIClientTest_GetToDoItem()
        {
            string baseAddress = "https://localhost:44328/todo";
            string path = $"{baseAddress}/api/gettodoitem";
            string error;
            string jsonStr;

            HttpClient c = new HttpClient() { BaseAddress = new Uri(baseAddress) };

            using (var response = await c.GetAsync(path).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    jsonStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var td = JsonSerializer.Deserialize<ToDoItem>(jsonStr);
                    Console.WriteLine("Item desc: " + td.Description);
                }
                else
                    error = response.ReasonPhrase + " [Path: " + path + "]";
            }
        }

        private static async void APIClientTest_GetByStatus()
        {
            string baseAddress = "https://localhost:44328/todo";
            string path = $"{baseAddress}/api/getbystatus/2";
            string error;
            string jsonStr;

            HttpClient c = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };

            using (var response = await c.GetAsync(path).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    jsonStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var wd = JsonSerializer.Deserialize<List<ToDoItem>>(jsonStr);
                }
                else
                    error = response.ReasonPhrase + " [Path: " + path + "]";
            }
        }

        private static void TDDataTest_GetByDescription()
        {
            ToDoRepository tdr = new ToDoRepository();
            var lst = tdr.GetByDescription("ain");
        }

        private static void TDDataTest_GetByStatus()
        {
            ToDoStatus s = new ToDoStatus();
            ToDoRepository tdr = new ToDoRepository();
            var lst = tdr.GetByStatus(s);
        }

        private static void TDDataTest_GetByDeadline()
        {
            DateTime d = new DateTime(2008, 9, 13);
            ToDoRepository tdr = new ToDoRepository();
            var lst = tdr.GetByDeadline(d);
        }

        private static void TDDatatest_GetByStatus()
        {
            ToDoStatus s = ToDoStatus.Completed;
            ToDoRepository tdr = new ToDoRepository();
            var lst = tdr.GetByStatus(s);
        }
        private static void TDDatatest_DelByStatus()
        {
            ToDoRepository tdr = new ToDoRepository();
            //var lst = tdr.GetByStatus(ToDoStatus.Completed);
            //foreach (var item in lst)
            //    tdr.Delete(item);

            tdr.DeleteByStatus(ToDoStatus.Completed);
        }

        private static void TDDataTest_Delete()
        {
            ToDoRepository tdr = new ToDoRepository();
            ToDoItem t = tdr.GetByID(new Guid("f2c47025-774d-4c95-bba3-94b882f451ae"));
            tdr.Delete(t);
        }
        private static void TDDataTest_DeleteByid()
        {
            string id = "7bf72465-204a-4dcf-9703-23cbc0501132";
            ToDoRepository tdr = new ToDoRepository();
            tdr.DeleteByID(new Guid(id));
        }

        private static void TDDataTest_Add()
        {
            ToDoRepository tdr = new ToDoRepository();

            //ToDoItem x = new ToDoItem();
            //x.Description = "lkjllkj";
            //x.Status = ToDoStatus.NotStarted;

            ToDoItem t = new ToDoItem()
            {
                Description = "lkjlkj",
                CreatedOn = DateTime.Now,
                ID = Guid.NewGuid(),
                Status = ToDoStatus.NotStarted,
                UserName = Environment.UserName
            };

            tdr.Add(t);
        }

        public static void TDDatatest_GetAll()
        {
            ToDoRepository tdr = new ToDoRepository();
            //var result = tdr.GetAll();
            List<ToDoItem> allItems = tdr.GetAll();

            foreach (var item in allItems)
            {
                Console.WriteLine(item.Description);
            }

            List<string> strList1 = allItems.Select(x => x.Description).ToList();
            List<string> strList2 = allItems.Where(x => x.Description.ToLower()
                                            .Contains("ain"))
                                            .OrderByDescending(x => x.Deadline)
                                            .ToList().Select(x => x.Description).ToList();
        }

        private static void SerialiseJSONTest2()
        {

            double x = 1.2345;
            string s1 = string.Format("The number is {0:#.00}", x);
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

        //private static async void WeatherAPIClient1()
        //{
        //    string cityName = "London";
        //    int citiTTemp = 12;

        //    // Works: api.openweathermap.org/data/2.5/weather?q=london&appid=fb9e0d8327cbfad455e3488b977c8a0f

        //    string error = "";
        //    string baseAddress = "http://api.openweathermap.org/data/2.5";
        //    string path = $"{baseAddress}/weather?q={cityName}&appid=fb9e0d8327cbfad455e3488b977c8a0f";
        //    string jsonStr = "";

        //    try
        //    {
        //        HttpClient c = new HttpClient();
        //        c.BaseAddress = new Uri(baseAddress);

        //        using (var response = await c.GetAsync(path).ConfigureAwait(false))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                //lst = await response.Content.ReadAsAsync<List<WeatherForecast>>().ConfigureAwait(false);
        //                jsonStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //                //var wd = await response.Content.ReadAsAsync<WeatherData>().ConfigureAwait(false);
        //                var wd = JsonSerializer.Deserialize<WeatherMapData2>(jsonStr);

        //            }
        //            else
        //                error = response.ReasonPhrase + " [Path: " + path + "]";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exception: {ex.Message}");
        //    }

        //    // Show results
        //    Console.WriteLine("Weather data as string:");
        //    Console.WriteLine(jsonStr);

        //    if (!string.IsNullOrEmpty(error))
        //        Console.WriteLine("Error: " + error);
        //    else
        //        Console.WriteLine("No errors");
        //}

        //private static void PersistAsLocalStrings()
        //{
        //    SaveLocalDatesAsString();
        //    RestoreLocalDatesFromString();
        //}

        //private static void SaveLocalDatesAsString()
        //{
        //    DateTime[] dates = { new DateTime(2014, 6, 14, 6, 32, 0),
        //           new DateTime(2014, 7, 10, 23, 49, 0),
        //           new DateTime(2015, 1, 10, 1, 16, 0),
        //           new DateTime(2014, 12, 20, 21, 45, 0),
        //           new DateTime(2014, 6, 2, 15, 14, 0) };
        //    string output = null;

        //    Console.WriteLine($"Current Time Zone: {TimeZoneInfo.Local.DisplayName}");
        //    Console.WriteLine($"The dates on an {Thread.CurrentThread.CurrentCulture.Name} system:");
        //    for (int ctr = 0; ctr < dates.Length; ctr++)
        //    {
        //        Console.WriteLine(dates[ctr].ToString("f"));
        //        output += dates[ctr].ToString() + (ctr != dates.Length - 1 ? "|" : "");
        //    }
        //    var sw = new StreamWriter((Stream)filenameTxt);
        //    sw.Write(output);
        //    sw.Close();
        //    Console.WriteLine("Saved dates...");
        //}

        //private static void RestoreLocalDatesFromString()
        //{
        //    TimeZoneInfo.ClearCachedData();
        //    Console.WriteLine($"Current Time Zone: {TimeZoneInfo.Local.DisplayName}");
        //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
        //    StreamReader sr = new StreamReader((string)filenameTxt);
        //    string[] inputValues = sr.ReadToEnd().Split(new char[] { '|' },
        //                                                StringSplitOptions.RemoveEmptyEntries);
        //    sr.Close();
        //    Console.WriteLine("The dates on an {0} system:",
        //                      Thread.CurrentThread.CurrentCulture.Name);
        //    foreach (var inputValue in inputValues)
        //    {
        //        DateTime dateValue;
        //        if (DateTime.TryParse(inputValue, out dateValue))
        //        {
        //            Console.WriteLine($"'{inputValue}' --> {dateValue:f}");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Cannot parse '{inputValue}'");
        //        }
        //    }
        //    Console.WriteLine("Restored dates...");
        //}

        //private static async void SimpleAPIClient()
        //{
        //    int daysAhead = 1;
        //    string cityName = "Singapore";

        //    string error = "";
        //    string baseAddress = "https://localhost:44328/weatherforecast";
        //    string path = $"{baseAddress}/api/forecastbycity/{daysAhead}/{cityName}";
        //    List<WeatherForecast> lst = new List<WeatherForecast>();

        //    try
        //    {
        //        HttpClient c = new HttpClient();
        //        c.BaseAddress = new Uri(baseAddress);

        //        using (var response = await c.GetAsync(path).ConfigureAwait(false))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                lst = await response.Content.ReadAsAsync<List<WeatherForecast>>().ConfigureAwait(false);
        //            }
        //            else
        //                error = response.ReasonPhrase + " [Path: " + path + "]";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exception: {ex.Message}");
        //    }

        //    // Show results
        //    Console.WriteLine("Forecast:");
        //    foreach (var item in lst)
        //        Console.WriteLine(item); // NOTE - this only works as it does because ToString has been implemented in WeatherForecast

        //    if (!string.IsNullOrEmpty(error))
        //        Console.WriteLine("Error: " + error);
        //    else
        //        Console.WriteLine("No errors");
        //}
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