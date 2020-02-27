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

        public void Add(Book book)
        {
            BookRepositary.Add(book);
        }
    }
}
