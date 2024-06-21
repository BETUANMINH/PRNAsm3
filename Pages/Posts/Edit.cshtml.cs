using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HE176084_MinhBT_A3.Models;
using HE176084_MinhBT_A3.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HE176084_MinhBT_A3.Pages.Posts
{
    public class EditModel : PageModel
    {
        private readonly HE176084_MinhBT_A3.Models.BlogContext _context;
        private readonly IHubContext<SignalRHub> _hubContext;

        public EditModel(HE176084_MinhBT_A3.Models.BlogContext context, IHubContext<SignalRHub> hub)
        {
            _context = context;
            _hubContext = hub;
        }

        [BindProperty]
        public Post Post { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToPage("/AppUsers/Login");

            }
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post =  await _context.Posts.FirstOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }
            Post = post;
            ViewData["AuthorID"] = new SelectList(_context.AppUsers, "UserID", "UserID");
            ViewData["CategoryID"] = new SelectList(_context.PostCategories, "CategoryID", "CategoryID");
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

            _context.Attach(Post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(Post.PostID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            await _hubContext.Clients.All.SendAsync("LoadPosts");
            return RedirectToPage("./Index");
        }

        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.PostID == id)).GetValueOrDefault();
        }
    }
}
