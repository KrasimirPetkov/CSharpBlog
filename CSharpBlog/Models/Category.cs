using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSharpBlog.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}