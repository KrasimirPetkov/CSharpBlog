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
            var pageNumber = page ?? 1;
            var posts = db.Posts.Include(p => p.Author).Include(p => p.Category).OrderByDescending(x => x.DateCreated);
            
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