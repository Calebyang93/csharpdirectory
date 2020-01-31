using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils2;

namespace Dev.ConsoleTests
{
  class Program
  {
    static void Main(string[] args)
    {
      Utils2Test();
    }

    private static void Utils2Test()
    {
      
    }

    private static void SimpleTest1()
    {
      // A trivial class, with one property
      MyClass x = new MyClass();
      x.MyProperty = "Hello from MyClass";
      Console.WriteLine(x.MyProperty);

      string word;

      // A more useful class, with properties and methods, public and private
      Translator t1 = new Translator("DE");
      word = "Thanks";
      Console.WriteLine(string.Format("{0} in German is: {1}", word, t1.Translate(word)));
      word = "Hello";
      Console.WriteLine(string.Format("{0} in German is: {1}", word, t1.Translate(word)));

      Translator t2 = new Translator("CN");
      Console.WriteLine(string.Format("{0} in Chinese is: {1}", word, t2.Translate(word)));

      Console.WriteLine("Press ENTER to end.");
      Console.ReadLine();
      /*
       * Output in console window:
       Hello from MyClass
       Thanks in German is: danke
       Hello in German is: Oops - 'Hello' is not in the dictionary!
       Hello in Chinese is: I don't know that language
       Press ENTER to end.
       */
    }
  }


  class MyClass
  {
    // This class is ok here, but can not be easily accessed by any other project.
    // It is therefore much better to put the class in another and new project.
    // Create a new project in the Solution, of type Class Library.
    //  - poss best if I talk you through this. It is very straightforward.
    public string MyProperty { get; set; }
  }

  class Translator
  {
    private Dictionary<string, string> dictionary;

    public Translator(string languageCode)
    {
      dictionary = GetDictionary(languageCode);
    }

    public string Translate(string word)
    {
      if (dictionary.Count == 0)
      {
        return "I don't know that language";
      }

      string wordToLookup = word.ToLower();
      if (dictionary.ContainsKey(wordToLookup))
        return dictionary[wordToLookup];
      else
        return string.Format("Oops - '{0}' is not in the dictionary!", word);
    }

    private Dictionary<string, string> GetDictionary(string language)
    {
      // You could easily imagine getting this data from elsewhere, rather than from the fixed strings declared here
      Dictionary<string, string>  d = new Dictionary<string, string>();
      switch (language)
      {
        case "DE":
          d.Add("thanks", "danke");
          break;
      }
      return d;
    }
  }
}
