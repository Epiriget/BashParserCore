using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public string Rating { get; set; }
        public string Date { get; set; }
        public string PostName { get; set; }
        public string Text { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
