using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CSharpBlog.Models
{
    public class Post
    {
        public Post()
        {
            DateCreated = DateTime.Now;
            Views = 0;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public ApplicationUser Author { get; set; }
        public int Views { get; set; }
    }
}