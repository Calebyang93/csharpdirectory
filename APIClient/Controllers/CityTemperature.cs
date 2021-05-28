using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIHost.Controllers
{
    public class CityTemperature
    {
        internal static object Now;

        public static object DateTime { get; internal set; }
        public int citId { get; set; }
        public string cityName { get; set; } 
    }
}
