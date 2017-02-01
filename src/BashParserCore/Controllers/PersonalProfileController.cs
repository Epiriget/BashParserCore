using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BashParserCore.Data;
using BashParserCore.Models;
using Microsoft.AspNetCore.Http;
using BashParserCore.Services;
using System.IO;

namespace BashParserCore.Controllers
{
    public class PersonalProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ICurrentUserService currUserService;

        public PersonalProfileController(ApplicationDbContext context, ICurrentUserService currUserService)
        {
            _context = context;
            this.currUserService = currUserService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormFile pic)
        {
            byte[] imageData = null;
            var currId = currUserService.getCurrentUser().Id;
            if (pic != null)
            {
                using (var binaryReader = new BinaryReader(pic.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)pic.Length);
                }
            }
            Userpic userpic = new Userpic { ApplicationUserId = currId, Picture = imageData };
            _context.Userpics.Add(userpic);
            _context.SaveChanges();
            return RedirectToAction("Create");
        }
    }
}
