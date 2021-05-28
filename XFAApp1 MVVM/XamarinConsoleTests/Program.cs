using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace XamarinConsoleTests
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Phonetic
    {
    }

    public class Definition
    {
        public List<object> antonyms { get; set; }
        public string definition { get; set; }
        public List<object> synonyms { get; set; }
        public string example { get; set; }
    }

    public class Meaning
    {
        public string partOfSpeech { get; set; }
        public List<Definition> definitions { get; set; }
    }

    public class DictionaryAPIResponse
    {
        public string word { get; set; }
        public List<Phonetic> phonetics { get; set; }
        public List<Meaning> meanings { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //SerializationTest();
            DeserializationTest1();
            //DeserializationTest2():
            //APIBritishCall1(string en_GB, string britword);
            //APIGermanCall1(string de, string ruword);
            //APIRussianCall2();

            Console.WriteLine("The test has completed. Press any key to return.");
            Console.ReadLine();


        }

        private static void DeserializationTest1()
        {
            throw new NotImplementedException();
        }

        private static async void APIRussianCall2()
        {
            string baseUrl = @"https://api.dictionaryapi.dev/api/v2/entries/ru/";

            HttpClient client = new HttpClient { BaseAddress = new Uri(baseUrl) };

            var response = await client.GetAsync("праздник");

            var stream = await response.Content.ReadAsStreamAsync();

            StreamReader reader = new StreamReader(stream);

            string jsonText = reader.ReadToEnd();


            // deserializing json to c# logic objects 
            DictionaryAPIResponse[] x = JsonSerializer.Deserialize<DictionaryAPIResponse[]>(jsonText); 

            var result = x[0].meanings[0].definitions[0].definition; // take first item in each array/list 
        }

        private static async void APIGermanCall1(string de, string deword)
        {
            string baseUrl = @"https://api.dictionaryapi.dev/api/v2/entries/de/";

            HttpClient client = new HttpClient { BaseAddress = new Uri(baseUrl) };

            var response = await client.GetAsync("Krankenhaus");

            var stream = await response.Content.ReadAsStreamAsync();

            StreamReader reader = new StreamReader(stream);

            string jsonText = reader.ReadToEnd();


            // deserializing json to c# logic objects 
            //DictionaryAPIResponse[] x = JsonSerializer.Deserialize<DictionaryAPIResponse[]>(jsonText); 

            //var result = x[0].meanings[0].definitions[0].definition; // take first item in each array/list 
        }

        private static async void APIBritishCall1(string en_GB, string britword)

        {

            string baseUrl = @"https://api.dictionaryapi.dev/api/v2/entries/en_GB/";

            HttpClient client = new HttpClient { BaseAddress = new Uri(baseUrl) };

            var response = await client.GetAsync("Challenging");

            var stream = await response.Content.ReadAsStreamAsync();

            StreamReader reader = new StreamReader(stream);

            string jsonText = reader.ReadToEnd();


            // deserializing json to c# logic objects 
            //DictionaryAPIResponse[] x = JsonSerializer.Deserialize<DictionaryAPIResponse[]>(jsonText); 

            //var result = x[0].meanings[0].definitions[0].definition; // take first item in each array/list 

        }
    }
}
