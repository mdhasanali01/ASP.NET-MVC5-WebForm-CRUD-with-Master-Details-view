using MVC_BookRepository_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading;

namespace MVC_BookRepository_CRUD.Controllers
{
    [Authorize]
    public class PublishersController : Controller
    {
        BookRepositoryDbContext db = new BookRepositoryDbContext();
        // GET: Authors
        public ActionResult Index()
        {
            return View(db.Publishers.Include(b => b.Books).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreatePublisher()
        {
            return PartialView("_CreatePublisher");
        }
        [HttpPost]
        public PartialViewResult CreatePublisher(Publisher b)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                db.Publishers.Add(b);
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
        public PartialViewResult EditPublisher(int id)
        {
            var b = db.Publishers.First(x => x.PublisherId == id);
            return PartialView("_EditPublisher", b);
        }
        [HttpPost]
        public PartialViewResult EditPublisher(Publisher b)
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
            return View(db.Publishers.First(x => x.PublisherId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int PublisherId)
        {
            var b = new Publisher {PublisherId = PublisherId };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
