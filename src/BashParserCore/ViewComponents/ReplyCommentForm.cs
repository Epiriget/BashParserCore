﻿using BashParserCore.Data;
using BashParserCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.ViewComponents
{
    public class ReplyCommentForm : ViewComponent
    {
        private ApplicationDbContext _context;
        public ReplyCommentForm(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(int postId, int parentId)
        {
            return View("~/Views/Shared/Components/ReplyCommentForm.cshtml", new Comment { PostId = postId, ParentID = parentId });
        }
    }
}
