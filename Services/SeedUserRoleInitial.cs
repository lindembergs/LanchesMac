using LanchesMac.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LanchesMac.Services;

public class SeedUserRoleInitial(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager) : ISeedUserRoleInitial
{
    public void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("Member").Result)
        {
            IdentityRole role = new()
            {
                Name = "Member",
                NormalizedName = "MEMBER"
            };
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }
    }

    public void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
        {
            IdentityUser user = new()
            {
                UserName = "usuario@localhost",
                Email = "usuario@localhost",
                NormalizedUserName = "usuario@localhost".ToUpper(),
                NormalizedEmail = "usuario@localhost".ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, "Member").Wait();
        }

        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            IdentityUser user = new()
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                NormalizedUserName = "admin@localhost".ToUpper(),
                NormalizedEmail = "admin@localhost".ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, "Admin").Wait();
        }
    }
}
