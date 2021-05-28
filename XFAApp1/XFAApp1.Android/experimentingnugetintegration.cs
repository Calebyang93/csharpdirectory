using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft;

namespace XFAApp1.Droid
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Root
    {
        public string Name { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double Price { get; set; }
        public List<string> Sizes { get; set; }

    }
       // Console.Readline("Enter the product  name: ${Name}");
    public class experimentingnugetintegration
    {
        public experimentingnugetintegration ( string jsontext)
        {
            Console.WriteLine("The object is ....");
        }
            
    }
}