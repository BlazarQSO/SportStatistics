using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using SportStatistics.Models;
using System.Web.Security;

namespace MvcInternetApplication.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {         
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);

                try
                {
                    using (var context = new UsersContext())
                    {
                        if (!context.Database.Exists())
                        {                            
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }
                 
                    WebSecurity.InitializeDatabaseConnection("UsersConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                                      
                    SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
                    SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;

                    if (!roles.RoleExists("Admin"))
                    {
                        roles.CreateRole("Admin");
                    }                   
                    if (!roles.RoleExists("User"))
                    {
                        roles.CreateRole("User");
                    }                   
                    if (membership.GetUser("Admin", false) == null)
                    {
                        membership.CreateUserAndAccount("Admin", "Admin");
                        roles.AddUsersToRoles(new[] { "Admin" }, new[] { "Admin" });
                    }                   
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}