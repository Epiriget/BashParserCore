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
using BashParserCore.Services;

namespace BashParserCore.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext _context;
        private ICurrentUserService currentUserService;
        public CommentsController(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            this.currentUserService = currentUserService;
        }

        [Authorize]
        public async Task<ActionResult> Create([Bind("ParentID,PostId,Text")] Comment comment)
        {
            comment.Author = currentUserService.getCurrentUser();
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }
            return RedirectPermanent($"/Posts/Details/{comment.PostId}");
        }
    }
}