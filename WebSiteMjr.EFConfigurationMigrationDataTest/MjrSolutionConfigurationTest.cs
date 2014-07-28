using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.EfConfigurationMigrationData;
using WebSiteMjr.EfConfigurationMigrationData.Migrations;

namespace WebSiteMjr.EFConfigurationMigrationDataTest
{
    [TestClass]
    public class MjrSolutionConfigurationTest
    {
        [TestMethod]
        [Ignore]
        public void Should_Seed_Employee_Succefully()
        {
            var mjrSolutionContext = new MjrSolutionContext();
            var mjrSolutionConfiguration = new MjrSolutionConfiguration();

            mjrSolutionConfiguration.SeedEmployee(mjrSolutionContext);
        }

        [TestMethod]
        public void Should_Seed_Membership_Succefully()
        {
            var mjrSolutionContext = new MjrSolutionContext();
            var mjrSolutionConfiguration = new MjrSolutionConfiguration();

            mjrSolutionConfiguration.SeedMembership(mjrSolutionContext);
        }
    }
}
