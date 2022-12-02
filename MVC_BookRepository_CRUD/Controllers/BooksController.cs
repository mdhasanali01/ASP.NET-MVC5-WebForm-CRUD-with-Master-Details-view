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
    public class BooksController : Controller
    {
        BookRepositoryDbContext db = new BookRepositoryDbContext();
        // GET: Books
        public ActionResult Index()
        {
            return View(db.Books.Include(x => x.Publisher).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Publishers = db.Publishers.ToList();
            ViewBag.Books = db.Books.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(BookInputModel t)
        {
            if (ModelState.IsValid)
            {
                var Book = new Book
                {
                    BookName = t.BookName,
                    PublishDate = t.PublishDate,
                    Price = t.Price,
                    Available = t.Available,                    
                    PublisherId = t.PublisherId
                };
                string ext = Path.GetExtension(t.Picture.FileName);
                string f = Guid.NewGuid() + ext;
                t.Picture.SaveAs(Server.MapPath("~/Uploads/") + f);
                Book.Picture = f;
                db.Books.Add(Book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Publishers = db.Publishers.ToList();
            ViewBag.Books = db.Books.ToList();
            return View(t);
        }
        public ActionResult Edit(int id)
        {
            var t = db.Books.First(x => x.BookId == id);
            ViewBag.Publishers = db.Publishers.ToList();
            ViewBag.CurrentPic = t.Picture;
            return View(new BookEditModel { BookId = t.BookId, BookName = t.BookName, PublishDate = t.PublishDate,Price=t.Price, Available=t.Available, PublisherId = t.PublisherId });
        }
        [HttpPost]
        public ActionResult Edit(BookEditModel t)
        {
            var book = db.Books.First(x => x.BookId == t.BookId);
            if (ModelState.IsValid)
            {

                book.BookName = t.BookName;
                book.PublishDate= t.PublishDate;
                book.Price = t.Price;
                book.Available = t.Available;
                if (t.Picture != null)
                {
                    string ext = Path.GetExtension(t.Picture.FileName);
                    string f = Guid.NewGuid() + ext;
                    t.Picture.SaveAs(Server.MapPath("~/Uploads/") + f);
                    book.Picture = f;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentPic = book.Picture;
            ViewBag.Publishers = db.Publishers.ToList();
            return View(t);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Books.Include(x => x.Publisher).First(x => x.BookId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            var t = new Book { BookId = id };
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}