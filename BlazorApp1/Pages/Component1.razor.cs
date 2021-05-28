using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Model;
using TD.Data;
using TD.Model;

namespace BlazorApp1.Pages
{
    public partial class Component1 : IComponent
    {
        public string MyText { get; set; }
        public List<string> MyList { get; set; }
        public string MyCaption { get; private set; }
        public List<ToDoItem> MyTDList { get; set; }
         

    public Component1()
    {
    MyText = "Hello from the view model!";
    MyList = new List<string>(new string[] { "aaa", "bbb", "ccc", "ddd", "eee", "fff", });

            MyCaption = "Hello from this view model";
            MyList = new List<String>(new string[] { "the", "category", "is", "xxx", });
            //ToDoRepository tdr = new ToDoRepository();
            //MyTDList = tdr.getAll();
            MyTDList = new List<ToDoItem>();
        }


        #region IComponent implementation
        public ISite Site { get; set; }

        public event EventHandler Disposed;


        public void Dispose()
        {
        }
        #endregion
    }
}
