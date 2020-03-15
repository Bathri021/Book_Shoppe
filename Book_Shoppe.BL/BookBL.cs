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
        IBookRepositary IBookRepos = new BookRepositary();
        public IEnumerable<Book> GetBooks()
        {
            IEnumerable<Book> Books = IBookRepos.GetAllBooks();
            return Books;
        }

        public static IEnumerable<Genre> GetGenres()
        {
            BookRepositary IBookRepos = new BookRepositary();
            IEnumerable<Genre> Generes = IBookRepos.GetAllGenres();
            return Generes;
        }

        public string GetGenreByGenreID(int id)
        {
            string GenreName = IBookRepos.GetGenreByGenreID(id);
            return GenreName;
        }

        public string AddGenre(Genre genre)
        {
            return IBookRepos.AddGenre(genre);
        }

        public string DeleteGenre(int id)
        {
            return IBookRepos.DeleteGenre(id);
        }
        public IEnumerable<Book> GetBooksByGenre(int id)
        {
            IEnumerable<Book> BooksByGenre = IBookRepos.GetBooksByGenre(id);
            return BooksByGenre;
        }

        public IEnumerable<Book> SearchResult(string SearchValue)
        {
            IEnumerable<Book> SearchedBooks = IBookRepos.SearchResult(SearchValue);
            return SearchedBooks;
        }
        public IEnumerable<Genre> GetAllGenres()
        {
            IEnumerable<Genre> Generes = IBookRepos.GetAllGenres();
            return Generes;
        }
        public IEnumerable<Book> GetUserBooks()
        {
            IEnumerable<Book> Books = IBookRepos.GetBookByUserID();
            return Books;
        }
        public string Add(Book book)
        {
           return IBookRepos.Add(book);
        }

        public string Edit(Book book)
        {
           return IBookRepos.Edit(book);
        }

     
        public Book GetBookByID(int id)
        {
            return IBookRepos.GetBookByID(id);
        }

        public bool Delete(int id)
        {
            IBookRepos.Delete(id);
            return true;
        }

        public Book GetBookDetails(int BookID)
        {
            return IBookRepos.GetBookDetails(BookID);
        }
    }
}
