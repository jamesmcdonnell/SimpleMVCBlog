using System.Web.WebPages;
using SimpleBlog.ViewModels;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View(new AuthLogin
            {
             //Test = "This is my test value set in my control"  
            });
        }

       [HttpPost]
        public ActionResult Login(AuthLogin form)
       {
           if (!ModelState.IsValid)
           {
               return View(form);
           }// form.Test = "This is a value set in my post action";

           if (form.Username != "correct name")
           {
               ModelState.AddModelError("Username", "Username not quite right!");
               return View(form);
           }
           return Content("The form is Valid!");
       }
    }
}
