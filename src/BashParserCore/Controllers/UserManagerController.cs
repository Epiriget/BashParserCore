using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BashParserCore.Data;
using BashParserCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace BashParserCore.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserManagerController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Moderator")]
        public  IActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = _context.Users.SingleOrDefault(m => m.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}