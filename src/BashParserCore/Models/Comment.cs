using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public Post Post { get; set; }
        public string Text { get; set; }
        public ApplicationUser Author { get; set; }
        public int? ParentID { get; set; }





    }
}
