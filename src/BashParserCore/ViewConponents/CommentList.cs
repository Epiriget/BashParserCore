using BashParserCore.Data;
using BashParserCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.ViewConponents
{
    public class CommentList : ViewComponent
    {
        private BashContext _context;
        public CommentList(BashContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> List(int postId)
        {
            return View(_context.Comments.Where(p => p.PostId == postId));
        }


        [HttpGet]
        public async Task<IViewComponentResult> Create(int postId, int parentId)
        {
            return View(new Comment { PostId = postId, ParentID = parentId });
        }


        [HttpPost]
        public async Task Create([Bind("Text, ParentID, PostId")]Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }
        }

    }
}
