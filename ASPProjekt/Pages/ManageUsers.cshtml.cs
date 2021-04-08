using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPProjekt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASPProjekt.Pages
{
    [Authorize(Roles = "admin")]
    public class ManageUsersModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        public List<User> Users { get; set; }
        public bool IsOrganizer { get; set; } 

        public ManageUsersModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync();
        }

        public async Task IsOrganizerAsync(User user)
        {
            IsOrganizer = await _userManager.IsInRoleAsync(user, "organizer");
        }
    }
}
