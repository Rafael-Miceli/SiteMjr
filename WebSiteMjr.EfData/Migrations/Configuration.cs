using System.Data.Entity.Migrations;
using FlexProviders.Aspnet;
using FlexProviders.Membership;
using FlexProviders.Roles;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.Domain.services.Membership;
using WebSiteMjr.Domain.services.Roles;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository;

namespace WebSiteMjr.EfData.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PersonsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PersonsContext context)
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
            SeedMembership(context);

            base.Seed(context);
        }

        private static void SeedCompany(PersonsContext context)
        {

            context.Company.AddOrUpdate(co => co.Name, new Company
                {
                    Name = "Mjr Equipamentos eletrônicos LTDA",
                    Email = "mjrtelecom@hotmail.com"
                });
        }

        private static void SeedMembership(PersonsContext context)
        {
            //Roles.Enabled = true;

            //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "UserName", true);

            var membership = new MembershipService(new FlexMembershipProvider(new MembershipRepository<User>(context), new AspnetEnvironment()));

            var roles = new RoleService(new FlexRoleProvider(new RoleRepository<Role, User>(context)));  // (SimpleRoleProvider)Roles.Provider;


            if (!membership.HasLocalAccount("mjrtelecom@hotmail.com"))
            {
                membership.CreateAccount(new User
                    {
                        Username = "mjrtelecom@hotmail.com", 
                        Password = "12345678A", 
                        IsLocal = true, 
                        Email = "mjrtelecom@hotmail.com",
                        Name = "Constantino",
                        LastName = "Paiva",
                        IdCompany = 1,
                        StatusUser = StatusUser.Active
                    });
            }

            if (!roles.RoleExists("MjrAdmin"))
            {
                roles.CreateRole("MjrAdmin");
            }

            if (!roles.IsUserInRole("mjrtelecom@hotmail.com", "MjrAdmin"))
            {
                roles.AddUsersToRoles(new[] { "mjrtelecom@hotmail.com" }, new[] { "MjrAdmin" });
            }

        }
    }
}
