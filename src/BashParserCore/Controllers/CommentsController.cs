using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BashParserCore.Data;
using BashParserCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BashParserCore.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
            _context.Posts.Include(t => t.Comments).FirstOrDefault();
        }

        public IActionResult List(int PostId)
        {
            return View(_context.Comments.Where(m => m.PostId == PostId));
        }

        public async Task<ActionResult> Create([Bind("ParentID,PostId,Text")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }
            return RedirectPermanent($"/Posts/Details/{comment.PostId}");
        }
    }
}