using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;
using ToDo.Repository;

namespace ToDo.Service
{
    public class ToDoService
    {
        private readonly IToDoRepository _repository;
        public ToDoService(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task AddOrUpdateToDo(ToDoViewModel model, string userId)
        {
            await _repository.AddOrUpdateToDo(model, userId);
        }

        public async Task<List<ToDoViewModel>> GetToDos()
        {
            return await _repository.GetToDos();
        }
    }
}
