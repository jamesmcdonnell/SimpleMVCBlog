using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]  // Locks down controller form those who are not admin
    public class PostsController : Controller
    {
        public ActionResult Index()
        {
            return Content("Admin Posts!");
        }

    }
}