using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.DAL
{
    public class BookRepositary
    {
      
        static BookRepositary()
        {
            DBContext booksContext = new DBContext();
            booksContext.Books.Add(new Book { BookID = 1, Title = "Warrior", Author = "Dany", Genere = "Fiction", Price = 200 });
            booksContext.Books.Add(new Book { BookID = 2, Title = "It's Me", Author = "Willam", Genere = "Philasopy", Price = 240 });
            booksContext.Books.Add(new Book { BookID = 3, Title = "Please Don't", Author = "Robert", Genere = "Fiction", Price = 400 });
            booksContext.Books.Add(new Book { BookID = 4, Title = "Real Lover", Author = "John", Genere = "Story", Price = 730 });
            booksContext.Books.Add(new Book { BookID = 5, Title = "Why it is happend?", Author = "David", Genere = "Story", Price = 840 });
            booksContext.Books.Add(new Book { BookID = 6, Title = "Belives not permanent", Author = "Steve", Genere = "Story", Price = 790 });
            booksContext.Books.Add(new Book { BookID = 7, Title = "Live your present", Author = "Tony", Genere = "Story", Price = 1730 });

        }
        public static void Add(Book book)
        {
            DBContext booksContext = new DBContext();
            booksContext.Books.Add(book);
        }
        public static void Delete(int BookID)
        {
            DBContext booksContext = new DBContext();
            Book book = GetBookByID(BookID);
            if (book != null)
                booksContext.Books.Remove(book);
        }
        public static void Updata(Book book)
        {
            DBContext booksContext = new DBContext();
            Book bookValue = booksContext.Books.Find(book.BookID);
            bookValue.Title = book.Title;
            bookValue.Author = book.Author;
            bookValue.Genere = book.Genere;
            bookValue.NoOfPages = book.NoOfPages;
            bookValue.Price = book.Price;
        }
        public static IEnumerable<Book> GetAllBooks()
        {
            DBContext booksContext = new DBContext();
            return booksContext.Books.ToList();
        }
        public static Book GetBookByID(int bookID)
        {
            DBContext booksContext = new DBContext();
            return booksContext.Books.Find(bookID);
        }
    }
   
}
