using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Role.Admin.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Role.Admin.ToString()));
            }

            if (!await roleManager.RoleExistsAsync(Role.User.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Role.User.ToString()));
            }
        }
    }
}
