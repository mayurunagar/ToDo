using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Data;
using ToDo.Data.Entities;
using ToDo.Models;

namespace ToDo.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ApplicationDbContext _context;
        public ToDoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddOrUpdateToDo(ToDoViewModel model, string userId)
        {
            try
            {
                var existsObject = _context.ToDoItems.Find(model.Id);

                var action = "Update";

                if (existsObject == null)
                {
                    existsObject = new ToDoItem
                    {
                        Id = Guid.NewGuid(),
                        CreatedBy = userId,
                        CreatedOn = DateTime.Now
                    };
                    _context.ToDoItems.Add(existsObject);

                    action = "Add";
                }

                var auditLog = new AuditLog
                {
                    Id = Guid.NewGuid(),
                    Action = action,
                    OldTitle = existsObject.Title,
                    NewTitle = model.Title,
                    OldDescription = existsObject.Description,
                    NewDescription = model.Description,
                    OldCompleted = existsObject.Completed,
                    NewCompleted = model.Completed,
                    AccountId = userId,
                    CreatedOn = DateTime.Now
                };

                existsObject.Title = model.Title;
                existsObject.Description = model.Description;
                existsObject.Completed = model.Completed;
                existsObject.ModifedOn = DateTime.Now;
                existsObject.ModifiedBy = userId;

                await _context.SaveChangesAsync();

                auditLog.TodoItemId = existsObject.Id;

                _context.AuditLogs.Add(auditLog);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ToDoViewModel>> GetToDos()
        {
            return await _context.ToDoItems.Select(s => new ToDoViewModel
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Completed = s.Completed
            }).OrderBy(o => o.Completed).ToListAsync();
        }
    }
}
