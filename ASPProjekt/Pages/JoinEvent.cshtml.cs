using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASPProjekt.Data;
using ASPProjekt.Models;
using Microsoft.AspNetCore.Identity;

namespace ASPProjekt.Pages
{
    public class JoinEventModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public JoinEventModel(ApplicationDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Event Event { get; set; }
        public bool Submitted { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Vem är det som ska gå med?
            var username = User.Identity.Name;
            var user = await _context.Users
                .Where(u => u.UserName == username)
                .Include(h => h.JoinedEvents)
                .FirstOrDefaultAsync();


            if (!user.JoinedEvents.Contains(Event))
            {
                // Om vi har en användare och ett event så är det
                // enkelt att lägga till eventet till användarens
                // lista på planerade events.
                Event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);
                user.JoinedEvents.Add(Event);
                await _context.SaveChangesAsync();
            }

            Submitted = true;
            return Page();
        }
    }
}
