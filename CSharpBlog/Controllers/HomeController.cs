using CSharpBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CSharpBlog.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string search)
        {
            var posts = db.Posts.Include(p => p.Author).Include(p => p.Category).OrderByDescending(x => x.DateCreated).ToList();
            
            if (!string.IsNullOrEmpty(search))
            {
                posts = db.Posts.Include(p => p.Author).Include(p => p.Category).Where(x => x.Title.Contains(search)).OrderByDescending(x => x.DateCreated).ToList();
            }

            return View(posts);
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