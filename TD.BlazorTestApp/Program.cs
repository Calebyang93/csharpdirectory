using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TD.BlazorTestApp
{
    public class Program
    {
        public static object Task { get; private set; }

        public static void Main(string[] args)
        {
        //    Task [] = {'Writing a book', 'Listening to a song', 'Sweeping the floor', 'Listening to Music' };
            try
            {
                for (int i = 0; i <= 10; i ++)
                {
                    Console.WriteLine("This is the task that was completed");

                }
            }
            catch  (NotImplementedException notImpt)
            {
                Console.WriteLine(notImpt.Message);
            }
           //  CreateHostBuilder(args).Build().Run();
        }

        private static void presentFeature()
        {
            try
            {
                presentFeature();
            }
            catch
            {
                Console.WriteLine("This is the exception error");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
