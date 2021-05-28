using System;
using System.Collections.Generic;
using System.Text;

namespace TD.Model
{
    public enum ToDoStatus
    {
        Unknown,
        NotStarted,
        InProgress,
        Completed
    }
    public class ToDoItem
    {
        public ToDoItem()
        {
            ID = Guid.NewGuid();
            CreatedOn = DateTime.Now;
            Status = ToDoStatus.NotStarted;
            UserName = Environment.UserName;
        }

        public ToDoItem(string description)
        {
            Description = description;
        }

        public Guid ID { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Category { get; set; }
        public string UserName { get; set; }
        public ToDoStatus Status { get; set; }
        public DateTime? Deadline { get; set; }
        public object Lists { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ToDoItem)
            {
                ToDoItem td = obj as ToDoItem;
                return td.Description?.ToLower() == Description?.ToLower() && td.UserName == UserName;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return $"{CreatedOn.ToString("yyyy-MMM-dd HH:mm")} {Status} {Description}";
        }
    }
}
