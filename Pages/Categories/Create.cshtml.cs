using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HE176084_MinhBT_A3.Models;

namespace HE176084_MinhBT_A3.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly HE176084_MinhBT_A3.Models.BlogContext _context;

        public CreateModel(HE176084_MinhBT_A3.Models.BlogContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToPage("/AppUsers/Login");

            }
            return Page();
        }

        [BindProperty]
        public PostCategory PostCategory { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToPage("/AppUsers/Login");

            }
            _context.PostCategories.Add(PostCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
