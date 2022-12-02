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
    public class TopicsController : Controller
    {
        BookRepositoryDbContext db = new BookRepositoryDbContext();
        // GET: Topics
        public ActionResult Index()
        {
            return View(db.Topics);
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateTopic()
        {
            return PartialView("_CreateTopic");
        }
        [HttpPost]
        public PartialViewResult CreateTopic(Topic t)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                db.Topics.Add(t);
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
        public PartialViewResult EditTopic(int id)
        {
            var b = db.Topics.First(x => x.TopicId == id);
            return PartialView("_EditTopic", b);
        }
        [HttpPost]
        public PartialViewResult EditTopic(Topic b)
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
            return View(db.Topics.First(x => x.TopicId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int TopicId)
        {
            var t = new Topic { TopicId = TopicId };
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}