﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSharpBlog.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}