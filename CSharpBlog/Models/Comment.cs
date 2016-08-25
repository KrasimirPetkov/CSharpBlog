using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSharpBlog.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public ApplicationUser Author { get; set; }
        public virtual Post Post { get; set; }
    }
}