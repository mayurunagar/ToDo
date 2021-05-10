using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Data.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string AccountId { get; set; }
        public string Action { get; set; }//Add, Update, Delete, Complete
        public Guid TodoItemId { get; set; }
        public ToDoItem TodoItem { get; set; }
        public string OldTitle { get; set; }
        public string NewTitle { get; set; }
        public string OldDescription { get; set; }
        public string NewDescription { get; set; }
        public bool OldCompleted { get; set; }
        public bool NewCompleted { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
