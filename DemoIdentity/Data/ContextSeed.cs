using FPTBookDemo.Models;
using Microsoft.AspNetCore.Identity;

namespace FPTBookDemo.Data
{
	public static class ContextSeed
	{
		public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			//Seed Roles
			await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.User.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Owner.ToString()));
        }
		public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			//Seed Default User
			var defaultUser = new ApplicationUser
			{
				UserName = "Admin123@gmail.com",
				Email = "Admin123@gmail.com",
				FirstName = "Admin",
				LastName = " ",
				EmailConfirmed = true,
				PhoneNumberConfirmed = true
			};
			if (userManager.Users.All(u => u.Id != defaultUser.Id))
			{
				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultUser, "Admin@123");;
					await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                    await roleManager.CreateAsync(new IdentityRole(Enums.Roles.User.ToString()));
                    await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Owner.ToString()));
                }

			}
		}
	}
}