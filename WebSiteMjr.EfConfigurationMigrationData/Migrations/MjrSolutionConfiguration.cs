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
            SeedRoles(context);
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

        private static void SeedRoles(MjrSolutionContext context)
        {
            context.Roles.AddOrUpdate(r => r.Name, new Role
            {
                Name = "MjrAdmin"
            });

            context.Roles.AddOrUpdate(r => r.Name, new Role
            {
                Name = "CompanyAdmin"
            });

            context.Roles.AddOrUpdate(r => r.Name, new Role
            {
                Name = "User"
            });
        }

        private static void SeedMembership(MjrSolutionContext context)
        {
            var encoder = new DefaultSecurityEncoder();

            var salt = encoder.GenerateSalt();
            var password = encoder.Encode("12345678A", salt);

            var firstUser = context.Users.FirstOrDefault(u => u.Username == "mjrtelecom@hotmail.com");
            var firstEmployee = context.Employees.FirstOrDefault(n => n.Email == "mjrtelecom@hotmail.com");

            if (firstUser == null)
            {
                context.Users.AddOrUpdate(usr => usr.Username, new User
                {
                    Username = "mjrtelecom@hotmail.com",
                    Password = password,
                    IsLocal = true,
                    Employee = firstEmployee,
                    StatusUser = StatusUser.Active,
                    Roles = context.Roles.Local.Where(r => r.Name == "MjrAdmin").ToList()
                });
            }
            else if (firstUser.Employee == null)
            {
                firstUser.Employee = firstEmployee;
            }

            context.SaveChanges();
        }
    }
}