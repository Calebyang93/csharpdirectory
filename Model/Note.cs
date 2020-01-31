using System;

namespace Model
{
  public class Note
  {
    public int ID { get; set; }
    public DateTime NoteDate { get; set; }
    public string NoteText { get; set; }
    public string Title { get; set; }

    public override string ToString()
    {
      return string.Format("{0}: {1} {2}", ID, Title.ToUpper(), NoteText);
    }
  }
 
}
