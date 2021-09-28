﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ThokchenNato.DatabaseContext;
using ThokchenNato.Models.Data;

namespace ThokchenNato
{
    public class MvcApplication : System.Web.HttpApplication
    {
        ThokchenNatoDbContext db = new ThokchenNatoDbContext();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest()
        {
            // Check if user is logged in
            if (User == null) { return; }

            // Get username
            string username = Context.User.Identity.Name;

                // Declare array of roles
                string[] roles = null;


                // Populate roles
                User dto = db.users.FirstOrDefault(x => x.Username == username);

                roles = db.userRoles.Where(x => x.UserId == dto.Id).Select(x => x.Role.Name).ToArray();


                // Build IPrincipal object
                IIdentity userIdentity = new GenericIdentity(username);
                IPrincipal newUserObj = new GenericPrincipal(userIdentity, roles);

                // Update Context.User
                Context.User = newUserObj;


        }
    }
}
