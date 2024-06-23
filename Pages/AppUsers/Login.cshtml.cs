using HE176084_MinhBT_A3.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;

namespace HE176084_MinhBT_A3.Pages.AppUser
{
    public class LoginModel : PageModel
    {
        private readonly BlogContext _context;

        public LoginModel(BlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoginUserDto LoginUser { get; set; }

        public void OnGet()
        {
            var session = HttpContext.Session.GetString("Email");
            if (session != null)
            {
                //move to action Index of controller Posts
                RedirectToPage("/Posts/Index");
            }
            else
            {
                RedirectToPage("Login");
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _context.AppUsers.Where(x => x.Email == LoginUser.Email && x.Password == LoginUser.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                ModelState.AddModelError("LoginUser.Email", "Email or password is incorrect");
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetInt32("UserID", user.UserID);
                return RedirectToPage("/Posts/Index");
            }
        }
    }
}
