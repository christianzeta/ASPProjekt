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

            User user = new User()
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
            };
            
            await userManager.CreateAsync(user, "Passw0rd!!");
            
            await roleManager.CreateAsync(new IdentityRole("admin"));
            await userManager.AddToRoleAsync(user, "admin");
            
            await SaveChangesAsync();
            
        }
        
    }
}
