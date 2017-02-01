using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BashParserCore.Data;
using BashParserCore.Models;
using Microsoft.AspNetCore.Authorization;
using BashParserCore.Data.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using BashParserCore.Services;

namespace BashParserCore.Controllers
{
    public class PostsController : Controller
    {
        private IRepository<Post> postRepository;
        private ICurrentUserService currUserService;
        public PostsController(ICurrentUserService currUserService, IRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
            this.currUserService = currUserService;
        }

        // GET: Posts
        [AllowAnonymous]
        public IActionResult Index()
        {
            //var user = currUserService.getCurrentUser();
            return View(postRepository.getElementList().Result);
        }

        // GET: Posts/Details/5
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = postRepository.getElement(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,PostName,Rating,Text")] Post post)
        {
            post.Author = currUserService.getCurrentUser();
            if (ModelState.IsValid)
            {
                postRepository.createElement(post);
                await postRepository.save();
            }
            return RedirectToAction("Index");
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Moderator")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = postRepository.getElement(id.Value);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,PostName,Rating,Text")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    postRepository.updateElement(post);
                    await postRepository.save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!postRepository.elementExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Moderator")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = postRepository.getElement(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Moderator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            postRepository.deleteElement(id);
            await postRepository.save();
            return RedirectToAction("Index");
        }
        public IActionResult GetUserpic(int Id)
        {
            Post post = postRepository.getElement(Id);
            return File(post.Author.Userpic.Picture, "image/jpg");
        }

    }
}
