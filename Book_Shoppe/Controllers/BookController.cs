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
using AutoMapper;

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
            Session["Genre"] = bookContext.GetGenreByGenreID(id);
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddBookFormVM book)
        {
            ViewBag.Genres = new SelectList(bookContext.GetAllGenres(), "GenreID", "GenreName");

            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<AddBookFormVM,Book>(); });
                IMapper iMapper = config.CreateMapper();
                Book _book = iMapper.Map<AddBookFormVM, Book>(book);
                _book.UserID = UserController.CurrentUser.UserID;
              

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

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Book,EditBookFormVM>(); });
            IMapper iMapper = config.CreateMapper();
            EditBookFormVM _book = iMapper.Map<Book,EditBookFormVM>(book);

           

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
        [ValidateAntiForgeryToken]
        public ActionResult Update(EditBookFormVM book)
        {
            ViewBag.Genres = new SelectList(bookContext.GetAllGenres(), "GenreID", "GenreName");

            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditBookFormVM, Book>(); });
                IMapper iMapper = config.CreateMapper();
                Book _book = iMapper.Map<EditBookFormVM, Book>(book);

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