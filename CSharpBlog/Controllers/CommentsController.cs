using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSharpBlog.Models;
using PagedList;
using Microsoft.AspNet.Identity;

namespace CSharpBlog.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index(int postId, int? page)
        {
            ViewBag.PostId = postId;
            int comCount = db.Comments.Where(c => c.PostId == postId).Count();
            var pageNumber = page ?? (comCount%4!=0?comCount/4+1:comCount/4);
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            var comments = db.Comments.Where(c => c.PostId == postId).Include(c => c.Author).ToList();
            var currentPage = comments.ToPagedList(pageNumber, 4);
            return PartialView(currentPage);
        }

        // GET: Comments/Create
        [Authorize]
        public ActionResult Create(int postId)
        {
            ViewBag.PostId = postId;
            return PartialView();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PostId,Body")] Comment comment)
        {
            string currentUserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                comment.ApplicationUserId = db.Users.FirstOrDefault(x => x.Id == currentUserId).Id;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Posts", new { @id = comment.PostId });
            }

            return PartialView(comment);
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PostId,Body,DateCreated,DateModified")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Posts", new { @id = comment.PostId });
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "Posts", new { @id = comment.PostId });
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
