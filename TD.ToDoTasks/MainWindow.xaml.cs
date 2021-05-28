using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using TD.Data;

namespace Wpf1
{
    public class MainWindowVM : ViewModelBase
    {
        public List<ToDoItem> TDItems { get { return GetProperty(() => TDItems); } set { SetProperty(() => TDItems, value); } }
        public ToDoItem SelectedTDItem { get { return GetProperty(() => SelectedTDItem); } set { SetProperty(() => SelectedTDItem, value); } }
        public string Info { get { return GetProperty(() => Info); } set { SetProperty(() => Info, value); } }
        public MainWindowVM()
        {
            TDItems = new List<ToDoItem>();
        }
        public Boolean CanGetItems()
        {
            return true; // !string.IsNullOrEmpty(TDItems);
        }
        [Command]
        public void GetItems()
        {
            ToDoRepository tdr = new ToDoRepository();
            TDItems = tdr.GetAll();
        }
        [Command]
        public void Save()
        {
            try
            {
                Info = "Saving...";
                ToDoRepository tdr = new ToDoRepository();
                foreach (var item in TDItems)
                {
                    if (tdr.Exists(item))
                        tdr.Update(item);
                    else
                        tdr.Add(item);
                }
                Info = "Saved ok";
            }
            catch (Exception ex)
            {
                Info = "Error: " + ex.Message;
            }
        }
    }
}
