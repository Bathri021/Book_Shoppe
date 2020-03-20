using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.DAL
{
    public interface IBookRepositary
    {
        string Add(Book book);
        void Delete(int BookID);
        string Edit(Book book);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Genre> GetAllGenres();
        string GetGenreByGenreID(int id);
        string AddGenre(Genre genre);
        string DeleteGenre(int id);
        IEnumerable<Book> GetBooksByGenre(int id);
        IEnumerable<Book> GetBookByUserID(int userID);
        Book GetBookByID(int bookID);
        IEnumerable<Book> SearchResult(string SearchValue);
        Book GetBookDetails(int bookID);
    }
    public class BookRepositary : IBookRepositary
    {
        public string Add(Book book)
        {
            using (BookShoppeDBContext booksContext = new BookShoppeDBContext())
            {
                // Transaction Meathod Testing
                using(DbContextTransaction dbTran = booksContext.Database.BeginTransaction())
                {
                    try
                    {
                        if (book.UserID==0)
                        {
                            return "User ID needed";
                        }
                        booksContext.Books.Add(book);
                        booksContext.SaveChanges();
                        dbTran.Commit();
                    }
                    catch (Exception)
                    {
                        dbTran.Rollback();
                        return null;
                    }
                }
            }
            return null;
        }

        public void Delete(int BookID)
        {
            using(BookShoppeDBContext bookContext = new BookShoppeDBContext())
            {
                Book book = bookContext.Books.Where(id => id.BookID == BookID).FirstOrDefault();
                bookContext.Books.Remove(book);
                bookContext.SaveChanges();
            }
        }

        public string Edit(Book book)
        {
            using (BookShoppeDBContext booksContext = new BookShoppeDBContext())
            {
                booksContext.Entry(book).State = EntityState.Modified;
                booksContext.SaveChanges();
                return null;
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            using (BookShoppeDBContext booksContext = new BookShoppeDBContext())
            {
                return booksContext.Books.Include("Genre").ToList();
            }
        }

       
        public IEnumerable<Genre> GetAllGenres()
        {
            using (BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                return _context.Genres.ToList();
            }
        }

        public string GetGenreByGenreID(int id)
        {
            using(BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                Genre genre = _context.Genres.Where(ID => ID.GenreID == id).SingleOrDefault();
                return genre.GenreName;
            }
        }

        public string AddGenre(Genre genre)
        {
            using (BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                _context.Genres.Add(genre);
                try
                {
                   _context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                   return e.Message;
                }
                return null;
            }
        }

        public string DeleteGenre(int id)
        {
            using(BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                Genre genre = _context.Genres.Where(Id => Id.GenreID == id).FirstOrDefault();
                _context.Genres.Remove(genre);
                _context.SaveChanges();
                return "Genre Removed Successfully";
            }
        }

        public IEnumerable<Book> GetBooksByGenre(int id)
        {
            using (BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                return _context.Books.Include("Genre").Where(m => m.GenreID == id).ToList();
            }
        }

        public IEnumerable<Book> GetBookByUserID(int userID)
        {
            using (BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                return _context.Books.Include("Genre").Where(m => m.UserID == userID).ToList();
            }
        }

        public Book GetBookByID(int bookID)
        {
            using (BookShoppeDBContext booksContext = new BookShoppeDBContext())
            {
                Book book = booksContext.Books.SingleOrDefault(ID => ID.BookID == bookID);
                return book;
            }
        }

        public IEnumerable<Book> SearchResult(string SearchValue)
        {
            IEnumerable<Book> SearchedBooks;
            using (BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                try
                {
                  SearchedBooks = _context.Books.Include("Genre").Where(b => b.Title.Contains(SearchValue) || b.Author.Contains(SearchValue) || SearchValue == null).ToList();
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
                return SearchedBooks;
            }
        }

        public Book GetBookDetails(int bookID)
        {
            using(BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                return _context.Books.Where(ID => ID.BookID == bookID).SingleOrDefault();
            }
        }
    }
   
}
