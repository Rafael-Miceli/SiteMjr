using FlexProviders.Aspnet;
using FlexProviders.Membership;
using FlexProviders.Roles;
using System.Data.Entity.Migrations;
using System.Linq;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.Domain.services.Membership;
using WebSiteMjr.Domain.services.Roles;
using WebSiteMjr.EfData.DataRepository;
using WebSiteMjr.EfData.UnitOfWork;

namespace WebSiteMjr.EfConfigurationMigrationData.Migrations
{
    public class MjrSolutionConfiguration : DbMigrationsConfiguration<MjrSolutionContext> // CreateDatabaseIfNotExists<MjrSolutionContext>
    {
        public MjrSolutionConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MjrSolutionContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

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

        private void SeedEmployee(MjrSolutionContext context)
        {
            context.Employees.AddOrUpdate(co => co.Name, new Employee
            {
                Name = "Mjr Administrador",
                Email = "mjrtelecom@hotmail.com",
                Company = context.Companies.FirstOrDefault(n => n.Email == "mjrtelecom@hotmail.com")
            });

            context.SaveChanges();
        }

        private static void SeedMembership(MjrSolutionContext context)
        {

            var membership = new FlexMembershipProvider(new MembershipRepository<User>(context), new AspnetEnvironment());
            var roles = new FlexRoleProvider(new RoleRepository<Role, User>(context));

            if (!membership.HasLocalAccount("mjrtelecom@hotmail.com") || context.Users.First().Employee == null)
            {
                Employee firstEmployee = context.Employees.FirstOrDefault(n => n.Email == "mjrtelecom@hotmail.com");
                
                membership.CreateAccount(user: new User
                {
                    Username = "mjrtelecom@hotmail.com",
                    Password = "12345678A",
                    IsLocal = true,
                    Employee = firstEmployee,
                    StatusUser = StatusUser.Active
                });
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