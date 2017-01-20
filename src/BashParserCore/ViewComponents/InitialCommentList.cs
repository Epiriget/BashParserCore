using BashParserCore.Data;
using BashParserCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.ViewComponents
{
    public class InitialCommentList : ViewComponent
    {
        private BashContext _context;
        public InitialCommentList(BashContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            ViewBag.postId = postId;
            return View("~/Views/Shared/Components/InitialCommentList.cshtml", _context.Comments.Where(p=>p.PostId == postId));
        }


       


       

    }
}
