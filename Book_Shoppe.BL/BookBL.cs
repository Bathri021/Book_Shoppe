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
            IEnumerable<Book> Books =BookRepositary.GetAllBooks();
            return Books;
        }

        public static IEnumerable<Genre> GetGenres()
        {
            IEnumerable<Genre> Generes = BookRepositary.GetAllGenres();
            return Generes;
        }

        public string GetGenreByGenreID(int id)
        {
            string GenreName = BookRepositary.GetGenreByGenreID(id);
            return GenreName;
        }

        public string AddGenre(Genre genre)
        {
            return BookRepositary.AddGenre(genre);
        }

        public string DeleteGenre(int id)
        {
            return BookRepositary.DeleteGenre(id);
        }
        public IEnumerable<Book> GetBooksByGenre(int id)
        {
            IEnumerable<Book> BooksByGenre = BookRepositary.GetBooksByGenre(id);
            return BooksByGenre;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            IEnumerable<Genre> Generes = BookRepositary.GetAllGenres();
            return Generes;
        }
        public IEnumerable<Book> GetUserBooks()
        {
            IEnumerable<Book> Books = BookRepositary.GetBookByUserID();
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
