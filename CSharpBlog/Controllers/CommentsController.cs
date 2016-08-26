using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSharpBlog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CSharpBlog.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        [Authorize]
        public ActionResult Create(int postId)
        {
            ViewBag.PostId = postId;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        [Authorize]
        public ActionResult CreateComment([Bind(Include = "CommentId,PostId,Body")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = HttpContext.User.Identity.GetUserId();
                comment.AuthorId = db.Users.FirstOrDefault(x => x.Id == currentUserId).Id;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }

            return PartialView(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}