using ASPProjekt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASPProjekt.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        
        public async Task ResetAndSeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            User admin = new User()
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
            };
            User organizer = new User()
            {
                UserName = "organizer@test.com",
                Email = "organizer@test.com",
            };

            await userManager.CreateAsync(admin, "Passw0rd!");
            await userManager.CreateAsync(organizer, "Passw0rd!");

            await roleManager.CreateAsync(new IdentityRole("admin"));
            await roleManager.CreateAsync(new IdentityRole("organizer"));
            await userManager.AddToRoleAsync(admin, "admin");
            await userManager.AddToRoleAsync(organizer, "organizer");

            await SaveChangesAsync();
            
        }
        
    }
}
