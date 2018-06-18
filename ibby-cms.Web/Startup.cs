using ibby_cms.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ibby_cms.Startup))]
namespace ibby_cms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesanUsers();
        }

        private void createRolesanUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var a = new CustomRoleStore(context);

            var roleManager = new RoleManager<CustomRole, int>(new CustomRoleStore(context));
            var UserManager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));

            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Admin")) {
                var role = new CustomRole { Name = "Admin", };
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  
                var user = new ApplicationUser();
                user.UserName = "shanu";
                user.Email = "syedshanumcain@gmail.com";

                string userPWD = "123Asd#@12";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded) {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Manager")) {
                var role = new CustomRole { Name = "Manager"};
                roleManager.Create(role);
            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Employee")) {
                var role = new CustomRole { Name = "Employee"};
                roleManager.Create(role);
            }
        }
    }
}
