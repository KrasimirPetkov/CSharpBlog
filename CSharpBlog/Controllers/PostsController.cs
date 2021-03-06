﻿using System;
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
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Include(p => p.Category)
                                .Include(p => p.Tags)
                                .Include(p => p.Comments)
                                .Include(p => p.Author)
                                .FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            post.Views += 1;
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Title,Body,CategoryId")] Post post, string tags)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                string[] t = tags.Split(new string[] { ", " }, StringSplitOptions.None);
                post.Tags = new HashSet<Tag>();
                foreach (var tag in t)
                {
                    if (!db.Tags.Any(x => x.Name.ToLower()==tag.ToLower()))
                    {
                        db.Tags.Add(new Tag() { Name = tag });
                    }
                    db.SaveChanges();
                    post.Tags.Add(db.Tags.FirstOrDefault(x => x.Name.ToLower() == tag.ToLower()));
                }
                post.Author =  db.Users.FirstOrDefault(x => x.Id == currentUserId);
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.FirstOrDefault(x => x.Id == id);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.Category.CategoryId);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Views,DateCreated,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.DateModified = DateTime.Now;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index", "Home", null);
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
