using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using NoteData;

namespace Dev.SQLite
{


  class Program
  {
    static void Main(string[] args)
    {

      BasicOperations();

      Console.WriteLine("Press ENTER to end...");
      Console.ReadLine();
    }

    private static void BasicOperations()
    {
      try
      {

        NoteRepository noteRepository = new NoteRepository();
        noteRepository.DeleteAll();
        noteRepository.Add("The Journey of the Sun", "Explore the sun Together");
        noteRepository.Update(12, "new title", "edited text");
        List<Note> lst = noteRepository.GetAll();

        Console.WriteLine("List:");
        foreach (Note note in lst)
        {
          Console.WriteLine(string.Format("{0:dd MMM yy}: {1}", note.NoteDate, note.NoteText));
        }
      }
      catch (Exception ex)
      {
      }
    }
  }
}
