using Book_Shoppe.DAL;
using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.BL
{
    public class BookBL
    {
        public IEnumerable<Book> GetBooks()
        {
            IEnumerable<Book> Books = BookRepositary.GetAllBooks();
            return Books;
        }

        public IEnumerable<Book> GetUserBooks()
        {
            IEnumerable<Book> Books = BookRepositary.GetUserBooks();
            return Books;
        }
        public string Add(Book book)
        {
           return BookRepositary.Add(book);
        }

        public string Edit(Book book)
        {
           return BookRepositary.Edit(book);
        }

     
        public Book GetBookByID(int id)
        {
            return BookRepositary.GetBookByID(id);
        }

        public bool Delete(int id)
        {
            BookRepositary.Delete(id);
            return true;
        }
    }
}
