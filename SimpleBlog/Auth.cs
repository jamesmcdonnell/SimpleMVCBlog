/*
 * Makes the currently active user available as an object retrieved
 * from our DB, for use with Roles.
 * We can retreive a fully hydrated nHibernate user entity that represents the 
 * currently logged in user
 */
using System.Linq;
using System.Web;
using NHibernate.Linq;
using SimpleBlog.Models;

namespace SimpleBlog
{
    public static class Auth
    {
        private const string UserKey = "SimpleBlog.Auth.UserKey";

        public static User User
        {
            get
            {    //1)see if he user is logged in
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                
                var user = HttpContext.Current.Items[UserKey] as User;
                if (user == null)
                {           //2)Retrieve their username and use that to get them from the database
                    user =  Database.Session.Query<User>()        
                            .FirstOrDefault((u => u.Username == HttpContext.Current.User.Identity.Name));

                    if (user == null)
                        return null;
                    
                    //3)Caching the user object inside of our Items dictionary
                    HttpContext.Current.Items[UserKey] = user;
                }
             
                return User;
            }         
        }         
    }
}