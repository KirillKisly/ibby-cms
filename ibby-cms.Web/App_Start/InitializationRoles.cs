using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ibby_cms.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ibby_cms.App_Start {
    public static class InitializationRoles {
        public static void CreateRolesanUsers() {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<CustomRole, int>(new CustomRoleStore(context));
            var UserManager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));

            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Admin")) {
                var role = new CustomRole { Name = "Admin", };
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  
                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@admin.com";

                string userPWD = "14324FdsgASfVcx$#%JGHDNXH23";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded) {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // Creating User role    
            if (!roleManager.RoleExists("User")) {
                var role = new CustomRole { Name = "User" };
                roleManager.Create(role);
            }
        }
    }
}