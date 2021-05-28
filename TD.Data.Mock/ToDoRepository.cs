using System;
using System.Collections.Generic;
using System.Text;
using TD.Data.Interface;
using TD.Model;
using System.Linq;
using Org.Apache.Http.Client.Protocol;

namespace TD.Data.Mock
{
    public class ToDoRepositoryMock : IToDoRepository
    {
        private static object siteUrl;
        List<ToDoItem> lst;
        ListCreationInformation listCreationInfo = new ListCreationInformation();

        public ToDoRepositoryMock()
        {
            lst = new List<ToDoItem>();
        }

        public object ListCreationInfo { get; private set; }
        public ClientContext ClientContext { get; set; }
        public object tList { get; private set; }

        public void Add(ToDoItem t)
        {
            lst.Add(t); 
        }

        public void AddByCategory(ToDoItem t)
        {
            throw new NotImplementedException();
        }

        public void AddById(ToDoItem t)
        {
            throw new NotImplementedException();
        }

        public void Delete(ToDoItem t)
        {
            DeleteById(t.ID);
        }

        public void DeleteById(Guid id)
        {
            ToDoItem tdi = lst.Where(x => x.ID == id).FirstOrDefault();
            if (tdi != null)
            {
                lst.Remove(tdi);
            }
        }

        public void DeleteByUser(ToDoItem t)
        {
            throw new NotImplementedException();
        }

        public List<ToDoItem> GetAll()
        {
            
            lst.Add(new ToDoItem()
            {
                CreatedOn = DateTime.Now,
                Description = "Check Grocery List",
                Deadline = new DateTime(2021, 4, 10, 15, 0, 0)
            });
            lst.Add(new ToDoItem()
            {
                CreatedOn = DateTime.Now,
                Description = "Add To do list",
                Deadline = new DateTime(2021, 5, 10, 15, 0, 0)
            });
            lst.Add(new ToDoItem()
            {
                CreatedOn = DateTime.Now,
                Description = "Work on Exercising",
                Deadline = new DateTime(2021, 5, 20, 18, 0,0)
            });
            lst.Add(new ToDoItem()
                {
                    CreatedOn = DateTime.Now, 
                    Description = "Writing Reflections",
                    Deadline = new DateTime(2021, 5, 21, 21, 0, 0)
            });
            lst.Add(new ToDoItem()
            {
                CreatedOn = DateTime.Now,
                Description = "Clearing Room",
                Deadline = new DateTime(2021, 5, 27, 13, 0, 0)

            });
            return lst;
        }

        public void Update(ToDoItem t, ToDoRepositoryMock description, ToDoRepositoryMock category)
        {
            //List<ToDoItem> tList = t.Lists.Add(ListCreationInfo);
            //tList.description = "ToDo List Requirements";
            //object p = tList.Update();
            //object t1 = Org.Apache.Http.Client.Protocol.ClientContext.ExecuteQuery();
        }

        public void Update(ToDoItem t)
        {
            throw new NotImplementedException();
        }

        public void UpdateByCategory(ToDoItem t)
        {
            throw new NotImplementedException();
        }

        public void UpdateByUser(ToDoItem t)
        {
            throw new NotImplementedException();
        }
    }
}

namespace TD.Data.Mock
{
    class web
    {
    }
}

namespace TD.Data.Mock
{
    class ListCreationInformation
    {
    }
}