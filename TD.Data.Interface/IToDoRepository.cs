using Model;
using System;
using System.Collections.Generic;
using System.Text;
using TD.Model;

namespace TD.Data.Interface
{
    public interface IToDoRepository
    {
        List<ToDoItem> GetAll();
        void Add(ToDoItem t);
        void AddByCategory(ToDoItem t);
        void AddById(ToDoItem t);
        void Delete(ToDoItem t);
        void DeleteById(Guid id);
        void DeleteByUser(ToDoItem t);
        void Update(ToDoItem t);
        void UpdateByUser(ToDoItem t);
        void UpdateByCategory(ToDoItem t);
    }
}
