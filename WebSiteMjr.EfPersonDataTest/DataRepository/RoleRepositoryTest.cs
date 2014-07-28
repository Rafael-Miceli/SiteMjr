using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using WebSiteMjr.EfData.DataRepository;

namespace WebSiteMjr.EfPersonDataTest.DataRepository
{
    [TestClass]
    public class RoleRepositoryTest
    {
        [TestMethod]
        public void Sould_GetUsersInRole_Return_Succefully()
        {
            var roleRepository = new RoleRepository<MjrAppRole, User>();

            var result = roleRepository.GetUsersInRole("MjrAdmin");

            Assert.IsNotNull(result);
        }
    }
}
