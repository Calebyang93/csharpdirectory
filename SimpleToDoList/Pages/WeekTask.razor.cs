using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace SimpleToDoList.Pages
{
    public partial class WeekTask
    {
    }

    public partial class MainLayout : IComponent
    {
        private readonly List<int> tid = new List<int>(new int[] {1234567});
        private readonly List<string> tworkDay = new List<string>(new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" });
        private readonly List<string> tname = new List<string>(new string[] { "Household Chores", "Professional or Career Related", "Hobbies", "Content Generation" });
        private readonly List<string> tcategory = new List<string>(new string[] { "Art content generation", "Travel", "Languages", "Writing Lyrics", "Melody Writing", "Sports" });
        private readonly List<int> tnoofpeoplewkg = new List<int>(new int[] { 3 });
        private readonly List<string> ttaskstatus = new List<string>(new string[] { "To be Completed", "In Progress", "Completed" });

    }

    //public void Attach(RenderHandle renderHandle)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task SetParametersAsync(ParameterView parameter)
    //{
    //    throw new NotImplementedException();
    //}

}
