using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HE176084_MinhBT_A3.Models;

namespace HE176084_MinhBT_A3.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly HE176084_MinhBT_A3.Models.BlogContext _context;

        public IndexModel(HE176084_MinhBT_A3.Models.BlogContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; } = default!;

        public async Task OnGetAsync(string searchInput = "")
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                RedirectToPage("/AppUsers/Login");

            }
            if (!string.IsNullOrEmpty(searchInput))
            {
                Post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Where(p => p.Title.Contains(searchInput) || p.Content.Contains(searchInput) || p.PostID.ToString().Contains(searchInput))
                .ToListAsync();
            }
            else
            {
                Post = await _context.Posts.ToArrayAsync();
            }
        }
    }
}
