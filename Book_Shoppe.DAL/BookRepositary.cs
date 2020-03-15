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
        IEnumerable<Book> GetBookByUserID();
        Book GetBookByID(int bookID);
        IEnumerable<Book> SearchResult(string SearchValue);
        Book GetBookDetails(int bookID);
    }
    public class BookRepositary : IBookRepositary
    {
        public string Add(Book book)
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
        public void Delete(int BookID)
        {
            using(var bookContext = new DBContext())
            {
                Book book = bookContext.Books.Where(id => id.BookID == BookID).FirstOrDefault();
                bookContext.Books.Remove(book);
                bookContext.SaveChanges();
            }
        }
        public string Edit(Book book)
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
        public IEnumerable<Book> GetAllBooks()
        {
            DBContext booksContext = new DBContext();
             return booksContext.Books.ToList();
        }

       
        public IEnumerable<Genre> GetAllGenres()
        {
            DBContext _context = new DBContext();
            return _context.Genres.ToList();
        }

        public string GetGenreByGenreID(int id)
        {
            using(DBContext _context = new DBContext())
            {
                Genre genre = _context.Genres.Where(ID => ID.GenreID == id).SingleOrDefault();
                return genre.GenreName;
            }
        }
        public string AddGenre(Genre genre)
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

        public string DeleteGenre(int id)
        {
            using(DBContext _context = new DBContext())
            {
                Genre genre = _context.Genres.Where(Id => Id.GenreID == id).FirstOrDefault();
                _context.Genres.Remove(genre);
                _context.SaveChanges();
                return "Genre Removed Successfully";
            }
        }
        public IEnumerable<Book> GetBooksByGenre(int id)
        {
            DBContext _context = new DBContext();
            return _context.Books.Where(m => m.GenreID == id).ToList();
        }
        public IEnumerable<Book> GetBookByUserID()
        {
            UserRepositary IUserRepos = new UserRepositary();
            DBContext _context = new DBContext();
            UserRepositary Repos = new UserRepositary();
            int userID = IUserRepos.GetCurrentUser().UserID;
            return _context.Books.Where(m => m.UserID == userID).ToList();
        }
        public Book GetBookByID(int bookID)
        {
            DBContext booksContext = new DBContext();
            Book book = booksContext.Books.SingleOrDefault(ID => ID.BookID==bookID);
            return book;
        }

        public IEnumerable<Book> SearchResult(string SearchValue)
        {
            IEnumerable<Book> SearchedBooks;
            using (DBContext _context = new DBContext())
            {
                try
                {
                  SearchedBooks = _context.Books.Where(b => b.Title.Contains(SearchValue) || b.Author.Contains(SearchValue) || SearchValue == null).ToList();
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
            using(DBContext _context = new DBContext())
            {
                return _context.Books.Where(ID => ID.BookID == bookID).SingleOrDefault();
            }
        }
    }
   
}
