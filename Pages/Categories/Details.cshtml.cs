using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HE176084_MinhBT_A3.Models;

namespace HE176084_MinhBT_A3.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly HE176084_MinhBT_A3.Models.BlogContext _context;

        public DetailsModel(HE176084_MinhBT_A3.Models.BlogContext context)
        {
            _context = context;
        }

      public PostCategory PostCategory { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToPage("/AppUsers/Login");

            }
            if (id == null || _context.PostCategories == null)
            {
                return NotFound();
            }

            var postcategory = await _context.PostCategories.FirstOrDefaultAsync(m => m.CategoryID == id);
            if (postcategory == null)
            {
                return NotFound();
            }
            else 
            {
                PostCategory = postcategory;
            }
            return Page();
        }
    }
}
