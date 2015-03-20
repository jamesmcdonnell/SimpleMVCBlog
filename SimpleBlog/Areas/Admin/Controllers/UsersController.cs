
using System.Linq;
using System.Web.Mvc;
using NHibernate.Linq;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]  // Locks down controller from those who are not admin
    [SelectedTab("users")]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {    //instanciate user view model and pass it into the view
            return View(new UsersIndex
            {       //retrieves every user in our DB and returns them as a clooection of user objects
                Users = Database.Session.Query<User>().ToList()
            });
        }

        public ActionResult New()
        {
            return View(new UsersNew
            {
                
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(UsersNew form)
        {
            if(Database.Session.Query<User>().Any(u => u.Username == form.Username))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            //if we passed validation and the username is unique
            //Create a new User and copy the data in
            var user = new User
            {
                Email = form.Email,
                 Username = form.Username,
            };

            user.SetPassword(form.Password);
            
            //Now that user is set up - save to database
            Database.Session.Save(user);
            
            return RedirectToAction("index");
        }
        //edit user with a given id
        public ActionResult Edit(int id)  
        {
        //retreive the user from the DB
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();
            
            return View(new UsersEdit
            {
                Username = user.Username,
            Email = user.Email
            } );
        }// end Action Edit()

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UsersEdit form)
        {
            //retreive the user from the DB
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();
            //If we find a username that is already taken in the database, except the current one,
            //then show the error
            if(Database.Session.Query<User>().Any(u => u.Username == form.Username && u.Id != id)) 
            ModelState.AddModelError("Username", "Username must be unique");

            //if model not valid return view
            if(!ModelState.IsValid)
                return View(form);

            //Populate form fields to DB
            user.Username = form.Username;
            user.Email = form.Email;
                Database.Session.Update(user);

            //lastly redirect the index action
            return RedirectToAction("index");
        }

        public ActionResult ResetPassword(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            return View(new UsersResetPassword
            {
                Username = user.Username
            });
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int id, UsersResetPassword form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();
            
            //Views username property is not populated by the form, but by the controller
            //so both need to set the property or it won't apear on the view
            form.Username = user.Username;
            
            if (!ModelState.IsValid)
                return View(form);

            
            user.SetPassword(form.Password);
            Database.Session.Update(user);

            //lastly redirect the index action
            return RedirectToAction("index");
        }

        //Action for Delete Link
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return Content("you are in the Delete ActionResult");//HttpNotFound();

            //if user exists delete it
            Database.Session.Delete(user);

            return RedirectToAction("index");
        }
    }
}
