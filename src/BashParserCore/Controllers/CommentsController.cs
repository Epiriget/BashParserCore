using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BashParserCore.Data;
using BashParserCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BashParserCore.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext _context;
        private IHttpContextAccessor httpContextAccessor;
        private UserManager<ApplicationUser> userManager;
        public CommentsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)

        {
            _context = context;
       //     _context.Posts.Include(t => t.Comments).FirstOrDefault();
       //     _context.Comments.Include(t => t.Author).FirstOrDefault();
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<ActionResult> Create([Bind("ParentID,PostId,Text")] Comment comment)
        {
            comment.Author = userManager.FindByIdAsync(httpContextAccessor.HttpContext
            .User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }
            return RedirectPermanent($"/Posts/Details/{comment.PostId}");
        }
    }
}