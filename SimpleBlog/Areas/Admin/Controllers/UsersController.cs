
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
    }
}
