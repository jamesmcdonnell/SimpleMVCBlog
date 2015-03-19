using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Infrastructure
{
    public class TransactionFilter :IActionFilter
    {   //execuited when the action begins
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Database.Session.BeginTransaction();
        }
        //executed when the action id finished
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //if no exception thrown, commit to DB, else rollback
            if(filterContext.Exception == null)
                Database.Session.Transaction.Commit();
            else
            {
                Database.Session.Transaction.Rollback();
            }
        }
    }
}