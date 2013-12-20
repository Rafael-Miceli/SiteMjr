using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services.Stuffs;

namespace WebSiteMjr.Domain.Test.services
{
    [TestClass]
    public class CheckInToolServiceTest
    {
        [TestMethod]
        public void Should_Return_Checkin_By_Employee()
        {
            var employeeName = new Holder
            {
                Id = 1,
                Name = "Celso"
            };
            var checkInToolService = new CheckinToolService(new StubCheckinToolRepository(), null, null, null, null);

            var checkins = checkInToolService.FilterCheckins(employeeName, null, null);

            Assert.AreEqual(0, checkins.Count(c => c.EmployeeCompanyHolderId != employeeName.Id));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Employee_And_Tool()
        {
            var employeeName = new Holder
            {
                Id = 1,
                Name = "Celso"
            };
            var tool = "Ferramenta 1";
            var checkInToolService = new CheckinToolService(new StubCheckinToolRepository(), null, null, null, null);

            var checkins = checkInToolService.FilterCheckins(employeeName, tool, null);

            Assert.AreEqual(0, checkins.Count(c => c.EmployeeCompanyHolderId != employeeName.Id || c.Tool.Name != tool));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Employee_And_Date()
        {
            var employeeName = new Holder
            {
                Id = 1,
                Name = "Celso"
            };
            var date = DateTime.Parse("09/12/2013");
            var checkInToolService = new CheckinToolService(new StubCheckinToolRepository(), null, null, null, null);

            var checkins = checkInToolService.FilterCheckins(employeeName, null, date).ToList();

            Assert.AreNotEqual(0, checkins.Count);
            Assert.AreEqual(checkins.Count, checkins.Count(c => c.EmployeeCompanyHolderId == employeeName.Id && c.CheckinDateTime.Date == date));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Employee_Tool_And_Date()
        {
            var employeeName = new Holder
            {
                Id = 1,
                Name = "Celso"
            };
            var tool = "Ferramenta 1";
            var date = DateTime.Parse("09/12/2013");
            var checkInToolService = new CheckinToolService(new StubCheckinToolRepository(), null, null, null, null);

            var checkins = checkInToolService.FilterCheckins(employeeName, tool, date).ToList();

            Assert.AreNotEqual(0, checkins.Count);
            Assert.AreEqual(checkins.Count, checkins.Count(c => c.EmployeeCompanyHolderId == employeeName.Id && c.Tool.Name == tool && c.CheckinDateTime.Date == date));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Tool()
        {
            var tool = "Ferramenta 1";
            var checkInToolService = new CheckinToolService(new StubCheckinToolRepository(), null, null, null, null);

            var checkins = checkInToolService.FilterCheckins(null, tool, null);

            Assert.AreEqual(0, checkins.Count(c => c.Tool.Name != tool));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Tool_And_Date()
        {
            var tool = "Ferramenta 2";
            var date = DateTime.Parse("10/12/2013");
            var checkInToolService = new CheckinToolService(new StubCheckinToolRepository(), null, null, null, null);

            var checkins = checkInToolService.FilterCheckins(null, tool, date).ToList();

            Assert.AreNotEqual(0, checkins.Count);
            Assert.AreEqual(checkins.Count, checkins.Count(c => c.Tool.Name == tool && c.CheckinDateTime.Date == date));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Date()
        {
            var date = DateTime.Parse("10/12/2013");
            var checkInToolService = new CheckinToolService(new StubCheckinToolRepository(), null, null, null, null);

            var checkins = checkInToolService.FilterCheckins(null, null, date);

            Assert.AreEqual(0, checkins.Count(c => c.CheckinDateTime.Date != date));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Company()
        {
            var companyName = new Holder
            {
                Id = 4,
                Name = "Portoverano"
            };
            var checkInToolService = new CheckinToolService(new StubCheckinToolRepository(), null, null, null, null);

            var checkins = checkInToolService.FilterCheckins(companyName, null, null);

            Assert.AreEqual(0, checkins.Count(c => c.EmployeeCompanyHolderId != companyName.Id));
        }
    }

    public class StubCheckinToolRepository : ICheckinToolRepository
    {
        private IList<CheckinTool> _checkins;
        private IList<Employee> _employees;
        private IList<Company> _companys;

        public StubCheckinToolRepository()
        {
            CreateEmployeesAndCompanys();
            CreateCheckins();
        }

        private void CreateCheckins()
        {
            _checkins = new List<CheckinTool>
            {
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("09/12/2013"),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Celso").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 1"
                    }
                },
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("10/12/2013"),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Lorena").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2"
                    }
                },
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("10/12/2013"),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Celso").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2"
                    }
                },
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("10/12/2013"),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Brendon").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 1"
                    }
                },
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("10/12/2013"),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Brendon").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 3"
                    }
                },
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("11/12/2013"),
                    EmployeeCompanyHolderId = _companys.First(e => e.Name == "Portoverano").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 3"
                    }
                },
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("11/12/2013"),
                    EmployeeCompanyHolderId = _companys.First(e => e.Name == "Portomare").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2"
                    }
                },
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("10/12/2013"),
                    EmployeeCompanyHolderId = _companys.First(e => e.Name == "Portomare").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2"
                    }
                }
            };
        }

        private void CreateEmployeesAndCompanys()
        {
            _employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Celso",
                    LastName = "Gay"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Brendon",
                    LastName = "Gay"
                },
                new Employee
                {
                    Id = 3,
                    Name = "Lorena",
                    LastName = "Gay"
                }
            };

            _companys = new List<Company>
            {
                new Company
                {
                    Id = 4,
                    Name = "Portoverano",
                    Email = "adm@portoverano.com"
                },
                new Company
                {
                    Id = 5,
                    Name = "Portofino",
                    Email = "adm@portofino.com"
                },
                new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                }
            };
        }

        public void Add(CheckinTool entitie)
        {
            throw new NotImplementedException();
        }

        public void Remove(object entitie)
        {
            throw new NotImplementedException();
        }

        public void Update(CheckinTool entitie)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CheckinTool> GetAll()
        {
            return _checkins;
        }

        public IEnumerable<CheckinTool> Query(Func<CheckinTool, bool> filter)
        {
            throw new NotImplementedException();
        }

        public CheckinTool GetById(object identitie)
        {
            throw new NotImplementedException();
        }

        public CheckinTool Get(Expression<Func<CheckinTool, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
