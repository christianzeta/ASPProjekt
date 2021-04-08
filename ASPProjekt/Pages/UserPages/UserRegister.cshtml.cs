using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPProjekt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPProjekt.Pages.UserPages
{
    public class UserRegisterModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public UserRegisterModel(UserManager<User> userManager)
        {
            _userManager = userManager;
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
