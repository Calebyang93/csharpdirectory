using System;
using Utils2;

namespace Dev.ConsoleTestsCore
{
  class Program
  {
    static void Main(string[] args)
    {
      Utils2Test();
      Console.WriteLine("Press ENTER to end.");
      Console.ReadLine();
    }

    private static void Utils2Test()
    {
      Utils2.StringUtils x = new Utils2.StringUtils();
      string s1 = "The Quick brown Fox jumps over the lazy dog.";
      string[] testStrings = new string[]
      {
        "The Quick brown Fox jumps over the lazy dog.",
        "The Quick  brown Fox jumps over   the lazy dog  .",
        "",
        " ",
        "a"
      };
      foreach (var testString in testStrings)
      {
        int wordCount = x.WordCount(testString);

        int n = testString.WordCount();
        Console.WriteLine(string.Format("Wordcount of\n{0}\nis {1}", testString, wordCount));
      }
    }
  }
}
