﻿/*
 * Used by nHibernate and is automatically hydrated with the information in the database
 * Has properties for each of the columns in the DB table row
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace SimpleBlog.Models
{
    public class User
    {

        private const int WorkFactor = 13;
        //This is used to prevent timing attacks when the user is null
        public static void FakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword("", WorkFactor);
        }


        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }

        public virtual void SetPassword(string password)
        {   //Use BCrypt to hash the password with a work factor of 13
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }
        // checks if the password entered into the form is the same as the one stored in the DB
        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }

    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("users");
                //param1 tells the mapping which property constitutes our primary key, param2 tells nhibernate that this 
                //key is automatically incremented by the DB.
            Id(x => x.Id, x => x.Generator(Generators.Identity)); //let the DB handle the generation of the unoque id
                //Name of property to be mapped, param2 optional, override default settings and make not nullable
                //Note, field name not explicitly defined, but it defaults to the property name (Username), and SQL in not case sensitive to it works
            Property(x => x.Username, x => x.NotNullable((true)));
            Property(x => x.Email, x => x.NotNullable(true));

            //Property(x => x.PasswordHash, x => x.Column("password_hash"));
            //Need multiple lambda to override cloum name, as naming convention is different in this case
            Property(x=> x.PasswordHash, x =>
            {
                x.Column("password_hash");
                x.NotNullable(true);
            });
        }
    }
}