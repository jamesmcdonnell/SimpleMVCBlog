using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.ViewModels
{
    public class UsersIndex
    {       //This is the type returned by nHibernate (list of users)
        public IEnumerable<User> Users { get; set; } 
    }
}