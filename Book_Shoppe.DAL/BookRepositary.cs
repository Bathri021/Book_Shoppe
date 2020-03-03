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
            DBContext booksContext = new DBContext();
            booksContext.Books.Add(book);
            try
            {
                booksContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                if(e.InnerException.InnerException.Message != null)
                {
                    return "The title of the book should not be duplicated";
                }
                else
                {
                    return "Please fill out the form correctly and sumbit your values";
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

        public static IEnumerable<Book> GetUserBooks()
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
