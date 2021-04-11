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
        
        public async Task ResetAndSeedAsync(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context
            )
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            User admin = new User()
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
            };
            User Attendee = new User()
            {
                UserName = "attendee@test.com",
                Email = "attendee@test.com",
            };
            User organizer = new User()
            {
                UserName = "organizer@test.com",
                Email = "organizer@test.com",
            };

            Event[] events = new Event[]
            {
                new Event()
                {
                    Title="Pokémon hunting",
                    Description="A once in a lifetime opportunity to catch some pokermans!",
                    Place="Safari Zone",
                    Address="Team Rocket Hideout 666",
                    Date= DateTime.Now.AddDays(120),
                    SpotsAvailable= 10,
                }
            };


            await userManager.CreateAsync(admin, "Passw0rd!");
            await userManager.CreateAsync(Attendee, "Passw0rd!");

            await userManager.CreateAsync(organizer, "Passw0rd!");

            await roleManager.CreateAsync(new IdentityRole("admin"));
            await roleManager.CreateAsync(new IdentityRole("organizer"));
            await roleManager.CreateAsync(new IdentityRole("attendee"));


            await userManager.AddToRoleAsync(admin, "admin");
            await userManager.AddToRoleAsync(Attendee, "attendee");
            await userManager.AddToRoleAsync(organizer, "organizer");

            await context.AddRangeAsync(events);

            await SaveChangesAsync();
            
        }
        
    }
}
