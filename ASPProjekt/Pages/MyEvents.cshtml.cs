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
    [Authorize]
    public class MyEventsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;


        public MyEventsModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Event> Event { get;set; }
        public User user { get; set; }

        public async Task OnGetAsync()
        {
            var username = User.Identity.Name;
            user = await _context.Users.Where(u => u.UserName == username).Include(h => h.JoinedEvents).FirstOrDefaultAsync();
            Event = user.JoinedEvents;
        }
    }
}
