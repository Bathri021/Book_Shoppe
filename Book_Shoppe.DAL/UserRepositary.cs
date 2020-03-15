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
       void SetCurrentUser(User user);
       User GetCurrentUser();
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
    }

    public class UserRepositary : IUserRepositary
    {
        public static User CurrentUser { get; set; }
        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }

        public User GetCurrentUser()
        {
            return CurrentUser;
        }

        public IEnumerable<User> GetUsers()
        {
            DBContext userDBContext = new DBContext();
            return userDBContext.Users.Where(m=>m.RoleID<=2).ToList();
        }

        public IEnumerable<Role> GetRoles()
        {
            DBContext RoleContext = new DBContext();
            return RoleContext.Roles.Where(m => m.RoleID <=2).ToList();
        }
       
        public string AddUser(User user)
        {
            DBContext _Context = new DBContext();
            _Context.Users.Add(user);
    
                _Context.SaveChanges();
          
            return null;
        }

        public User ValidateLogIn(string userName,string password)
        {
            User _user=null;
            DBContext _Context = new DBContext();
            _user = _Context.Users.Where(u=> u.UserName==userName && u.Password==password).SingleOrDefault();
            return _user;
        }
        public User GetUserByID(int userID)
        {
            DBContext _context = new DBContext();
            return _context.Users.SingleOrDefault(ID => ID.UserID == userID);
        }
        public string EditUser(User user)
        {
            DBContext _context = new DBContext();
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
            using (var Context = new DBContext())
            {
                User user = Context.Users.Where(ID => ID.UserID == id).FirstOrDefault();
                Context.Users.Remove(user);
                Context.SaveChanges();
                return true;
            }
        }

        public string AddToWishList(int userID, int bookID)
        {
            using(DBContext _context = new DBContext())
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

        public IEnumerable<Book> GetUserWishlist(int id)
        {
            List<Book> books = new List<Book>();
            using(DBContext _context = new DBContext())
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
            using(DBContext _context = new DBContext())
            {
              WishList wishlist =  _context.WishList.Where(ID => ID.UserID == userID && ID.BookID == bookID).SingleOrDefault();
                if (wishlist==null)
                    return false;
                return true;
            }
        }
}
}
