using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealtimeIndicatorData
{
    public class UserManager
    {
        private readonly IndicatorValuesEntities context;

        /// <summary>
        /// init and open connexion
        /// </summary>
        public UserManager()
        {
            context = new IndicatorValuesEntities();
        }

        /// <summary>
        /// insert user to database
        /// </summary>
        /// <param name="user"></param>
        public void InsertUser(User user)
        {            
            context.Users.AddObject(user);
            context.SaveChanges();            
        }

        /// <summary>
        /// update last login time when user logs in 
        /// </summary>
        /// <param name="user"></param>
        public void UpdateLastLogin(User user)
        {
            var matchedUser = context.Users.First(u => u.username == user.username && u.password == user.password);
            matchedUser.lastLogin = DateTime.Now;
            context.SaveChanges();
        }

        /// <summary>
        /// check with user name and password if user exists 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ExistsUser(User user)
        {
            var matchedUser = context.Users.Where(u => u.username == user.username && u.password == user.password)
                .SingleOrDefault();

            if (matchedUser == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// check with user name if user exists
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool ExistsUser(string userName)
        {
            var matchedUser = context.Users.Where(u => u.username == userName)
                .SingleOrDefault();

            if (matchedUser == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// get user's password
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetPassword(string userName)
        {
            var user = context.Users.Where(u => u.username == userName)
                .FirstOrDefault();

            string password = string.Empty;
            if (user != null)
            {
                password = user.password;
            }

            return password;
        }
    }
}
