using Book_Shoppe.DAL;
using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.BL
{
    public interface IBookBL
    {
        IEnumerable<Book> GetBooks();
        string GetGenreByGenreID(int id);
        string AddGenre(Genre genre);
        string DeleteGenre(int id);
        IEnumerable<Book> GetBooksByGenre(int id);
        IEnumerable<Book> SearchResult(string SearchValue);
        IEnumerable<Genre> GetAllGenres();
        IEnumerable<Book> GetUserBooks(int userID);
        string Add(Book book);
        string Edit(Book book);
        Book GetBookByID(int id);
        bool Delete(int id);
        Book GetBookDetails(int BookID);
    }
    public class BookBL : IBookBL
    {
        IBookRepositary IBookRepos = new BookRepositary();

        public IEnumerable<Book> GetBooks()
        {
            IEnumerable<Book> Books = IBookRepos.GetAllBooks();
            return Books;
        }

        // Static Meathod To Get the Genres for the Master page Nav
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
            // Check the Genre is already exists in the list
            IEnumerable<Genre> genres = IBookRepos.GetAllGenres();
            bool duplications = false;
            foreach (Genre item in genres)
            {
                if (item.GenreName==genre.GenreName)
                {
                    duplications = true;
                }
            }
            if (!duplications)
                return IBookRepos.AddGenre(genre);
            return "Duplication Not Allowed In Genre!";
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
        public IEnumerable<Book> GetUserBooks(int userID)
        {
            IEnumerable<Book> Books = IBookRepos.GetBookByUserID(userID);
            return Books;
        }
        public string Add(Book book)
        {  
            // Check the Book Title of the current requested Book from Existing Books
            IEnumerable<Book> Books = IBookRepos.GetAllBooks();
            bool duplications = false;
            foreach (var item in Books)
            {
                if (item.Title == book.Title)
                {
                    duplications = true;
                }
            }
            if (!duplications)
                return IBookRepos.Add(book);
            else
                return "Duplication Not Allowed In Book Title!";
           
        }

        public string Edit(Book book)
        {
            // Check the Book Title of the current requested Book from Existing Books
            IEnumerable<Book> Books = IBookRepos.GetAllBooks();
            bool duplications = false;
            foreach (var item in Books)
            {
                if (item.Title == book.Title)
                {
                    duplications = true;
                }
            }
            if (!duplications)
                return IBookRepos.Edit(book);
            else
                return "Duplication Not Allowed In Book Title!";
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
