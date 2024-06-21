using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HE176084_MinhBT_A3.Models;
using Microsoft.AspNetCore.SignalR;
using HE176084_MinhBT_A3.Hubs;

namespace HE176084_MinhBT_A3.Pages.Posts
{
    public class CreateModel : PageModel
    {
        private readonly HE176084_MinhBT_A3.Models.BlogContext _context;
        private readonly IHubContext<SignalRHub> _hubContext;
        public CreateModel(HE176084_MinhBT_A3.Models.BlogContext context, IHubContext<SignalRHub> hub)
        {
            _context = context;
            _hubContext = hub;
        }

        public IActionResult OnGet()
        {
        ViewData["AuthorID"] = new SelectList(_context.AppUsers, "UserID", "UserID");
        ViewData["CategoryID"] = new SelectList(_context.PostCategories, "CategoryID", "CategoryID");
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if(userId == null)
            {
                return RedirectToPage("/AppUsers/Login");

            }
            else
            {
                Post.AuthorID = userId.Value;
                Post.CreatedDate = DateTime.Now.Date;
                Post.UpdatedDate = DateTime.Now.Date;
                _context.Posts.Add(Post);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("LoadPosts");
                return RedirectToPage("./Index");
            }
        }
    }
}
