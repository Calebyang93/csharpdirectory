using System;
using System.Collections.Generic;
using System.Text;

namespace Model2
{
  public class Person
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTime dob { get; set; }

    public override string ToString()
    {
      return $"{FirstName} {LastName}";
    }
  }
}
// Add another API 
