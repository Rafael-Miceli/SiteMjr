using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services.Stuffs;

namespace WebSiteMjr.Domain.Test
{
    [TestClass]
    public class CheckInToolServiceTest
    {
        [TestMethod]
        public void Should_Return_Checkin_By_Employee()
        {
            var employeeName = "Celso";
            var checkInToolService = new CheckinToolService(new StubCheckinToolRepository(), null);

            var checkinsByEmplyee = checkInToolService.FilterCheckins(employeeName, null, null);

            Assert.AreEqual(0, checkinsByEmplyee.Count(c => c.Employee.Name != employeeName));
        }

    }

    public class StubCheckinToolRepository : ICheckinToolRepository
    {
        private IList<CheckinTool> _checkins;
        private IList<Employee> _employees;

        public StubCheckinToolRepository()
        {
            CreateEmployees();
            CreateCheckins();
        }

        private void CreateCheckins()
        {
            _checkins = new List<CheckinTool>
            {
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("09/12/2013"),
                    Employee = _employees.First(e => e.Name == "Celso"),
                    Tool = new Tool
                    {
                        Name = "Ferramenta 1"
                    }
                },
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("10/12/2013"),
                    Employee = _employees.First(e => e.Name == "Lorena"),
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2"
                    }
                },
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("10/12/2013"),
                    Employee = _employees.First(e => e.Name == "Celso"),
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2"
                    }
                },
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("10/12/2013"),
                    Employee = _employees.First(e => e.Name == "Brendon"),
                    Tool = new Tool
                    {
                        Name = "Ferramenta 1"
                    }
                },
                new CheckinTool
                {
                    CheckinDateTime = DateTime.Parse("10/12/2013"),
                    Employee = _employees.First(e => e.Name == "Brendon"),
                    Tool = new Tool
                    {
                        Name = "Ferramenta 3"
                    }
                }
            };
        }

        private void CreateEmployees()
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
