using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BashParserCore.Data;
using BashParserCore.Models;
using Microsoft.EntityFrameworkCore;

namespace BashParserCore.Controllers
{
    public class CommentsController : Controller
    {
        private BashContext _context;

        public CommentsController(BashContext context)
        {
            _context = context;
        }

        public IActionResult Index(int postID)
        {
            return View(new Comment(null, _context.Comments.Where(m => m.PostID == postID)));
        }
    }
}