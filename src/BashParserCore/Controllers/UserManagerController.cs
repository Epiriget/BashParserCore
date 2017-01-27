using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BashParserCore.Data;
using BashParserCore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using BashParserCore.Services;

namespace BashParserCore.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CurrentUserService currUserService;
        private UserManager<ApplicationUser> userManager;
        private IEmailSender messageSender;
        public UserManagerController(ApplicationDbContext context, CurrentUserService currUserService, UserManager<ApplicationUser> userManager, IEmailSender messageSender)
        {
            _context = context;
            this.currUserService = currUserService;
            this.userManager = userManager;
            this.messageSender = messageSender;
        }


        [Authorize(Roles = "Moderator")]
        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = _context.Users.SingleOrDefault(m => m.Id == id);
            var currentId = currUserService.getCurrentUser().Id;
            ApplicationUser mainAdmin = userManager.FindByEmailAsync("mainadmin@gmail.com").Result;
            if (id != currentId && id != mainAdmin.Id)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                await messageSender.SendEmailAsync(user.Email, "Your account has been deleted", "Da");
            }
            else
            {
                return RedirectToAction("AccessError");
            }
            return RedirectToAction("Index");
        }

        public IActionResult AccessError()
        {
            return View();
        }
    }
}