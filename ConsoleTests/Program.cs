using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace ConsoleTests
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Phonetic
    {
        public string text { get; set; }
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
        public List<Phonetic> phonetics { get; set; }
        public string phonetic { get; set; }
        public string word { get; set; }
        public List<Meaning> meanings { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //APIGermanTranslation1();
            APILookUpWord("en", "refute");
            APILookUpWord("en", "set");
            Console.WriteLine("Press Enter to Close!");
            Console.ReadLine();
        }

        private static async void APILookUpWord(string Countrycode, string w)
        {
            string baseUrl = $@"https://api.dictionaryapi.dev/api/v2/entries/{Countrycode}/";
            HttpClient client = new HttpClient { BaseAddress = new Uri(baseUrl) };
            var response = await client.GetAsync($"{w}");
            var stream = await response.Content.ReadAsStreamAsync();
            StreamReader reader = new StreamReader(stream);
            string jsonText = reader.ReadToEnd();
            //      namespace DirectoryAPI response {
            //class consoleTest
            {
                // deserializing json to c# logic objects  
                DictionaryAPIResponse[] x = JsonSerializer.Deserialize<DictionaryAPIResponse[]>(jsonText);
                //var result = x[0].meanings[0].definitions[0].definition; // take first item in each array/list  
            }

        }
        private static async void APIGermanTranslation1()
            {
                string baseUrl = @"https://api.dictionaryapi.dev/api/v2/entries/de/";
                HttpClient client = new HttpClient { BaseAddress = new Uri(baseUrl) };
                var response = await client.GetAsync("Krankenhaus");
                var stream = await response.Content.ReadAsStreamAsync();
                StreamReader reader = new StreamReader(stream);
                string jsonText = reader.ReadToEnd();
          //      namespace DirectoryAPI response {
        //class consoleTest
            {
        // deserializing json to c# logic objects  
        DictionaryAPIResponse[] x = JsonSerializer.Deserialize<DictionaryAPIResponse[]>(jsonText);
        //var result = x[0].meanings[0].definitions[0].definition; // take first item in each array/list  
             }
                
            }
        }
    }
