using System.Linq;
using System.Threading.Tasks;

using CarStore.Common;
using CarStore.Data.Models;
using CarStore.Data.Seeding.Contracts;

using Microsoft.AspNetCore.Identity;

namespace CarStore.Data.Seeding
{
    public class UserSeeder : ISeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Customer> _userManager;

        public UserSeeder(RoleManager<IdentityRole> roleManager, UserManager<Customer> userManager)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
        }

        public async Task SeedAsync(CarStoreDbContext dbContext)
        {
            var roleExists = await this._roleManager.RoleExistsAsync(GlobalConstants.AdminRole);

            if (!roleExists)
            {
                var role = new IdentityRole();
                role.Name = GlobalConstants.AdminRole;

                await this._roleManager.CreateAsync(role);
            }

            if (dbContext.Users.Any())
            {
                return;
            }

            var user = new Customer()
            {
                FirstName = "Simeon",
                LastName = "Gerginov",
                Age = 21,
                UserName = "simeongerginov1@gmail.com",
                Email = "simeongerginov1@gmail.com"
            };

            var password = "12345678";

            var result = await this._userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await this._userManager.AddToRoleAsync(user, GlobalConstants.AdminRole);
            }
        }
    }
}
