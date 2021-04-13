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
using Microsoft.AspNetCore.Authorization;

namespace ASPProjekt.Pages
{
    [Authorize(Roles="organizer")]
    public class OrganizerEventsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;


        public OrganizerEventsModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public IList<Event> Event { get;set; }
        public User user { get; set; }

        public async Task OnGetAsync()
        {
            var username = User.Identity.Name;
            user = await _userManager.FindByNameAsync(username);
            Event = await _context.Events.Where(o => o.Organizer == user).ToListAsync();
        }
    }
}
