using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public IEnumerable<Comment> embeddedComments { get; set; }
        public int? ParentID { get; set; }

        public Post post { get; set; }
        public int PostID { get; set; }

        public Comment()
        {

        }
        public Comment(int? parentID, IEnumerable<Comment> comments)
        {
            ParentID = parentID;
            embeddedComments = comments;
        }

    }
}
