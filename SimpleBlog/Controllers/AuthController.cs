using System.Web.Security;
using System.Web.WebPages;
using SimpleBlog.ViewModels;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        //On Logout, remove authentication token and redirect to home page
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }
        
        
        public ActionResult Login()
        {
            return View(new AuthLogin
            {
             //Test = "This is my test value set in my control"  
            });
        }

       [HttpPost]
        public ActionResult Login(AuthLogin form, string returnURL)
       {
           if (!ModelState.IsValid)
           {
               return View(form);
           }

           //create persistant cookie for authentication
           //all encryption and hashing is handled by asp.net
           FormsAuthentication.SetAuthCookie(form.Username,true);

           //if return url exists in the query string redirect the user to that url
           // This code will be updated to be database driven later
           if (!string.IsNullOrWhiteSpace((returnURL)))
               return Redirect(returnURL);

           return RedirectToRoute("home");
       }
    }
}
