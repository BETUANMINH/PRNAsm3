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
        private readonly BlogContext _context;

        public IndexModel(BlogContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get; set; } = new List<Post>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            // Default method in case this page is accessed without AJAX
        }

        public async Task<JsonResult> OnGetGetPosts(int pageNumber = 1, string query = "")
        {
            Console.WriteLine("Page: " + pageNumber);
            Console.WriteLine("Query: " + query);
            int PageSize = 5; // Number of posts per page
            var postsQuery = _context.Posts
                .Include(p => p.Category)
                .Where(p => string.IsNullOrEmpty(query) || p.Title.Contains(query) || p.Content.Contains(query))
                .OrderByDescending(p => p.CreatedDate);

            var totalPosts = await postsQuery.CountAsync();
            TotalPages = (int)Math.Ceiling(totalPosts / (double)PageSize);

            Post = await postsQuery
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return new JsonResult(new
            {
                items = Post.Select(p => new
                {
                    p.PostID,
                    p.CreatedDate,
                    p.UpdatedDate,
                    p.Title,
                    p.Content,
                    p.PublishStatus,
                    p.AuthorID,
                    p.CategoryID,
                    Category = p.Category != null ? new
                    {
                        p.Category.CategoryName,
                        p.Category.Description
                    } : null
                }),
                totalPages = TotalPages
            });
        }
    }
}
