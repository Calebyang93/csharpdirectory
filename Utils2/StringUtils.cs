using System;
using System.Collections.Generic;
using System.Text;

namespace Utils2
{
  public class StringUtils
  {
    public int WordCount(string s)
    {
      if (string.IsNullOrWhiteSpace(s))
        return 0;
      string[] words = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      return words.Length;
    }
  }

  public static class StringThings
  {
    public static int WordCount(this string s)
    {
      if (string.IsNullOrWhiteSpace(s))
        return 0;
      string[] words = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      return words.Length;
    }
  }
}

