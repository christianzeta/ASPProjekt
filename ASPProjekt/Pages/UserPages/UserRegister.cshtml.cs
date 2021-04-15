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
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _rolemanager;

        public UserRegisterModel(UserManager<User> userManager,
            ApplicationDbContext context, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _rolemanager = roleManager;
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

            await _context.AddAsync(newUser);
            await _userManager.AddToRoleAsync(newUser, "attendee");
            await _context.SaveChangesAsync();
            


            if (result.Succeeded)
            {
                return RedirectToPage("UserLogin");
            }

            return Page();
            
        }
    } 
}
