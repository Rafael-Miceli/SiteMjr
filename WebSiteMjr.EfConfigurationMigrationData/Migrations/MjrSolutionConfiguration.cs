using FlexProviders.Aspnet;
using FlexProviders.Membership;
using FlexProviders.Roles;
using System.Data.Entity.Migrations;
using System.Linq;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.EfData.DataRepository;

namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    public class MjrSolutionConfiguration : DbMigrationsConfiguration<MjrSolutionContext> 
    {
        public MjrSolutionConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MjrSolutionContext context)
        {
            SeedCompany(context);
            SeedEmployee(context);
            SeedMembership(context);

            base.Seed(context);
        }

        private static void SeedCompany(MjrSolutionContext context)
        {
            context.Companies.AddOrUpdate(co => co.Name, new Company
                {
                    Name = "Mjr Equipamentos eletrônicos LTDA",
                    Email = "mjrtelecom@hotmail.com"
                });

            context.SaveChanges();
        }

        public void SeedEmployee(MjrSolutionContext context)
        {
            context.Employees.AddOrUpdate(em => em.LastName, new Employee
            {
                Name = "Mjr Administrador",
                LastName = " - ",
                Email = "mjrtelecom@hotmail.com",
                Company = context.Companies.FirstOrDefault(n => n.Id == 1)
            });

            context.SaveChanges();
        }

        private static void SeedMembership(MjrSolutionContext context)
        {

            var membership = new FlexMembershipProvider(new MembershipRepository<User>(context), new AspnetEnvironment());
            var roles = new FlexRoleProvider(new RoleRepository<Role, User>(context));

            var firstUser = context.Users.FirstOrDefault(u => u.Username == "mjrtelecom@hotmail.com");
            var firstEmployee = context.Employees.FirstOrDefault(n => n.Email == "mjrtelecom@hotmail.com");

            if (firstUser == null)
            {
                membership.CreateAccount(user: new User
                {
                    Username = "mjrtelecom@hotmail.com",
                    Password = "12345678A",
                    IsLocal = true,
                    Employee = firstEmployee,
                    StatusUser = StatusUser.Active
                });
            }
            else if (firstUser.Employee == null)
            {
                firstUser.Employee = firstEmployee;
                context.SaveChanges();
            }

            if (!roles.RoleExists("MjrAdmin"))
            {
                roles.CreateRole("MjrAdmin");
            }

            if (!roles.RoleExists("CompanyAdmin"))
            {
                roles.CreateRole("CompanyAdmin");
            }

            if (!roles.RoleExists("User"))
            {
                roles.CreateRole("User");
            }

            if (!roles.IsUserInRole("mjrtelecom@hotmail.com", "MjrAdmin"))
            {
                roles.AddUsersToRoles(new[] { "mjrtelecom@hotmail.com" }, new[] { "MjrAdmin" });
            }
        }
    }
}