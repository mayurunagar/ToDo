using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Repository
{
    public interface IToDoRepository
    {
        public Task<List<ToDoViewModel>> GetToDos();
        public Task AddOrUpdateToDo(ToDoViewModel model, string userId);
    }
}
