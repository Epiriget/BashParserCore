﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BashParserCore.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public Post post { get; set; }
    }
}
