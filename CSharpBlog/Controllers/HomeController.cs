using CSharpBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;

namespace CSharpBlog.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string q, int? page)
        {
            ViewBag.Q = q;
            ViewBag.C = db.Categories;
            var pageNumber = page ?? 1;
            var posts = db.Posts.Include(p => p.Author)
                                .Include(p => p.Category)
                                .Include(p => p.Tags)
                                .Include(p => p.Comments)
                                .OrderByDescending(x => x.DateCreated);
            
            if (!string.IsNullOrEmpty(q))
            {
                posts = db.Posts.Include(p => p.Author).Include(p => p.Category).Where(x => x.Title.Contains(q)).OrderByDescending(x => x.DateCreated);
            }
            var currentPage = posts.ToPagedList(pageNumber, 5);

            return View(currentPage);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            return PartialView(db.Categories.ToList());
        }

        public ActionResult Category(int id)
        {
            var posts = db.Posts.OrderByDescending(a => a.DateCreated).Where(x => x.CategoryId == id)
                                .Include(x => x.Category)
                                .Include(x => x.Tags)
                                .Include(x => x.Comments)
                                .Include(x => x.Author);
            ViewBag.Category = db.Categories.Find(id).Name;
            ViewBag.C = db.Categories;
            return View(posts);
        }

        public ActionResult Tag(int id)
        {
            var posts = db.Posts.OrderByDescending(a => a.DateCreated).Where(x => x.Tags.Contains(db.Tags.FirstOrDefault(y => y.TagId==id)))
                                .Include(x => x.Category)
                                .Include(x => x.Tags)
                                .Include(x => x.Comments)
                                .Include(x => x.Author);
            ViewBag.Tag = db.Tags.Find(id).Name;
            ViewBag.C = db.Categories;
            return View(posts); ;
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