using Book_Shoppe.DAL;
using Book_Shoppe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shoppe.BL
{
    public class UserBL
    {
        private User CurrentUser;
        IUserRepositary IUserRepos = new UserRepositary();

        public User GetCurrentUser()
        {
            return CurrentUser;
        }

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
            IUserRepos.SetCurrentUser(CurrentUser);
        }

        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> Users = IUserRepos.GetUsers();
            return Users;
        }

        public IEnumerable<Role> GetRoles()
        {
            IEnumerable<Role> Roles = IUserRepos.GetRoles();
            return Roles;
        }

        public string AddUser(User user)
        {
            // Check the User Name of the current requested User from Existing Users
            IEnumerable<User> Users = IUserRepos.GetUsers();
            bool duplications=false;
            foreach (var item in Users)
            {
               if( item.UserName == user.UserName || item.MailID == user.MailID)
                {
                    duplications = true;
                } 
            }
            if (!duplications)
                return IUserRepos.AddUser(user);
            else
                return "Duplication Not Allowed In UserName and Email!";
        }

        public User LogIn(string userName,string password)
        {
            return IUserRepos.ValidateLogIn(userName, password);
        }

        public User GetUserByID(int id)
        {
            return IUserRepos.GetUserByID(id);
        }

        public string EditUser(User user)
        {
            return IUserRepos.EditUser(user);
        }

        public bool Delete(int id)
        {
            return IUserRepos.Delete(id);
        }

        public string AddToWishList(int userID, int bookID)
        {
            // Check wheather the Book is already added in the Wishlist
            if (IUserRepos.CheckBookInWishList(userID, bookID))
                return "Book already added in the wishlist";
            return IUserRepos.AddToWishList(userID, bookID);
        }

        public void RemoveBookFormWishlist(int id)
        {
            IUserRepos.RemoveBookFormWishlist(id);
        }

        public IEnumerable<Book> GetUserWishlist(int id)
        {
            return IUserRepos.GetUserWishlist(id);
        }

        public IEnumerable<Book> GetUserOrderList(int id)
        {
            return IUserRepos.GetUserOrderList(id);
        }

        public string AddToOrder(int userID,int bookID)
        {
            if (IUserRepos.CheckBookInOrderList(userID, bookID))
                return "Book already added in the Orders";
            return IUserRepos.AddToOrder(userID, bookID);
        }
    }
}
