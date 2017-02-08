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
using Microsoft.EntityFrameworkCore;

namespace BashParserCore.Controllers
{
    public class UserManagerController : Controller
    {
        private ApplicationDbContext _context;
        private ICurrentUserService currUserService;
        private UserManager<ApplicationUser> userManager;
        private IEmailSender messageSender;
        public UserManagerController(ApplicationDbContext context, ICurrentUserService currUserService, UserManager<ApplicationUser> userManager, IEmailSender messageSender)
        {
            _context = context;
            this.currUserService = currUserService;
            this.userManager = userManager;
            this.messageSender = messageSender;
        }


        [Authorize(Roles = "Moderator")]
        public IActionResult Index()
        {
            var Users = _context.Users.Include(p=>p.Userpic);
            return View(Users);
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
                await messageSender.SendEmailAsync(user.Email, "Your account has been deleted", $"Уважаемый {user.UserName}, Ваш профиль на ресурсе BashParserCore был удален, поскольку Вы - мудак.");
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