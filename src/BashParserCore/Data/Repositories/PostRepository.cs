using BashParserCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.Data.Repositories
{
    public class PostRepository : IReposotory<Post>
    {
        private BashContext _context;
        public PostRepository(BashContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> getElementList()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> getElement(int id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(m => m.PostID == id);
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
            var post = getElement(id).Result;
            _context.Posts.Remove(post);
        }

        public void updateElement(Post post)
        {
            _context.Update(post);
        }

        public bool elementExists(int id)
        {
            return _context.Posts.Any(e => e.PostID == id);
        }
    }
}
