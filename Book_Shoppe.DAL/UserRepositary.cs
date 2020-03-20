using Book_Shoppe.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.DAL
{
    public interface IUserRepositary
    {
       IEnumerable<User> GetUsers();
       IEnumerable<Role> GetRoles();
       string AddUser(User user);
       User ValidateLogIn(string userName, string password);
       User GetUserByID(int userID);
       string EditUser(User user);
       bool Delete(int id);
       string AddToWishList(int userID,int bookID);
       IEnumerable<Book> GetUserWishlist(int id);
       bool CheckBookInWishList(int userID, int bookID);
       void RemoveBookFormWishlist(int id);
       bool CheckBookInUserCart(int userID, int bookID);
       string AddToCart(int userID, int bookID);
       IEnumerable<Book> GetUserCartDetails(int id);
    }

    public class UserRepositary : IUserRepositary
    {
        public IEnumerable<User> GetUsers()
        {
            BookShoppeDBContext userDBContext = new BookShoppeDBContext();
            return userDBContext.Users.Include("Role").Where(m=>m.RoleID<=2).ToList();
        }

        public IEnumerable<Role> GetRoles()
        {
            BookShoppeDBContext RoleContext = new BookShoppeDBContext();
            return RoleContext.Roles.Where(m => m.RoleName=="Seller"&&m.RoleName=="Customer").ToList();
        }
       
        public string AddUser(User user)
        {
            BookShoppeDBContext _Context = new BookShoppeDBContext();
            _Context.Users.Add(user);
    
                _Context.SaveChanges();
          
            return null;
        }

        public User ValidateLogIn(string userName,string password)
        {
            User _user=null;
            BookShoppeDBContext _Context = new BookShoppeDBContext();
            _user = _Context.Users.Include("Role").Where(u=> u.UserName==userName && u.Password==password).SingleOrDefault();
            return _user;
        }
        public User GetUserByID(int userID)
        {
            BookShoppeDBContext _context = new BookShoppeDBContext();
            return _context.Users.Include("Role").SingleOrDefault(ID => ID.UserID == userID);
        }
        public string EditUser(User user)
        {
            BookShoppeDBContext _context = new BookShoppeDBContext();
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException.InnerException.Message != null)
                {
                    return "The User Name should not be duplicated";
                }
                else
                {
                    return "Please fill out the form correctly and sumbit your values";
                }
            }
            return null;
        }
        public bool Delete(int id)
        {
            using (var Context = new BookShoppeDBContext())
            {
                User user = Context.Users.Where(ID => ID.UserID == id).FirstOrDefault();
                Context.Users.Remove(user);
                Context.SaveChanges();
                return true;
            }
        }

        public string AddToWishList(int userID, int bookID)
        {
            using(BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                WishList wishlist = new WishList()
                {
                    UserID = userID,
                    BookID = bookID
                };
                _context.WishList.Add(wishlist);
                _context.SaveChanges();
                return null;
            }
        }

        public void RemoveBookFormWishlist(int id)
        {
            using(BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                WishList wishlist = _context.WishList.Where(w => w.BookID == id).SingleOrDefault();
                _context.WishList.Remove(wishlist);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Book> GetUserWishlist(int id)
        {
            List<Book> books = new List<Book>();
            using(BookShoppeDBContext _context = new BookShoppeDBContext())
            {
              List<WishList> wishlist =  _context.WishList.Where(ID => ID.UserID == id).ToList();

                foreach (var item in wishlist)
                {
                    Book book = _context.Books.Where(ID => ID.BookID == item.BookID).SingleOrDefault();
                    books.Add(book);
                }
                return books;
            }
        }

        public bool CheckBookInWishList(int userID, int bookID)
        {
            using(BookShoppeDBContext _context = new BookShoppeDBContext())
            {
              WishList wishlist =  _context.WishList.Where(ID => ID.UserID == userID && ID.BookID == bookID).SingleOrDefault();
                if (wishlist==null)
                    return false;
                return true;
            }
        }

        public bool CheckBookInUserCart(int userID, int bookID)
        {
            using (BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                Cart cart = _context.Carts.Where(u => u.UserID == userID).SingleOrDefault();

                if (cart == null)
                {
                    return false; 
                }
                else
                {
                   int cartID = cart.CartID;
                   CartBook cartBook = _context.CartBooks.Where(cb => cb.CartID == cartID && cb.BookID == bookID).SingleOrDefault();
                    if(cartBook==null)
                        return false; // Book is not exist in User Cart
                    return true; // Book is already added in User Cart 
                }
            }
        }

        public string AddToCart(int userID, int bookID)
        {
            using(BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                Cart _cart = _context.Carts.Where(c => c.UserID == userID).SingleOrDefault();

                if (_cart==null)
                {
                    Cart cart = new Cart()
                    {
                        UserID = userID,
                    };
                    _context.Carts.Add(cart);
                    _context.SaveChanges();
                    _cart = _context.Carts.Where(c => c.UserID == userID).SingleOrDefault();
                }


                CartBook cartbook = new CartBook()
                {
                     CartID = _cart.CartID,
                     BookID = bookID,
                };
                _context.CartBooks.Add(cartbook);
                _context.SaveChanges();
                return null;
            }
        }

        public IEnumerable<Book> GetUserCartDetails(int id)
        {
            List<Book> books = new List<Book>();
            using (BookShoppeDBContext _context = new BookShoppeDBContext())
            {
                try
                {
                    Cart cart = new Cart();
                    cart = _context.Carts.Where(ID => ID.UserID == id).SingleOrDefault();

                    int cartID = cart.CartID;

                    List<CartBook> cartBooks = _context.CartBooks.Where(cb => cb.CartID == cartID).ToList();

                    foreach (var item in cartBooks)
                    {
                        Book book = _context.Books.Where(ID => ID.BookID == item.BookID).SingleOrDefault();
                        books.Add(book);
                    }
                    return books;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
}
}
