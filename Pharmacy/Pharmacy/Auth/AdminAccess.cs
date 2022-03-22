using Pharmacy.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            var authenticated = base.AuthorizeCore(httpContext);
            if (!authenticated && httpContext.Session["userRole"]==null) { return false; }

            //var userEmail = httpContext.User.Identity.Name;
            //PharmacyDBEntities db = new PharmacyDBEntities();
            //var userRole = from u in db.Users
            //           where u.email.Equals(userEmail)
            //           select u.role;
            //if (userRole.Equals("Customer")) return true;
            //else return false;

            if (httpContext.Session["userRole"].ToString().Equals("Admin")){
                return true;
            }
            else return false;

            //return base.AuthorizeCore(httpContext);
        }
    }
}