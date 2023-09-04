using Airline_DE.Enums;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Airline_DE.Seeds
{
    public static class SeedRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues(typeof(RoleType)).Cast<RoleType>())
            {
                string currentRole = role.ToString();
                if (roleManager.FindByIdAsync(currentRole).Result == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(currentRole));
                }
            }
        }
    }
}
