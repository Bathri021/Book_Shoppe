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
    public class BookController : Controller
    {
        IBookBL IBookBL = new BookBL();

        // GET: Book 
        // Sellers Books Page
       // [SellerAuthorizationFilter]
        [Authorize(Roles ="Seller")]
        public ActionResult Index()
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            IEnumerable<Book> Books = IBookBL.GetUserBooks(userID);
            return View(Books);
        }

        // [AdminAuthorizationFilter]
         [Authorize(Roles ="Admin")]
        public ActionResult EditGenre()
        {
            IEnumerable<Genre> Genres = IBookBL.GetAllGenres();
            return View(Genres);
        }

        // Master nav link Geners filter
        public ActionResult Geners(int id)
        {
            IEnumerable<Book> BooksByGenre = IBookBL.GetBooksByGenre(id);
            Session["Genre"] = IBookBL.GetGenreByGenreID(id);
            return View(BooksByGenre);
        }

        // Remove the Genre
        public ActionResult DeleteGenre(int id)
        {
            ViewBag.Message =  IBookBL.DeleteGenre(id);
            return RedirectToAction("EditGenre");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       // [AdminAuthorizationFilter]
        [Authorize(Roles = "Admin")]
        public ActionResult AddGenre(FormCollection fc)
        {
            Genre Genre = new Genre();
            Genre.GenreName = fc[1];

            ViewBag.Alert = IBookBL.AddGenre(Genre);

            if (ViewBag.Alert==null)
            {
                ViewBag.Message = "Added Successfully";
                ViewBag.Alert = null;
            }
            return RedirectToAction("EditGenre");
        }

        // Get Meathod for Add new Book
        [HttpGet]
       // [SellerAuthorizationFilter]
       [Authorize(Roles ="Seller")]
        public ActionResult Create()
        {
            ViewBag.Genres = new SelectList(IBookBL.GetAllGenres(),"GenreID","GenreName");
            return View();
        }


        // Post Meathod for Add new Book
        [HttpPost]
       // [SellerAuthorizationFilter]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Seller")]
        public ActionResult Create(AddBookFormVM book)
        {
            ViewBag.Genres = new SelectList(IBookBL.GetAllGenres(), "GenreID", "GenreName");

            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<AddBookFormVM,Book>(); });
                IMapper iMapper = config.CreateMapper();
                Book _book = iMapper.Map<AddBookFormVM, Book>(book);
                _book.UserID = UserController.CurrentUser.UserID;
              

                ViewBag.Alert =  IBookBL.Add(_book);

                if (ViewBag.Alert == null)
                {
                   ViewBag.Message = "Added Successfully";
                    ViewBag.Alert = null;
                }

            }
            return View(book);
        }

        // Edit the Details of the Existing Book
       // [SellerAuthorizationFilter]
        [HttpGet]
        [Authorize(Roles ="Seller")]
        public ActionResult Edit(int id)
        {
            Book book = IBookBL.GetBookByID(id);
            ViewBag.Genres = new SelectList(IBookBL.GetAllGenres(), "GenreID", "GenreName");

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Book,EditBookFormVM>(); });
            IMapper iMapper = config.CreateMapper();
            EditBookFormVM _book = iMapper.Map<Book,EditBookFormVM>(book);

           

            return View(_book);
        }

        // Delete the Existing Book
        [HttpGet]
        //[SellerAuthorizationFilter]
        [Authorize(Roles ="Seller")]
        public ActionResult Delete(int id)
        {
            IBookBL.Delete(id);
            ViewBag.Message = "Deleted Successfully";
            return RedirectToAction("Index");
        }

        // Post Meathod for Update the Edited Book Details
        [HttpPost]
       // [SellerAuthorizationFilter]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Seller")]
        public ActionResult Update(EditBookFormVM book)
        {
            ViewBag.Genres = new SelectList(IBookBL.GetAllGenres(), "GenreID", "GenreName");

            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditBookFormVM, Book>(); });
                IMapper iMapper = config.CreateMapper();
                Book _book = iMapper.Map<EditBookFormVM, Book>(book);

                ViewBag.Alert = IBookBL.Edit(_book);
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