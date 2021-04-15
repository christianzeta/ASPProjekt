using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPProjekt.Models;
using ASPProjekt.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASPProjekt.Pages.UserPages
{
    public class UserRegisterModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public UserRegisterModel(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public NewUserForm NewUser { get; set; }

        public class NewUserForm
        {
            public string userName { get; set; }
            public string password { get; set; }
        }

        public async Task<IActionResult> OnPost()
        {
            
            User newUser = new User()
            {
                UserName = NewUser.userName,
                    
            };

            var result = await _userManager.CreateAsync(newUser, NewUser.password);
                

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }

            return Page();
            
        }
    } 
}
