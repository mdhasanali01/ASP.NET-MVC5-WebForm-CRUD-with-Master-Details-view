using MVC_BookRepository_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MVC_BookRepository_CRUD.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        BookRepositoryDbContext db = new BookRepositoryDbContext();
        // GET: Authors
        public ActionResult Index()
        {
            return View(db.Authors.Include(b => b.AuthorBooks).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateAuthor()
        {
            return PartialView("_CreateAuthor");
        }
        [HttpPost]
        public PartialViewResult CreateAuthor(Author b)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                db.Authors.Add(b);
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public PartialViewResult EditAuthor(int id)
        {
            var b = db.Authors.First(x => x.AuthorId == id);
            return PartialView("_EditAuthor", b);
        }
        [HttpPost]
        public PartialViewResult EditAuthor(Author b)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(db.Authors.First(x => x.AuthorId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int AuthorId)
        {
            var b = new Author { AuthorId = AuthorId };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}