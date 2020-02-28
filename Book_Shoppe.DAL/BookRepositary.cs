using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.DAL
{
    public class BookRepositary
    {

        static void InitializeBooks()
        {
            DBContext booksContext = new DBContext();
            booksContext.Books.Add(new Book { BookID = 1, UserID = 1, Title = "Warrior", Author = "Dany", Genere = "Fiction", Price = 200 });
            booksContext.Books.Add(new Book { BookID = 2, UserID = 1, Title = "It's Me", Author = "Willam", Genere = "Philasopy", Price = 240 });
            booksContext.Books.Add(new Book { BookID = 3, UserID = 1, Title = "Please Don't", Author = "Robert", Genere = "Fiction", Price = 400 });
            booksContext.Books.Add(new Book { BookID = 4, UserID = 1, Title = "Real Lover", Author = "John", Genere = "Story", Price = 730 });
            booksContext.Books.Add(new Book { BookID = 5, UserID = 1, Title = "Why it is happend?", Author = "David", Genere = "Story", Price = 840 });
            booksContext.Books.Add(new Book { BookID = 6, UserID = 1, Title = "Belives not permanent", Author = "Steve", Genere = "Story", Price = 790 });
            booksContext.Books.Add(new Book { BookID = 7, UserID = 1, Title = "Live your present", Author = "Tony", Genere = "Story", Price = 1730 });
            booksContext.SaveChanges();
        }
        public static void Add(Book book)
        {
            DBContext booksContext = new DBContext();
            booksContext.Books.Add(book);
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
        public static void Edit(Book book)
        {
            DBContext booksContext = new DBContext();

            booksContext.Entry(book).State = EntityState.Modified;
            booksContext.SaveChanges();
        }
        public static IEnumerable<Book> GetAllBooks()
        {
            InitializeBooks();
            DBContext booksContext = new DBContext();
            return booksContext.Books.ToList();
        }
        public static Book GetBookByID(int bookID)
        {
            DBContext booksContext = new DBContext();
            Book book = booksContext.Books.SingleOrDefault(ID => ID.BookID==bookID);
            return book;
        }
    }
   
}
