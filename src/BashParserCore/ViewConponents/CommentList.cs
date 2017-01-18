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

        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            ViewBag.postId = postId;
            return View("~/Views/Shared/Components/CommentList.cshtml", _context.Comments.Where(p => p.PostId == postId));
        }


       


       

    }
}
