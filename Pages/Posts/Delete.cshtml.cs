﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HE176084_MinhBT_A3.Models;
using Microsoft.AspNetCore.SignalR;
using HE176084_MinhBT_A3.Hubs;

namespace HE176084_MinhBT_A3.Pages.Posts
{
    public class DeleteModel : PageModel
    {
        private readonly HE176084_MinhBT_A3.Models.BlogContext _context;
        private readonly IHubContext<SignalRHub> _hubContext;
        public DeleteModel(HE176084_MinhBT_A3.Models.BlogContext context, IHubContext<SignalRHub> hub)
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

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.PostID == id);

            if (post == null)
            {
                return NotFound();
            }
            else 
            {
                Post = post;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(id);

            if (post != null)
            {
                Post = post;
                _context.Posts.Remove(Post);
                await _context.SaveChangesAsync();
            }
            await _hubContext.Clients.All.SendAsync("LoadPosts");
            return RedirectToPage("./Index");
        }
    }
}
