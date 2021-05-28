using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Model
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
        }

        public ToDoItem(string description)
        {
            Description = description;
        }

        [JsonPropertyName("id")]
        public Guid ID { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("createdon")]
        public DateTime CreatedOn { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("status")]
        public ToDoStatus Status { get; set; }
        [JsonPropertyName("deadline")]
        public DateTime? Deadline { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ToDoItem)
            {
                ToDoItem td = obj as ToDoItem;
                return td.Description.ToLower() == Description.ToLower() && td.UserName == UserName;
            }
            else
                return false;
        }
    }
}