using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleToDoList.Pages
{
    public partial class ToDoList {
    }

    public partial class MainLayout : IComponent
    {
        private readonly List<string> lst = new List<string>(new string[] { "Study for Math Exam", "Write Song Lyrics", "Practice guitar" });
        private readonly List<string> task = new List<string>(new string[]{"Memorise Poem", "Cook", "Buy Grocieries"});


 //       foreach (var t in Lst)
	//{
 //           console.writeline(t);
	//}
        public void Attach(RenderHandle renderHandle)
        {
            throw new NotImplementedException();
        }

        public Task SetParametersAsync(ParameterView parameters)
        {
            throw new NotImplementedException();
        }
    }
}

