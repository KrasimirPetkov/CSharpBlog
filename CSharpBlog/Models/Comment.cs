using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSharpBlog.Models
{
    public class Comment
    {
        public Comment()
        {
            DateCreated = DateTime.Now;
        }

        public int Id { get; set; }
        public int PostId { get; set; }
        public string ApplicationUserId { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}