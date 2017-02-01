using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.Models
{
    public class Userpic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public byte[] Picture { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
