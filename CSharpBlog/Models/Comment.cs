using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSharpBlog.Models
{
    public class Comment
    {
        public Comment()
        {
            this.DateCreated = DateTime.Now;
        }

        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string AuthorId { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual Post Post { get; set; }
    }
}