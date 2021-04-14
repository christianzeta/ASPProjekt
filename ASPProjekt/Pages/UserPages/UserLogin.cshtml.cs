using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPProjekt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASPProjekt.Data;

namespace ASPProjekt.Pages.UserPages
{
    public class UserLoginModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserLoginModel(UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginForm Login { get; set; }

        public class LoginForm
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _signInManager
                 .PasswordSignInAsync(Login.UserName, Login.Password, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
