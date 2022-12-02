using MVC_BookRepository_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVC_BookRepository_CRUD.ViewModels;
using System.IO;

namespace MVC_BookRepository_CRUD.Controllers
{
    [Authorize]
    public class BookAuthorsController : Controller
    {
        private readonly BookRepositoryDbContext db = new BookRepositoryDbContext();
        // GET: BookAuthors
        public ActionResult Index()
        {
            var data = db.Books
                .Include(x => x.AuthorBooks.Select(y => y.Author))
                .Include(x=> x.Publisher)
                .ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            ViewBag.Publishers = db.Publishers.ToList();

            var data = new AuthorBookInputModel();
            data.Authors.Add(new AuthorViewModel());
            return View(data);
        }
        [HttpPost]
        public ActionResult Create(AuthorBookInputModel model, int[] AuthorID)
        {
            if (ModelState.IsValid)
            {
                var et = new Book
                {
                    BookName = model.BookName,
                    PublishDate = model.PublishDate,
                    Price = model.Price,
                    Available = model.Available,
                    PublisherId= model.PublisherId
                    
                };
                foreach (var i in AuthorID)
                {
                    et.AuthorBooks.Add(new AuthorBook { AuthorId = i });
                }
                if (model.Picture.ContentLength > 0)
                {
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string fileName = Guid.NewGuid() + ext;
                    model.Picture.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), fileName));
                    et.Picture = fileName;
                }
                db.Books.Add(et);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Authors = db.Authors.ToList();

            return View(model);
        }
        public JsonResult GetAddress(int id)
        {
            var t = db.Authors.FirstOrDefault(x => x.AuthorId == id);
            return Json(t.Address);
        }
        public ActionResult CreateNewField(AuthorBookInputModel data)
        {
            ViewBag.Authors = db.Authors.ToList();
            //data.Authors.Add(new AuthorViewModel());
            return PartialView();
        }
    }
}