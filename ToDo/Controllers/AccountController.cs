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
using ViewModel;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController
        (
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok(new { response = true, userName = model.UserName, userId = user.Id });
                }
            }
            return Ok(new { response = false, message = "Unauthorized" });
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public async Task<bool> Register(UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    return result.Succeeded;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpGet]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
