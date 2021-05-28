using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Model;
using TD.Data;
using TD.Model;

namespace BlazorApp1.Pages
{
    public partial class Component2: IComponent 
    {
        public string MyCaption { get; set; }
        public List<string> MyList { get; set; }

        private List<ToDoItem> myTDList;

        public List<ToDoItem> GetMyTDList()
        {
            return myTDList;
        }

        public void SetMyTDList(List<ToDoItem> value)
        {
            myTDList = value;
        }

        public List<ToDoItem> MyTDList { get; set; }

        public Component2()
        {
            MyCaption = "Hello from this view model";
            MyList = new List<String>(new string[] { "the", "category", "is", "xxx", });
            ToDoRepository tdr = new ToDoRepository();
            SetMyTDList(tdr.GetAll());
        }

        //public void Attach(RenderHandle renderHandle)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SetParametersAsync(ParameterView parameters)
        //{
        //    throw new NotImplementedException();
        //}
    }

}
