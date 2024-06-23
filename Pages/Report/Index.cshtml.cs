using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HE176084_MinhBT_A3.Models;

namespace HE176084_MinhBT_A3.Pages.Report
{
    public class IndexModel : PageModel
    {
        private readonly BlogContext _context;

        public IndexModel(BlogContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public IList<Post> Post { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Posts != null)
            {
                var query = _context.Posts
                    .Include(p => p.Author)
                    .Include(p => p.Category)
                    .AsQueryable();

                if (StartDate.HasValue)
                {
                    query = query.Where(x => x.CreatedDate.Date >= StartDate.Value.Date);
                }

                if (EndDate.HasValue)
                {
                    query = query.Where(x => x.CreatedDate.Date <= EndDate.Value.Date);
                }

                Post = await query
                    .OrderByDescending(x => x.CreatedDate)
                    .ToListAsync();
            }
        }
    }
}
