using BashParserCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.Data.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
   
        public async Task<IEnumerable<Post>> getElementList()
        {
            var Posts = await _context.Posts.Include(p => p.Author).ThenInclude(p => p.Userpic).ToListAsync();
            return Posts;
        }

        public Post getElement(int id)
        {
            var post = _context.Posts.Where(p => p.Id == id).Include(p => p.Comments).ThenInclude(p => p.Author)
                .ThenInclude(p=>p.Userpic).Include(p => p.Author).ThenInclude(p=>p.Userpic).Single();
            return post;
        }

        public void createElement(Post post)
        {
            _context.Add(post);
        }

        public async Task save()
        {
            await _context.SaveChangesAsync();
        }

        public void deleteElement(int id)
        {
            var post = getElement(id);
            _context.Posts.Remove(post);
        }

        public void updateElement(Post post)
        {
            _context.Update(post);
        }

        public bool elementExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

    }
}
