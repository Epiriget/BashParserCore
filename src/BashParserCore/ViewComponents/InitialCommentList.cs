using BashParserCore.Data;
using BashParserCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.ViewComponents
{
    public class InitialCommentList : ViewComponent
    {
        private ApplicationDbContext _context;
        public InitialCommentList(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            ViewBag.postId = postId;
            var comments = await _context.Comments.Where(p => p.PostId == postId).ToListAsync();
            return View("~/Views/Shared/Components/InitialCommentList.cshtml", _context.Comments.Where(p=>p.PostId == postId));
        }

    }
}
