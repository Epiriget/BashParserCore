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
        private BashContext _context;

        public CommentsController(BashContext context)
        {
            _context = context;
            _context.Posts.Include(t => t.Comments).FirstOrDefault();
        }

        public IActionResult List(int PostId)
        {
            return View(_context.Comments.Where(m => m.PostId == PostId));
        }

        public IActionResult Create(int Id) //For parent's comment Id
        {
            ViewBag.commentId = Id;
            ViewBag.postId = _context.Comments.Find(Id).PostId;
            return View("~/Views/Comments/Create.cshtml");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Text, ParentID, PostId")]Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                /*       var foundedComment = _context.Comments
                           .Where(t => t.ParentID == comment.ParentID)
                           .Where(t => t.Text == comment.Text);
                       _context.Comments.Find(comment.ParentID).embeddedComments
                           .Append(foundedComment.First());
                       await _context.SaveChangesAsync();*/
            }
            return RedirectPermanent($"/Posts/Details/{comment.PostId}");
        }
    }
}