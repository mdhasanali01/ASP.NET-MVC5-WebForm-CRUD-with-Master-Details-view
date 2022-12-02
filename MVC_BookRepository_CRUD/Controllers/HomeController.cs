using MVC_BookRepository_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_BookRepository_CRUD.Controllers
{
    public class HomeController : Controller
    {
        BookRepositoryDbContext db = new BookRepositoryDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Authors.ToList();
            return View();
        }
    }
}