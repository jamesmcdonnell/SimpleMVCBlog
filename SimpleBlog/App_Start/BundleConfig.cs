using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;

namespace SimpleBlog
{
    public class BundleConfig
    {
        //Need a style and a script bundle for both front end and backend
        public static void RegisterBundles(BundleCollection bundles )
        {
                                            // Style Bundles

            bundles.Add(new StyleBundle("~/admin/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/admin.css"));

            bundles.Add(new StyleBundle("~/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/site.css"));

                                            //Script Bundles
            // ensure to call in this order because of dependencies
            bundles.Add(new ScriptBundle("~/admin/scripts")
                .Include("~/scripts/jquery-2.1.3.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive.js")
                .Include("~/scripts/bootsrap.js")
                .Include("~/areas/admin/scripts/Forms.js"));

            bundles.Add(new ScriptBundle("~/scripts")
                .Include("~/scripts/jquery-2.1.3.js")
                .Include("~/scripts/jquery.validate.js")
                .Include("~/scripts/jquery.validate.unobtrusive.js")
                .Include("~/scripts/bootsrap.js"));
        }
    }
}