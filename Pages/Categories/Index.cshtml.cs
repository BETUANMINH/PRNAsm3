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
    public class IndexModel : PageModel
    {
        private readonly HE176084_MinhBT_A3.Models.BlogContext _context;

        public IndexModel(HE176084_MinhBT_A3.Models.BlogContext context)
        {
            _context = context;
        }

        public IList<PostCategory> PostCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                RedirectToPage("/AppUsers/Login");

            }
            if (_context.PostCategories != null)
            {
                PostCategory = await _context.PostCategories.ToListAsync();
            }
        }
    }
}
