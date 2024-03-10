using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Thrift_Us.Data;

namespace Thrift_Us.Models
{
    public static class IdentitySeedData
    {

        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            ThriftDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<ThriftDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            RoleManager<IdentityRole> roleManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            /*string roleName = "Admin";

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }*/
            IdentityUser user = await userManager.FindByNameAsync(adminUser);

            if (user == null)
            {
                user = new IdentityUser("admin0@gmail.com");
                user.EmailConfirmed = true;
                user.Email = "admin0@gmail.com";
                user.PhoneNumber = "9847298292";

                await userManager.CreateAsync(user, adminPassword);

               
            }
        }
    }
}
