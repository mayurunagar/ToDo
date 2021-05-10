using Microsoft.AspNetCore.Identity;
using ToDo.Models;

namespace ToDo
{
    public static class MyIdentityDataInitializer
    {
        private static string Admin = "Admin";
        private static string Email = "admin@mishatechnologies.com";

        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(Admin).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = Admin;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync(Email).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = user.Email = Email;
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync(user, "Bravo@2020").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Admin).Wait();
                }
            }
        }
    }
}
