using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPProjekt.Data;
using ASPProjekt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPProjekt.Pages.UserPages
{
    public class UserLogoutModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public UserLogoutModel(UserManager<User> userManager, SignInManager<User> signInManager,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public LogoutForm Logout { get; set; }

        public class LogoutForm
        {
            public bool IsLoggedOut { get; set; }
        }

        public async Task<IActionResult> OnPost()
        {
            await _signInManager.SignOutAsync();

            return RedirectToPage("/Index");
        }


    }
}
