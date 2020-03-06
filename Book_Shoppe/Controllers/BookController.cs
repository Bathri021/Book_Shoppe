using Book_Shoppe.DAL;
using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Book_Shoppe.BL;
using Book_Shoppe.Models;
using Book_Shoppe.App_Start;

namespace Book_Shoppe.Controllers
{
   
    [HandleError]
    public class BookController : Controller
    {
        BookBL bookContext = new BookBL();


        // // GET: Book 
        [SellerAuthorizationFilter]
        public ActionResult Index()
        {
            IEnumerable<Book> Books = bookContext.GetUserBooks();
            return View(Books);
        }

        [AdminAuthorizationFilter]
        public ActionResult EditGenre()
        {
            IEnumerable<Genre> Genres = bookContext.GetAllGenres();
            return View(Genres);
        }

        public ActionResult Geners(int id)
        {
            IEnumerable<Book> BooksByGenre = bookContext.GetBooksByGenre(id);
            return View(BooksByGenre);
        }

        public ActionResult DeleteGenre(int id)
        {
            ViewBag.Message =  bookContext.DeleteGenre(id);
            return RedirectToAction("EditGenre");
        }

        [HttpPost]
        public ActionResult AddGenre(FormCollection fc)
        {
            Genre Genre = new Genre();
            Genre.GenreName = fc[0];

            ViewBag.Alert = bookContext.AddGenre(Genre);

            if (ViewBag.Alert==null)
            {
                ViewBag.Message = "Added Successfully";
                ViewBag.Alert = null;
            }
            return RedirectToAction("EditGenre");
        }

        [HttpGet]
        [SellerAuthorizationFilter]
        public ActionResult Create()
        {
            ViewBag.Genres = new SelectList(bookContext.GetAllGenres(),"GenreID","GenreName");
            return View();
        }

        [HttpPost]
        [SellerAuthorizationFilter]
        public ActionResult Create(FormCollection Fc ,AddBookFormVM book)
        {
            ViewBag.Genres = new SelectList(bookContext.GetAllGenres(), "GenreID", "GenreName");

            if (ModelState.IsValid)
            {
                Book _book = new Book()
                {
                    UserID = UserController.CurrentUser.UserID,
                    Title = book.Title,
                    Author = book.Author,
                    GenreID = book.GenreID,
                    Price = book.Price,
                    NoOfPages = book.NoOfPages
                };

                ViewBag.Alert =  bookContext.Add(_book);

                if (ViewBag.Alert == null)
                {
                   ViewBag.Message = "Added Successfully";
                    ViewBag.Alert = null;
                }

            }
            return View(book);
        }
        [HttpGet]
        [SellerAuthorizationFilter]
        public ActionResult Edit(int id)
        {
            Book book = bookContext.GetBookByID(id);
            ViewBag.Genres = new SelectList(bookContext.GetAllGenres(), "GenreID", "GenreName");


            EditBookFormVM _book = new EditBookFormVM()
            {
                BookID = book.BookID,
                UserID = book.UserID,
                Title = book.Title,
                Author = book.Author,
                GenreID = book.GenreID,
                Price = book.Price,
                NoOfPages = book.NoOfPages
            };

            return View(_book);
        }

        [HttpGet]
        [SellerAuthorizationFilter]
        public ActionResult Delete(int id)
        {
            bookContext.Delete(id);
            ViewBag.Message = "Deleted Successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [SellerAuthorizationFilter]
        public ActionResult Update(EditBookFormVM book)
        {
            ViewBag.Genres = new SelectList(bookContext.GetAllGenres(), "GenreID", "GenreName");

            if (ModelState.IsValid)
            {
                Book _book = new Book()
                {
                    BookID = book.BookID,
                    UserID = book.UserID,
                    Title = book.Title,
                    Author = book.Author,
                    GenreID = book.GenreID,
                    Price = book.Price,
                    NoOfPages = book.NoOfPages
                };
                ViewBag.Alert = bookContext.Edit(_book);
                if (ViewBag.Alert == null)
                {
                    ViewBag.Message = "Updated Successfully";
                    ViewBag.Alert = null;
                }

                return RedirectToAction("Index");
            }
            return View("Edit", book);
        }
    }
}