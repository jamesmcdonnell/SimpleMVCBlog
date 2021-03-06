﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.ViewModels
{
    public class UsersIndex
    {       //This is the type returned by nHibernate (list of users)
        public IEnumerable<User> Users { get; set; } 
    }

    public class UsersNew
    {
        [Required, MaxLength(128)]
        public string Username { get; set; }
        
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(256),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class UsersEdit
    {
        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class UsersResetPassword
    {   //no need for data annotations because this property will not be used to communicate from the view to the controller,
        //it will only be used as data thats being populated from the controller and being presented in the view
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}