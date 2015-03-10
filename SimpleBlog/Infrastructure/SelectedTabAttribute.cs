using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SimpleBlog.Infrastructure
{
    //this attribute can only be used on classes and methods
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SelectedTabAttribute : ActionFilterAttribute
    {
        private readonly string _selectedTab;

        public SelectedTabAttribute(string selectedTab)
        {
            _selectedTab = selectedTab;
        }


        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {   //set the viewbag to tell the layout which tab is selected
            filterContext.Controller.ViewBag.SelectedTab = _selectedTab;
        }
    }
}