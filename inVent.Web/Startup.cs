using inVent.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(inVent.Web.Startup))]
namespace inVent.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (!roleManager.RoleExists("Admin"))
            {
                // Create Admin Role
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Create Super User called "Admin"
                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "admin@admin.com";

                string userPWD = "Baking1!";

                var chkUser = UserManager.Create(user, userPWD);
                
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
                
            }
            if (!roleManager.RoleExists("Sales Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Sales Manager";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Salesman"))
            {
                var role = new IdentityRole();
                role.Name = "Salesman";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("President"))
            {
                var role = new IdentityRole();
                role.Name = "President";
                roleManager.Create(role);
            }
        }
    }
}
