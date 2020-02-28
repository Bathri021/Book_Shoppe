using Book_Shoppe.DAL;
using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Shoppe.BL;
using Book_Shoppe.Models;

namespace Book_Shoppe.Controllers
{
    public class BookController : Controller
    {
        BookBL bookContext = new BookBL();
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
        public ActionResult Create(AddBookFormVM book)
        {
            if (ModelState.IsValid)
            {
                Book _book = new Book()
                {
                    UserID = book.UserID,
                    Title = book.Title,
                    Author = book.Author,
                    Genere = book.Genere,
                    Price = book.Price,
                    NoOfPages = book.NoOfPages
                };
                bookContext.Add(_book);
                TempData["Message"] = "Added Successfully";
            }
            return View(book);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Book book = bookContext.GetBookByID(id);

            EditBookFormVM _book = new EditBookFormVM()
            {
                BookID = book.BookID,
                UserID = book.UserID,
                Title = book.Title,
                Author = book.Author,
                Genere = book.Genere,
                Price = book.Price,
                NoOfPages = book.NoOfPages
            };
            return View(_book);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            bookContext.Delete(id);
            TempData["Message"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(EditBookFormVM book)
        {
            if (ModelState.IsValid)
            {
                Book _book = new Book()
                {
                    BookID = book.BookID,
                    UserID = book.UserID,
                    Title = book.Title,
                    Author = book.Author,
                    Genere = book.Genere,
                    Price = book.Price,
                    NoOfPages = book.NoOfPages
                };
                bookContext.Edit(_book);
                TempData["Message"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            return View("Edit", book);
        }
    }
}