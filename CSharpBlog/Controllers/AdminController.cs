using CSharpBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSharpBlog.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            ViewBag.UC = db.Users.Count();
            ViewBag.PC = db.Posts.Count();
            ViewBag.CoC = db.Comments.Count();
            ViewBag.CaC = db.Categories.Count();
            ViewBag.TC = db.Tags.Count();
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