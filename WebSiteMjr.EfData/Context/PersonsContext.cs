using System.Data.Entity;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.EfBaseData.Context;

namespace WebSiteMjr.EfData.Context
{
    public class PersonsContext: BaseContext<PersonsContext>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CompanyArea> ToolLocalizations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Holder> Holders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Company>().ToTable("Companies");
            modelBuilder.Entity<CompanyArea>().ToTable("ToolLocalizations");

            modelBuilder.Entity<CompanyArea>()
            .HasMany(i => i.Companies)
            .WithMany(s => s.CompanyAreas)
            .Map(m =>
            {
                m.MapLeftKey("ToolLocalization_Id");
                m.MapRightKey("Company_Id");
                m.ToTable("ToolLocalizationCompanies");
            });
        }
    }
}
