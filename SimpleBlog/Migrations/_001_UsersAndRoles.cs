using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using FluentMigrator;

namespace SimpleBlog.Migrations
{
    [Migration(1)]  //Fluent migrator uses this to tell versions apart, the name of the class is a convention for the reader.
    public class _001_UsersAndRoles :Migration
    {
        public override void Up() //for adding changes to DB
        {   //Use static members provided by fluentmigrator
            Create.Table("users")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("username").AsString(128)
                .WithColumn("email").AsCustom("VARCHAR(256)") //Note: 265 is the max length on an email ;-)
                .WithColumn("password_hash").AsString(128);

            Create.Table("roles")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("name").AsString(128);

            /* pivot table to - Many to Many association of users <-> roles */

            Create.Table("role_users")                                     //if a user is deleted, delete the row in this table
                .WithColumn("user_id").AsInt32().ForeignKey("users", "id").OnDelete(Rule.Cascade) //user_id can only refer to a valid user id
                .WithColumn("role_id").AsInt32().ForeignKey("roles","id").OnDelete(Rule.Cascade);
        }

        public override void Down() //for rolling back changes to DB
        {
            Delete.Table("role_users");//This comes first so SQL doesn't throw a constraint error
            Delete.Table("users");
            Delete.Table("roles");
        }
    }
}