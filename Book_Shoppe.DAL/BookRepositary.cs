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
    public class BookRepositary
    {

       
        public static string Add(Book book)
        {
            using (DBContext booksContext = new DBContext())
            {
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
        public static void Delete(int BookID)
        {
            using(var bookContext = new DBContext())
            {
                Book book = bookContext.Books.Where(id => id.BookID == BookID).FirstOrDefault();
                bookContext.Books.Remove(book);
                bookContext.SaveChanges();
            }
        }
        public static string Edit(Book book)
        {
            DBContext booksContext = new DBContext();

            booksContext.Entry(book).State = EntityState.Modified;
            try
            {
                booksContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException.InnerException.Message != null)
                {
                    return "The title of the book should not be duplicated";
                }
                throw;
            }
            return null;
        }
        public static IEnumerable<Book> GetAllBooks()
        {
            DBContext booksContext = new DBContext();
             return booksContext.Books.ToList();
        }

       
        public static IEnumerable<Genre> GetAllGenres()
        {
            DBContext _context = new DBContext();
            return _context.Genres.ToList();
        }

        public static string GetGenreByGenreID(int id)
        {
            using(DBContext _context = new DBContext())
            {
                Genre genre = _context.Genres.Where(ID => ID.GenreID == id).SingleOrDefault();
                return genre.GenreName;
            }
        }
        public static string AddGenre(Genre genre)
        {
            DBContext _context = new DBContext();
            _context.Genres.Add(genre);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException.InnerException.Message != null)
                {
                    return "The title of the Genre should not be duplicated";
                }
                throw;
            }
            return null;
        }

        public static string DeleteGenre(int id)
        {
            using(DBContext _context = new DBContext())
            {
                Genre genre = _context.Genres.Where(Id => Id.GenreID == id).FirstOrDefault();
                _context.Genres.Remove(genre);
                _context.SaveChanges();
                return "Genre Removed Successfully";
            }
        }
        public static IEnumerable<Book> GetBooksByGenre(int id)
        {
            DBContext _context = new DBContext();
            return _context.Books.Where(m => m.GenreID == id).ToList();
        }
        public static IEnumerable<Book> GetBookByUserID()
        {
            DBContext _context = new DBContext();
            UserRepositary Repos = new UserRepositary();
            int userID = UserRepositary.GetCurrentUser().UserID;
            return _context.Books.Where(m => m.UserID == userID).ToList();
        }
        public static Book GetBookByID(int bookID)
        {
            DBContext booksContext = new DBContext();
            Book book = booksContext.Books.SingleOrDefault(ID => ID.BookID==bookID);
            return book;
        }
    }
   
}
