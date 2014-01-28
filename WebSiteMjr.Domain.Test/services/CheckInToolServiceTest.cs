﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.Domain.services.Stuffs;
using Moq;
using WebSiteMjr.Domain.Test.Model;

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
            var checkInToolService = new CheckinToolService(new FakeCheckinToolRepository(), null, null);

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
            var checkInToolService = new CheckinToolService(new FakeCheckinToolRepository(), null, null);

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
            var checkInToolService = new CheckinToolService(new FakeCheckinToolRepository(), null, null);

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
            var checkInToolService = new CheckinToolService(new FakeCheckinToolRepository(), null, null);

            var checkins = checkInToolService.FilterCheckins(employeeName, tool, date).ToList();

            Assert.AreNotEqual(0, checkins.Count);
            Assert.AreEqual(checkins.Count, checkins.Count(c => c.EmployeeCompanyHolderId == employeeName.Id && c.Tool.Name == tool && c.CheckinDateTime.Date == date));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Tool_Name()
        {
            var tool = "Ferramenta 1";
            var checkInToolService = new CheckinToolService(new FakeCheckinToolRepository(), null, null);

            var checkins = checkInToolService.FilterCheckins(null, tool, null);

            Assert.AreEqual(0, checkins.Count(c => c.Tool.Name != tool));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Tool_Id()
        {
            //Arrange
            const int toolId = 1;
            var checkInToolService = new CheckinToolService(new FakeCheckinToolRepository(), null, null);

            //Act
            var checkins = checkInToolService.ListCheckinToolsWithActualTool(toolId);

            //Assert
            var checkinsList = checkins != null ? checkins.ToList() : null;
            Assert.IsNotNull(checkinsList);
            Assert.AreEqual(0, checkinsList.Count(c => c.Tool.Id != toolId));
            Assert.AreEqual(checkinsList.Count(), checkinsList.Count(c => c.Tool.Id == toolId));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Tool_And_Date()
        {
            var tool = "Ferramenta 2";
            var date = DateTime.Parse("10/12/2013");
            var checkInToolService = new CheckinToolService(new FakeCheckinToolRepository(), null, null);

            var checkins = checkInToolService.FilterCheckins(null, tool, date).ToList();

            Assert.AreNotEqual(0, checkins.Count);
            Assert.AreEqual(checkins.Count, checkins.Count(c => c.Tool.Name == tool && c.CheckinDateTime.Date == date));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Date()
        {
            var date = DateTime.Parse("10/12/2013");
            var checkInToolService = new CheckinToolService(new FakeCheckinToolRepository(), null, null);

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
            var checkInToolService = new CheckinToolService(new FakeCheckinToolRepository(), null, null);

            var checkins = checkInToolService.FilterCheckins(companyName, null, null);

            Assert.AreEqual(0, checkins.Count(c => c.EmployeeCompanyHolderId != companyName.Id));
        }

        [TestMethod]
        public void Should_Create_First_Checkin_For_Tool_With_Company()
        {
            //Arrange
            var company = CompanyDummies.CreateOneCompany();
            var newCheckinTool = CheckinToolDummies.CreateOneCheckinTool();

            var checkinToolRepositoryMock = new Mock<ICheckinToolRepository>();
            var companyServiceMock = new Mock<ICompanyService>();

            checkinToolRepositoryMock.Setup(x => x.Add(newCheckinTool));
            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(company.Id))).Returns(company);

            var checkinToolService = new CheckinToolService(checkinToolRepositoryMock.Object, new StubUnitOfWork(),
                companyServiceMock.Object);

            //Act
            checkinToolService.CheckinTool(newCheckinTool);

            //Assert
            checkinToolRepositoryMock.VerifyAll();
            //companyServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Create_First_Checkin_For_Tool_With_Employee()
        {
            //Arrange
            var employee = EmployeeDummies.CreateOneEmployee();
            var newCheckinTool = CheckinToolDummies.CreateOneCheckinTool();

            var checkinToolRepositoryMock = new Mock<ICheckinToolRepository>();
            var companyServiceMock = new Mock<ICompanyService>();

            checkinToolRepositoryMock.Setup(x => x.Add(newCheckinTool));
            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(employee.Id)));

            var checkinToolService = new CheckinToolService(checkinToolRepositoryMock.Object, new StubUnitOfWork(),
                companyServiceMock.Object);

            //Act
            checkinToolService.CheckinTool(newCheckinTool);

            //Assert
            checkinToolRepositoryMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Should_Not_Create_Checkin_For_The_Same_Employee_Twice_Then()
        {
            //Arrange
            var employee = new Employee
            {
                Id = 2,
                Name = "Brendon",
                LastName = "Gay"
            };

            var newCheckin = new CheckinTool
            {
                Id = 9,
                CheckinDateTime = new DateTime(2014, 12, 10, 14, 22, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool
                {
                    Name = "Ferramenta 1",
                    Id = 1
                }
            };

            var companyServiceMock = new Mock<ICompanyService>();

            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(employee.Id)));

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(),
                companyServiceMock.Object); 

            //Act
            checkinToolService.CheckinTool(newCheckin);

            //Assert
            Assert.IsNull(checkinToolService.FindToolCheckin(newCheckin.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Should_Not_Create_Checkin_For_The_Same_Employee_Twice_Then_Updating_The_Date()
        {
            //Arrange
            var employee = new Employee
            {
                Id = 2,
                Name = "Brendon",
                LastName = "Gay"
            };

            var newCheckin = new CheckinTool
            {
                Id = 1,
                CheckinDateTime = new DateTime(2014, 12, 10, 14, 22, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool
                {
                    Name = "Ferramenta 1",
                    Id = 1
                }
            };

            var companyServiceMock = new Mock<ICompanyService>();

            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(employee.Id)));

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(),
                companyServiceMock.Object);

            //Act
            checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.AreNotEqual(newCheckin.EmployeeCompanyHolderId, checkinToolService.FindToolCheckin(newCheckin.Id).EmployeeCompanyHolderId);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectExistsException<CheckinTool>))]
        public void Should_Not_Create_Checkin_When_The_CheckinDateTime_Of_This_Tool_Is_Equal_To_A_CheckinDateTime_Existing_Of_the_Same_Tool()
        {
            //Arrange
            var newCheckin = new CheckinTool
            {
                Id = 10,
                CheckinDateTime = new DateTime(2013, 12, 10, 15, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            var companyServiceMock = new Mock<ICompanyService>();

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            //Act
            checkinToolService.CheckinTool(newCheckin);

            //Assert
            Assert.IsNull(checkinToolService.FindToolCheckin(newCheckin.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectExistsException<CheckinTool>))]
        public void Should_Not_Update_Checkin_When_The_CheckinDateTime_Of_This_Tool_Is_Equal_To_A_CheckinDateTime_Existing_Of_the_Same_Tool()
        {
            //Arrange
            var company = new Company
            {
                Id = 4,
                Name = "Portoverano",
                Email = "adm@portoverano.com"
            };

            var newCheckin = new CheckinTool
            {
                Id = 6,
                CheckinDateTime = new DateTime(2013, 12, 11, 11, 02, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            var companyServiceMock = new Mock<ICompanyService>();

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            //Act
            checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.AreNotEqual(company.Id, checkinToolService.FindToolCheckin(newCheckin.Id).EmployeeCompanyHolderId);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinInconsistencyException))]
        public void Should_Not_Update_Checkin_When_Change_The_CheckinDateTime_Of_This_Tool_Create_Inconsistency_Wtih_Chekins_Between_Companies()
        {
            //Arrange
            var company = new Company
            {
                Id = 4,
                Name = "Portoverano",
                Email = "adm@portoverano.com"
            };

            var newCheckin = new CheckinTool
            {
                Id = 6,
                CheckinDateTime = new DateTime(2014, 1, 21, 17, 15, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            var companyServiceMock = new Mock<ICompanyService>();
            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 5, 6)))
                .Returns(() => company)
                .Callback(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                });

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            var originalCheckin = checkinToolService.FindToolCheckin(newCheckin.Id);
            var checkinBeforeThis = checkinToolService.FindToolCheckin(4);
            var checkinAfterThis = checkinToolService.FindToolCheckin(7);


            //Act
            checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.AreEqual(originalCheckin.CheckinDateTime, checkinToolService.FindToolCheckin(newCheckin.Id).CheckinDateTime);
            Assert.IsTrue(checkinToolService.IsCheckinOfThisToolInCompany(checkinBeforeThis.EmployeeCompanyHolderId) && checkinToolService.IsCheckinOfThisToolInCompany(checkinAfterThis.EmployeeCompanyHolderId));
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinInconsistencyException))]
        public void Should_Update_Checkin_When_Change_The_Checkin_Of_This_Tool_Dont_Create_Inconsistency_Wtih_Chekins_Between_Employees()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();

            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 5, 6)))
                .Returns(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                });

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            var originalCheckin = new CheckinTool
            {
                Id = 4,
                CheckinDateTime = new DateTime(2013, 12, 10, 15, 32, 00),//DateTime.Parse("10/12/2013"),
                EmployeeCompanyHolderId = 6,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                },
                CompanyAreaId = 1
            };

            var checkinBeforeThis = checkinToolService.FindToolCheckin(2);
            var checkinAfterThis = checkinToolService.FindToolCheckin(6);
            var newCheckin = checkinToolService.FindToolCheckin(4);
            newCheckin.EmployeeCompanyHolderId = 1;

            //Act
            checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.AreNotEqual(originalCheckin.EmployeeCompanyHolderId, checkinToolService.FindToolCheckin(newCheckin.Id).EmployeeCompanyHolderId);
            Assert.IsFalse(checkinBeforeThis.EmployeeCompanyHolderId == newCheckin.EmployeeCompanyHolderId || checkinAfterThis.EmployeeCompanyHolderId == newCheckin.EmployeeCompanyHolderId);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinCompanyToCompanyException))]
        public void Should_Not_Create_Checkin_In_Company_When_The_Last_Checkin_Of_This_Tool_Was_In_A_Company()
        {
            //Arrange
            var company = new Company
            {
                Id = 4,
                Name = "Portoverano",
                Email = "adm@portoverano.com"
            };

            var newCheckin = new CheckinTool
            {
                Id = 10,
                CheckinDateTime = new DateTime(2014, 1, 21, 17, 15, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            var companyServiceMock = new Mock<ICompanyService>();
            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 6)))
                .Returns(() => company)
                .Callback(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                });

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            //Act
            checkinToolService.CheckinTool(newCheckin);

            //Assert
            Assert.IsNull(checkinToolService.FindToolCheckin(newCheckin.Id));
            companyServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinInconsistencyException))]
        public void Should_Not_Update_Checkin_In_Company_When_The_Last_Checkin_Of_This_Tool_Was_In_A_Company()
        {
            //Arrange
            var company = new Company
            {
                Id = 4,
                Name = "Portoverano",
                Email = "adm@portoverano.com"
            };

            var newCheckin = new CheckinTool
            {
                Id = 6,
                CheckinDateTime = new DateTime(2013, 12, 10, 17, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            var companyServiceMock = new Mock<ICompanyService>();
            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 6)))
                .Returns(() => company)
                .Callback(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                });

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            //Act
            checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.AreNotEqual(company.Id, checkinToolService.FindToolCheckin(newCheckin.Id).EmployeeCompanyHolderId);
            companyServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Update_Checkin_In_Company_When_The_Last_Checkin_Of_This_Tool_Was_Not_In_A_Company()
        {
            //Arrange
            var company = new Company
            {
                Id = 4,
                Name = "Portoverano",
                Email = "adm@portoverano.com"
            };

            var newCheckin = new CheckinTool
            {
                Id = 4,
                CheckinDateTime = new DateTime(2013, 12, 10, 15, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            var companyServiceMock = new Mock<ICompanyService>();
            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 6)))
                .Returns(() => company)
                .Callback(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                });

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            //Act
            checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.AreEqual(company.Id, checkinToolService.FindToolCheckin(newCheckin.Id).EmployeeCompanyHolderId);
            companyServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Create_Checkin_In_Company_When_The_Last_Checkin_Of_This_Tool_Was_Not_In_A_Company()
        {
            //Arrange
            var company = new Company
            {
                Id = 4,
                Name = "Portoverano",
                Email = "adm@portoverano.com"
            };

            var newCheckin = new CheckinTool
            {
                Id = 10,
                CheckinDateTime = new DateTime(2014, 1, 21, 17, 15, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool { Id = 1, Name = "Ferramenta 1" }
            };

            var companyServiceMock = new Mock<ICompanyService>();
            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 6)))
                .Returns(() => company)
                .Callback(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                });

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            //Act
            checkinToolService.CheckinTool(newCheckin);

            //Assert
            Assert.IsNotNull(checkinToolService.FindToolCheckin(newCheckin.Id));
            Assert.AreEqual(company.Id, checkinToolService.FindToolCheckin(newCheckin.Id).EmployeeCompanyHolderId);
            Assert.AreEqual(newCheckin.Tool.Name, checkinToolService.FindToolCheckin(newCheckin.Id).Tool.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinCompanyToCompanyException))]
        public void Should_Not_Create_Checkin_In_Company_When_The_Last_Checkin_Of_This_Tool_Was_In_A_Company_And_DateTime_Is_Between_Existing_ones()
        {
            //Arrange
            var company = new Company
            {
                Id = 4,
                Name = "Portoverano",
                Email = "adm@portoverano.com"
            };

            var newCheckin = new CheckinTool
            {
                Id = 10,
                CheckinDateTime = new DateTime(2013, 12, 10, 15, 35, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            var companyServiceMock = new Mock<ICompanyService>();
            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 6)))
                .Returns(() => company)
                .Callback(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                });

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            //Act
            checkinToolService.CheckinTool(newCheckin);

            //Assert
            Assert.IsNull(checkinToolService.FindToolCheckin(newCheckin.Id));
            companyServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Create_Checkin_In_Company_When_The_Last_Checkin_Of_This_Tool_Was_Not_A_Company()
        {
            //Arrange
            var company = new Company
            {
                Id = 6,
                Name = "Portoverano",
                Email = "adm@portoverano.com"
            };

            var employee = new Employee
            {
                Id = 2,
                Name = "Brendon"
            };

            var newCheckin = new CheckinTool
            {
                Id = 10,
                CheckinDateTime = new DateTime(2014, 1, 21, 17, 15, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            var companyServiceMock = new Mock<ICompanyService>();
            companyServiceMock.Setup(x => x.FindCompany(It.IsNotIn(2))).Returns(company);

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            //Act
            checkinToolService.CheckinTool(newCheckin);

            //Assert
            Assert.IsNotNull(checkinToolService.FindToolCheckin(newCheckin.Id));
            Assert.AreEqual(employee.Id, checkinToolService.FindToolCheckin(newCheckin.Id).EmployeeCompanyHolderId);
            Assert.AreEqual(newCheckin.Tool.Name, checkinToolService.FindToolCheckin(newCheckin.Id).Tool.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinInconsistencyException))]
        public void Should_Not_Delete_Checkin_When_Change_The_CheckinDateTime_Of_This_Tool_Create_Inconsistency_Wtih_Chekins_Between_Companies()
        {
            //Arrange
            var company = new Company
            {
                Id = 4,
                Name = "Portoverano",
                Email = "adm@portoverano.com"
            };

            var newCheckin = new CheckinTool
            {
                Id = 6,
                CheckinDateTime = new DateTime(2014, 1, 21, 17, 15, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            var companyServiceMock = new Mock<ICompanyService>();
            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 5, 6)))
                .Returns(() => company)
                .Callback(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                });

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            var originalCheckin = checkinToolService.FindToolCheckin(newCheckin.Id);
            var checkinBeforeThis = checkinToolService.FindToolCheckin(4);
            var checkinAfterThis = checkinToolService.FindToolCheckin(7);


            //Act
            checkinToolService.DeleteToolCheckin(newCheckin.Id);

            //Assert
            Assert.IsNotNull(checkinToolService.FindToolCheckin(newCheckin.Id));
            Assert.AreEqual(originalCheckin.CheckinDateTime, checkinToolService.FindToolCheckin(newCheckin.Id).CheckinDateTime);
            Assert.IsTrue(checkinToolService.IsCheckinOfThisToolInCompany(checkinBeforeThis.EmployeeCompanyHolderId) && checkinToolService.IsCheckinOfThisToolInCompany(checkinAfterThis.EmployeeCompanyHolderId));
        }

        [TestMethod]
        public void Should_Ignore_CompanyArea_When_Creating_A_Checkin_In_Employee()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();

            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 5, 6)))
                .Returns(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                });

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            var newCheckin = checkinToolService.FindToolCheckin(1);
            newCheckin.Id = 10;
            newCheckin.CompanyAreaId = 2;

            //Act
            checkinToolService.CheckinTool(newCheckin);

            //Assert
            Assert.IsNull(checkinToolService.FindToolCheckin(newCheckin.Id).CompanyAreaId);
        }

        [TestMethod]
        public void Should_Ignore_CompanyArea_When_Updating_Checkin_To_An_Employee()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();

            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 5, 6)))
                .Returns(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                });

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            var newCheckin = new CheckinTool
            {
                Id = 4,
                CheckinDateTime = new DateTime(2013, 12, 10, 15, 32, 00),//DateTime.Parse("10/12/2013"),
                EmployeeCompanyHolderId = 6,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                },
                CompanyAreaId = 1
            };
            newCheckin.EmployeeCompanyHolderId = 2;

            //Act
            checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.IsNull(checkinToolService.FindToolCheckin(newCheckin.Id).CompanyAreaId);
        }

        [TestMethod]
        public void Should_Ignore_CompanyArea_When_Updating_To_A_CompanyArea_That_Dont_Exists_In_Company()
        {
            //Arrange
            var companyAreas = new Collection<CompanyArea>
            {
                new CompanyArea
                {
                    Id = 2,
                    Name = "Portão de visitantes"
                }
            };

            var companyServiceMock = new Mock<ICompanyService>();

            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 5, 6)))
                .Returns(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com",
                    CompanyAreas = new Collection<CompanyArea>
                    {
                        new CompanyArea
                        {
                            Id = 2,
                            Name = "Portão de visitantes"
                        }
                    }
                });

            companyServiceMock.Setup(x => x.FindCompanyCompanyAreas(It.Is<string>(str => str == "Portomare")))
                .Returns(companyAreas);

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            var newCheckin = new CheckinTool
            {
                Id = 4,
                CheckinDateTime = new DateTime(2013, 12, 10, 15, 32, 00),//DateTime.Parse("10/12/2013"),
                EmployeeCompanyHolderId = 6,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                },
                CompanyAreaId = 1
            };

            //Act
            checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.IsNull(checkinToolService.FindToolCheckin(newCheckin.Id).CompanyAreaId);
        }

        [TestMethod]
        public void Should_Update_Checkin_CompanyArea_When_CompanyArea_Not_Null_And_In_A_Company()
        {
            //Arrange
            var companyAreas = new Collection<CompanyArea>
            {
                new CompanyArea
                {
                    Id = 1,
                    Name = "Portão de visitantes"
                }
            };

            var companyServiceMock = new Mock<ICompanyService>();

            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 5, 6)))
                .Returns(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com",
                    CompanyAreas = new Collection<CompanyArea>
                    {
                        new CompanyArea
                        {
                            Id = 2,
                            Name = "Portão de visitantes"
                        }
                    }
                });

            companyServiceMock.Setup(x => x.FindCompanyCompanyAreas(It.Is<string>(str => str == "Portomare")))
                .Returns(companyAreas);

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            var newCheckin = new CheckinTool
                {
                    Id = 30,
                    CheckinDateTime = new DateTime(2013, 12, 09, 12, 32, 00),//DateTime.Parse("10/12/2013"),
                    EmployeeCompanyHolderId = 2,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    }
                };
            newCheckin.EmployeeCompanyHolderId = 6;
            newCheckin.CompanyAreaId = 1;

            //Act
            checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.IsNotNull(checkinToolService.FindToolCheckin(newCheckin.Id).CompanyAreaId);
        }

        [TestMethod]
        public void Should_Update_Checkin_CompanyArea_When_CompanyArea_Is_Null_And_In_A_Company()
        {
            //Arrange
            var companyServiceMock = new Mock<ICompanyService>();

            companyServiceMock.Setup(x => x.FindCompany(It.IsIn(4, 5, 6)))
                .Returns(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                });

            var checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), companyServiceMock.Object);

            var newCheckin = new CheckinTool
            {
                Id = 30,
                CheckinDateTime = new DateTime(2013, 12, 09, 12, 32, 00),//DateTime.Parse("10/12/2013"),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };
            newCheckin.EmployeeCompanyHolderId = 6;

            //Act
            checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.IsNotNull(checkinToolService.FindToolCheckin(newCheckin.Id));
            Assert.AreEqual(newCheckin.EmployeeCompanyHolderId, checkinToolService.FindToolCheckin(newCheckin.Id).EmployeeCompanyHolderId);
            Assert.IsNull(checkinToolService.FindToolCheckin(newCheckin.Id).CompanyAreaId);
        }
    }


    public class FakeCheckinToolRepository : ICheckinToolRepository
    {
        private IList<CheckinTool> _checkins;
        private IList<Employee> _employees;
        private IList<Company> _companys;

        public bool Created { get; set; }

        public FakeCheckinToolRepository()
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
                    Id = 1,
                    CheckinDateTime = new DateTime(2013, 12, 09, 12, 32, 00), //DateTime.Parse("09/12/2013"),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Celso").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 1",
                        Id = 1
                    }
                },
                new CheckinTool
                {
                    Id = 3,
                    CheckinDateTime = new DateTime(2013, 12, 10, 14, 22, 00),//DateTime.Parse("10/12/2013"),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Brendon").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 1",
                        Id = 1
                    }
                },
                new CheckinTool
                {
                    Id = 30,
                    CheckinDateTime = new DateTime(2013, 12, 09, 12, 32, 00),//DateTime.Parse("10/12/2013"),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Brendon").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    }
                },
                new CheckinTool
                {
                    Id = 2,
                    CheckinDateTime = new DateTime(2013, 12, 10, 12, 32, 00),//DateTime.Parse("10/12/2013"),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Lorena").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    }
                },
                new CheckinTool
                {
                    Id = 4,
                    CheckinDateTime = new DateTime(2013, 12, 10, 15, 32, 00),//DateTime.Parse("10/12/2013"),
                    EmployeeCompanyHolderId = _companys.First(e => e.Name == "Portomare").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    },
                    CompanyAreaId = 1
                },
                new CheckinTool
                {
                    Id = 6,
                    CheckinDateTime = new DateTime(2013, 12, 10, 17, 32, 00),//DateTime.Parse("10/12/2013"),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Celso").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    }
                },
                new CheckinTool
                {
                    Id = 7,
                    CheckinDateTime = new DateTime(2013, 12, 11, 11, 02, 00),//DateTime.Parse("11/12/2013"),
                    EmployeeCompanyHolderId = _companys.First(e => e.Name == "Portomare").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 2",
                        Id = 2
                    }
                },
                new CheckinTool
                {
                    Id = 8,
                    CheckinDateTime = new DateTime(2013, 12, 11, 12, 32, 00),//DateTime.Parse("11/12/2013"),
                    EmployeeCompanyHolderId = _companys.First(e => e.Name == "Portoverano").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 3",
                        Id = 3
                    }
                },
                new CheckinTool
                {
                    Id = 5,
                    CheckinDateTime = new DateTime(2013, 12, 10, 16, 32, 00),//DateTime.Parse("10/12/2013"),
                    EmployeeCompanyHolderId = _employees.First(e => e.Name == "Brendon").Id,
                    Tool = new Tool
                    {
                        Name = "Ferramenta 3",
                        Id = 3
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
                    Email = "adm@portoverano.com",
                    CompanyAreas = new Collection<CompanyArea>
                    {
                        new CompanyArea
                        {
                            Id = 2,
                            Name = "Portão de visitantes"
                        },
                        new CompanyArea
                        {
                            Id = 1,
                            Name = "Portão"
                        }
                    }
                },
                new Company
                {
                    Id = 5,
                    Name = "Portofino",
                    Email = "adm@portofino.com",
                    CompanyAreas = new Collection<CompanyArea>
                    {
                        new CompanyArea
                        {
                            Id = 1,
                            Name = "Portão"
                        }
                    }
                },
                new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com",
                    CompanyAreas = new Collection<CompanyArea>
                    {
                        new CompanyArea
                        {
                            Id = 2,
                            Name = "Portão de visitantes"
                        }
                    }
                }
            };
        }

        public void Add(CheckinTool entitie)
        {
            _checkins.Add(entitie);
        }

        public void Remove(object entitie)
        {
            _checkins.Remove(_checkins.FirstOrDefault(c => c.Id == (int) entitie));
        }

        public void Update(CheckinTool entitie)
        {
            var checkinToUpdate = _checkins.FirstOrDefault(c => c.Id == entitie.Id);
            if (checkinToUpdate == null) return;
            checkinToUpdate.EmployeeCompanyHolderId = entitie.EmployeeCompanyHolderId;
            checkinToUpdate.CheckinDateTime = entitie.CheckinDateTime;
            checkinToUpdate.Tool = entitie.Tool;
        }

        public IEnumerable<CheckinTool> GetAll()
        {
            return _checkins;
        }

        public IEnumerable<CheckinTool> Query(Func<CheckinTool, bool> filter)
        {
            return _checkins.Where(filter);
        }

        public CheckinTool GetById(object identitie)
        {
            return _checkins.FirstOrDefault(c => c.Id == (int) identitie);
        }

        public CheckinTool Get(Expression<Func<CheckinTool, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
