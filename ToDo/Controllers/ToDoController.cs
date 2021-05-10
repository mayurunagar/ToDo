using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;
using ToDo.Service;
using System.Security.Claims;

namespace ToDo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly ToDoService _toDoService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ToDoController(ILogger<ToDoController> logger, ToDoService todoService, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _toDoService = todoService;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public async Task<List<ToDoViewModel>> Get()
        {
            return await _toDoService.GetToDos();
        }

        [HttpPost("[action]")]
        public async Task AddOrUpdate([FromBody] ToDoViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _toDoService.AddOrUpdateToDo(model, userId);
        }
    }
}
