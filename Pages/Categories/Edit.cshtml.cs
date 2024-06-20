using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HE176084_MinhBT_A3.Models;

namespace HE176084_MinhBT_A3.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly HE176084_MinhBT_A3.Models.BlogContext _context;

        public EditModel(HE176084_MinhBT_A3.Models.BlogContext context)
        {
            _context = context;
        }

        [BindProperty]
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

            var postcategory =  await _context.PostCategories.FirstOrDefaultAsync(m => m.CategoryID == id);
            if (postcategory == null)
            {
                return NotFound();
            }
            PostCategory = postcategory;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToPage("/AppUsers/Login");

            }
            _context.Attach(PostCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostCategoryExists(PostCategory.CategoryID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PostCategoryExists(int id)
        {
          return (_context.PostCategories?.Any(e => e.CategoryID == id)).GetValueOrDefault();
        }
    }
}
