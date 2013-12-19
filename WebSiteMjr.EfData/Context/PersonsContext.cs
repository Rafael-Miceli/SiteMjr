using System.ComponentModel.DataAnnotations.Schema;
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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Holder> Holders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Company>().ToTable("Companies");
        }
    }
}
