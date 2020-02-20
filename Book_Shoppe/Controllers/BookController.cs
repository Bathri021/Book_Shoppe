using Book_Shoppe.DAL;
using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Shoppe.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            IEnumerable<Book> Books = BookRepositary.GetAllBooks();
            return View(Books);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                BookRepositary.Add(book);
                TempData["Message"] = "Added Successfully";
            }
            return View(book);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Book book = BookRepositary.GetBookByID(id);
            return View(book);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            BookRepositary.Delete(id);
            TempData["Message"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(Book book)
        {
            if (ModelState.IsValid)
            {
                BookRepositary.Updata(book);
                TempData["Message"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            return View("Edit", book);
        }
    }
}