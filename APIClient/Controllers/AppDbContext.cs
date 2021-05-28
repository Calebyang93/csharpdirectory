using System.Collections.Generic;

namespace APIHost.Controllers
{
    internal class AppDbContext
    {
        public IEnumerable<ToDoController> toDo { get; internal set; }
        public object Category { get; internal set; }
        public object Status { get; internal set; }
    }
}