using SimpleBlog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            
            //local variable which is an array of strings which contains the namespace of wherever our post controller exists
            var namespaces = new[] {typeof(PostsController).Namespace};
            //ignore axd logging files
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Defaullt URL routs to thePosts controller
            routes.MapRoute("Home", "", new { controller = "Posts", action = "Index"}, namespaces);

            routes.MapRoute("Login", "login", new { controller = "Auth", action = "Login" }, namespaces);

            /*
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
             * */
        }
    }
}