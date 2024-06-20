using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using HE176084_MinhBT_A3.Models;
using Microsoft.EntityFrameworkCore;

namespace HE176084_MinhBT_A3.Pages.AppUser
{
    public class RegisterModel : PageModel
    {
        private readonly BlogContext _context;

        public RegisterModel(BlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegisterUserDto RegisterUser { get; set; }

        public void OnGet()
        {
            var session = HttpContext.Session.GetString("Email");
            if (session != null)
            {
                RedirectToPage("/Posts/Index");
            }
            else
            {
                RedirectToPage("Register");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            var user = new Models.AppUser
            {
                Fullname = RegisterUser.Fullname,
                Address = RegisterUser.Address,
                Email = RegisterUser.Email,
                Password = RegisterUser.Password,
            };

            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetInt32("UserID", user.UserID);
            return RedirectToPage("/Posts/Index");
        }
    }
}