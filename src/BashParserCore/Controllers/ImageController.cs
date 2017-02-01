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
    public class ImageController : Controller
    {
        private ApplicationDbContext context;

        public ImageController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult GetUserpic(string Id)
        {
            var author = context.ApplicationUser.Where(p => p.Id == Id).Include(p => p.Userpic).Single();
            if (author.Userpic != null)
            {
                return File(author.Userpic.Picture, "image/jpg");
            }
            else
            {
                return File("~/images/DefaultUser.jpg", "image/jpg");
            }
        }
    }
}