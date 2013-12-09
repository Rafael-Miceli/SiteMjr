﻿using System.Data.Entity;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;

namespace WebSiteMjr.EfConfigurationMigrationData
{
    public class MjrSolutionContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Stuff> Stuffs { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<StuffCategory> StuffCategories { get; set; }
        public DbSet<StuffManufacture> StuffManufactures { get; set; }

        static MjrSolutionContext()
        {
            Database.SetInitializer<MjrSolutionContext>(null);
        }

        public MjrSolutionContext() : base("DefaultConnection")
        {}
    }
}
