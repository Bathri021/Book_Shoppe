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
        public static List<Book> bookList = new List<Book>();

        static BookRepositary()
        {
            bookList.Add(new Book { BookID = 1, Title = "Warrior", Author = "Dany", Genere = "Fiction", Price = 200 });
            bookList.Add(new Book { BookID = 2, Title = "It's Me", Author = "Willam", Genere = "Philasopy", Price = 240 });
            bookList.Add(new Book { BookID = 3, Title = "Please Don't", Author = "Robert", Genere = "Fiction", Price = 400 });
            bookList.Add(new Book { BookID = 4, Title = "Real Lover", Author = "John", Genere = "Story", Price = 730 });
            bookList.Add(new Book { BookID = 5, Title = "Why it is happend?", Author = "David", Genere = "Story", Price = 840 });
            bookList.Add(new Book { BookID = 6, Title = "Belives not permanent", Author = "Steve", Genere = "Story", Price = 790 });
            bookList.Add(new Book { BookID = 7, Title = "Live your present", Author = "Tony", Genere = "Story", Price = 1730 });
        }
        public static void Add(Book book)
        {
            bookList.Add(book);
        }
        public static void Delete(int BookID)
        {
            Book book = GetBookByID(BookID);
            if (book != null)
                bookList.Remove(book);
        }
        public static void Updata(Book book)
        {
            Book bookValue = bookList.Find(id => id.BookID == book.BookID);
            bookValue.Title = book.Title;
            bookValue.Author = book.Author;
            bookValue.Genere = book.Genere;
            bookValue.NoOfPages = book.NoOfPages;
            bookValue.Price = book.Price;
        }
        public static IEnumerable<Book> GetAllBooks()
        {
            return bookList;
        }
        public static Book GetBookByID(int bookID)
        {
            return bookList.Find(id => id.BookID == bookID);
        }
    }
   
}
