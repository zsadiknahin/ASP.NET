using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.EF;

namespace UniversityManagementSystem.Auth
{
    public class AdminAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = (User)httpContext.Session["info"];
            if (user != null && user.Role == "Admin")
            {
                return true;
            }
            return false;
        }
    }
}