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
            using(DBContext bookContext = new DBContext())
            {
                Book book = bookContext.Books.Where(id => id.BookID == BookID).FirstOrDefault();
                bookContext.Books.Remove(book);
                bookContext.SaveChanges();
            }
        }

        public string Edit(Book book)
        {
            using (DBContext booksContext = new DBContext())
            {
                booksContext.Entry(book).State = EntityState.Modified;
                booksContext.SaveChanges();
                return null;
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            using (DBContext booksContext = new DBContext())
            {
                return booksContext.Books.Include("Genre").ToList();
            }
        }

       
        public IEnumerable<Genre> GetAllGenres()
        {
            using (DBContext _context = new DBContext())
            {
                return _context.Genres.ToList();
            }
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
            using (DBContext _context = new DBContext())
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
            using (DBContext _context = new DBContext())
            {
                return _context.Books.Include("Genre").Where(m => m.GenreID == id).ToList();
            }
        }

        public IEnumerable<Book> GetBookByUserID()
        {
            UserRepositary IUserRepos = new UserRepositary();
            using (DBContext _context = new DBContext())
            {
                UserRepositary Repos = new UserRepositary();
                int userID = IUserRepos.GetCurrentUser().UserID;
                return _context.Books.Include("Genre").Where(m => m.UserID == userID).ToList();
            }
        }

        public Book GetBookByID(int bookID)
        {
            using (DBContext booksContext = new DBContext())
            {
                Book book = booksContext.Books.SingleOrDefault(ID => ID.BookID == bookID);
                return book;
            }
        }

        public IEnumerable<Book> SearchResult(string SearchValue)
        {
            IEnumerable<Book> SearchedBooks;
            using (DBContext _context = new DBContext())
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
            using(DBContext _context = new DBContext())
            {
                return _context.Books.Where(ID => ID.BookID == bookID).SingleOrDefault();
            }
        }
    }
   
}
