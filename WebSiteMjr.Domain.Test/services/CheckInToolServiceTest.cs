using System;
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
        private CheckinToolService _checkinToolService;
        private Mock<ICompanyService> _companyServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            var company = new Company
            {
                Id = 4,
                Name = "Portoverano",
                Email = "adm@portoverano.com"
            };

            _companyServiceMock = new Mock<ICompanyService>();
            _companyServiceMock.Setup(x => x.ExistsCheckinOfToolInCompany(It.IsIn(4, 6)))
                .Returns(() => company)
                .Callback(() => new Company
                {
                    Id = 6,
                    Name = "Portomare",
                    Email = "adm@portomare.com"
                });

            _checkinToolService = new CheckinToolService(new FakeCheckinToolRepository(), new StubUnitOfWork(), _companyServiceMock.Object);
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Employee()
        {
            var employeeName = new Holder
            {
                Id = 1,
                Name = "Celso"
            };

            var checkins = _checkinToolService.FilterCheckins(employeeName, null, null);

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
            const string tool = "Ferramenta 1";


            var checkins = _checkinToolService.FilterCheckins(employeeName, tool, null);

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

            var checkins = _checkinToolService.FilterCheckins(employeeName, null, date).ToList();

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

            var checkins = _checkinToolService.FilterCheckins(employeeName, tool, date).ToList();

            Assert.AreNotEqual(0, checkins.Count);
            Assert.AreEqual(checkins.Count, checkins.Count(c => c.EmployeeCompanyHolderId == employeeName.Id && c.Tool.Name == tool && c.CheckinDateTime.Date == date));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Tool_Name()
        {
            var tool = "Ferramenta 1";

            var checkins = _checkinToolService.FilterCheckins(null, tool, null);

            Assert.AreEqual(0, checkins.Count(c => c.Tool.Name != tool));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Tool_Id()
        {
            //Arrange
            const int toolId = 1;

            //Act
            var checkins = _checkinToolService.ListCheckinToolsWithActualTool(toolId);

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


            var checkins = _checkinToolService.FilterCheckins(null, tool, date).ToList();


            Assert.AreNotEqual(0, checkins.Count);
            Assert.AreEqual(checkins.Count, checkins.Count(c => c.Tool.Name == tool && c.CheckinDateTime.Date == date));
        }

        [TestMethod]
        public void Should_Return_Checkin_By_Date()
        {
            var date = DateTime.Parse("10/12/2013");

            var checkins = _checkinToolService.FilterCheckins(null, null, date);

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

            var checkins = _checkinToolService.FilterCheckins(companyName, null, null);

            Assert.AreEqual(0, checkins.Count(c => c.EmployeeCompanyHolderId != companyName.Id));
        }

        [TestMethod]
        public void Should_Create_First_Checkin_For_Tool_With_Company()
        {
            var newCheckinTool = CheckinToolDummies.CreateOneCheckinTool();

            
            _checkinToolService.CheckinTool(newCheckinTool);

            
            var checkin = _checkinToolService.FindToolCheckin(newCheckinTool.Id);
            Assert.IsNotNull(checkin);
        }

        [TestMethod]
        public void Should_Create_First_Checkin_For_Tool_With_Employee()
        {
            var newCheckinTool = CheckinToolDummies.CreateOneCheckinTool();

            
            _checkinToolService.CheckinTool(newCheckinTool);

            
            var checkin = _checkinToolService.FindToolCheckin(newCheckinTool.Id);
            Assert.IsNotNull(checkin);
        }

        [TestMethod]
        public void Should_Create_Checkin_For_Tool_With_Informer_Field_Different_From_Null()
        {
            var newCheckinTool = CheckinToolDummies.CreateOneCheckinToolWithInformer();


            _checkinToolService.CheckinTool(newCheckinTool);


            var checkin = _checkinToolService.FindToolCheckin(newCheckinTool.Id);
            Assert.IsNotNull(checkin);
            Assert.IsNotNull(checkin.Informer);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_A_Update_In_A_CheckinTool_When_CanCheckinToolBetweenCompanies_Is_True_And_Checkin_Update_Is_Creating_Same_Holder_Twice_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

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

            //Act
            _checkinToolService.CheckinTool(newCheckin);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_A_Update_In_A_CheckinTool_When_CanCheckinToolBetweenCompanies_Is_False_And_Checkin_Update_Is_Creating_Same_Holder_Twice_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

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

            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectExistsException<CheckinTool>))]
        public void Should_Not_Create_Checkin_When_The_CheckinDateTime_Of_This_Tool_Is_Equal_To_A_CheckinDateTime_Existing_Of_the_Same_Tool_And_CanCheckinToolBetweenCompanies_Is_False_Should_Rise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var newCheckin = new CheckinTool
            {
                Id = 10,
                CheckinDateTime = new DateTime(2013, 12, 10, 15, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            //Act
            _checkinToolService.CheckinTool(newCheckin);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectExistsException<CheckinTool>))]
        public void Should_Not_Create_Checkin_When_The_CheckinDateTime_Of_This_Tool_Is_Equal_To_A_CheckinDateTime_Existing_Of_the_Same_Tool_And_CanCheckinToolBetweenCompanies_Is_True_Should_Rise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var newCheckin = new CheckinTool
            {
                Id = 10,
                CheckinDateTime = new DateTime(2013, 12, 10, 15, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            //Act
            _checkinToolService.CheckinTool(newCheckin);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectExistsException<CheckinTool>))]
        public void Should_Not_Update_Checkin_When_The_CheckinDateTime_Of_This_Tool_Is_Equal_To_A_CheckinDateTime_Existing_Of_the_Same_Tool_And_CanCheckinToolBetweenCompanies_Is_False_Should_Rise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

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
                EmployeeCompanyHolderId = company.Id,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };


            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.AreNotEqual(company.Id, _checkinToolService.FindToolCheckin(newCheckin.Id).EmployeeCompanyHolderId);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectExistsException<CheckinTool>))]
        public void Should_Not_Update_Checkin_When_The_CheckinDateTime_Of_This_Tool_Is_Equal_To_A_CheckinDateTime_Existing_Of_the_Same_Tool_And_CanCheckinToolBetweenCompanies_Is_True_Should_Rise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

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

            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.AreNotEqual(company.Id, _checkinToolService.FindToolCheckin(newCheckin.Id).EmployeeCompanyHolderId);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_Checkin_Of_A_Tool_When_Changing_Only_The_CheckinDateTime_Of_This_Checkin_Is_Creating_Inconsistency_Wtih_Chekins_Twice_And_CanCheckinToolBetweenCompanies_Is_False_Then_Should_Rise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var newCheckin = new CheckinTool
            {
                Id = 6,
                CheckinDateTime = new DateTime(2014, 1, 21, 17, 15, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            
            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_Checkin_Of_A_Tool_When_Changing_Only_The_CheckinDateTime_Of_This_Checkin_Is_Creating_Inconsistency_Wtih_Chekins_Twice_And_CanCheckinToolBetweenCompanies_Is_True_Then_Should_Rise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var newCheckin = new CheckinTool
            {
                Id = 6,
                CheckinDateTime = new DateTime(2014, 1, 21, 17, 15, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Should_Not_Update_Checkin_When_Changing_The_Holder_Is_Creating_Inconsistency_Wtih_Chekins_Between_Holders_And_CanCheckinToolBetweenCompanies_Is_False_Then_Should_Rise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var newCheckin = new CheckinTool
            {
                Id = 4,
                CheckinDateTime = new DateTime(2013, 12, 10, 15, 32, 00),
                EmployeeCompanyHolderId = 1,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                },
                CompanyAreaId = 1
            };

            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Should_Not_Update_Checkin_When_Changing_The_Holder_Is_Creating_Inconsistency_Wtih_Chekins_Between_Holders_And_CanCheckinToolBetweenCompanies_Is_True_Then_Should_Rise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var newCheckin = new CheckinTool
            {
                Id = 4,
                CheckinDateTime = new DateTime(2013, 12, 10, 15, 32, 00),
                EmployeeCompanyHolderId = 1,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                },
                CompanyAreaId = 1
            };

            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinCompanyToCompanyException))]
        public void Given_A_Checkin_Of_A_Tool_When_CanCheckinToolBetweenCompanies_Is_False_And_The_Last_Checkin_Of_This_Tool_Was_In_A_Company_Then_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;
            
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
                EmployeeCompanyHolderId = company.Id,
                Tool = new Tool
                {

                    Id = 2,
                    Name = "Ferramenta 2"
                }
            };


            //Act
            _checkinToolService.CheckinTool(newCheckin);


            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Given_A_Checkin_Of_A_Tool_When_CanCheckinToolBetweenCompanies_Is_True_And_The_Last_Checkin_Of_This_Tool_Was_In_A_Company_Then_Create_Checkin()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;
            
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
                EmployeeCompanyHolderId = company.Id,
                Tool = new Tool
                {
                    Id = 2,
                    Name = "Ferramenta 2"
                }
            };

            //Act
            _checkinToolService.CheckinTool(newCheckin);


            //Assert
            Assert.IsNotNull(_checkinToolService.FindToolCheckin(newCheckin.Id));
            _companyServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinCompanyToCompanyException))]
        public void Given_A_Checkin_Of_A_Tool_When_CanCheckinToolBetweenCompanies_Is_False_And_The_Checkins_Between_This_Tool_Will_Create_Inconsistency_Between_Companies_Then_Should_Not_Update_Checkin() 
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var newCheckin = new CheckinTool
            {
                Id = 6,
                CheckinDateTime = new DateTime(2013, 12, 10, 17, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Given_A_Checkin_Of_A_Tool_In_A_Company_When_CanCheckinToolBetweenCompanies_Is_True_And_The_Last_Checkin_Of_This_Tool_Was_In_A_Company_Then_Should_Update_Checkin_In_Company()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

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
                EmployeeCompanyHolderId = company.Id,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };
            
            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.AreEqual(company.Id, _checkinToolService.FindToolCheckin(newCheckin.Id).EmployeeCompanyHolderId);
        }

        [TestMethod]
        public void Given_An_Update_Of_A_Checkin_Of_A_Tool_When_CanCheckinToolBetweenCompanies_Is_False_And_The_Last_Checkin_Of_This_Tool_Was_Not_In_A_Company_Then_Should_Update_Checkin_In_Company()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

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

            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.AreEqual(company.Id, _checkinToolService.FindToolCheckin(newCheckin.Id).EmployeeCompanyHolderId);
            _companyServiceMock.VerifyAll();
        }

        [TestMethod]
        public void Should_Create_Checkin_In_Company_When_The_Last_Checkin_Of_This_Tool_Was_Not_In_A_Company_And_CanCheckinToolBetweenCompanies_Is_False()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

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

            //Act
            _checkinToolService.CheckinTool(newCheckin);

            //Assert
            var checkin = _checkinToolService.FindToolCheckin(newCheckin.Id);

            Assert.IsNotNull(checkin);
            Assert.AreEqual(company.Id, checkin.EmployeeCompanyHolderId);
            Assert.AreEqual(newCheckin.Tool.Name, checkin.Tool.Name);
        }

        [TestMethod]
        public void Should_Create_Checkin_In_Company_When_The_Last_Checkin_Of_This_Tool_Was_Not_In_A_Company_And_CanCheckinToolBetweenCompanies_Is_True()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

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

            //Act
            _checkinToolService.CheckinTool(newCheckin);

            //Assert
            var checkin = _checkinToolService.FindToolCheckin(newCheckin.Id);

            Assert.IsNotNull(checkin);
            Assert.AreEqual(company.Id, checkin.EmployeeCompanyHolderId);
            Assert.AreEqual(newCheckin.Tool.Name, checkin.Tool.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinCompanyToCompanyException))]
        public void Given_The_Creation_Of_A_Checkin_Of_A_Tool_When_CanCheckinToolBetweenCompanies_Is_False_And_The_Checkins_Between_This_Tool_Will_Create_Inconsistency_Between_Companies_Or_Identical_Holders_Then_Should_Not_Create_Checkin()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var newCheckin = new CheckinTool
            {
                Id = 10,
                CheckinDateTime = new DateTime(2013, 12, 10, 15, 35, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            //Act
            _checkinToolService.CheckinTool(newCheckin);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Should_Create_Checkin_In_Company_When_The_Last_Checkin_Of_This_Tool_Was_Not_A_Company()
        {
            //Arrange
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

            //Act
            _checkinToolService.CheckinTool(newCheckin);

            //Assert
            var checkin = _checkinToolService.FindToolCheckin(newCheckin.Id);

            Assert.IsNotNull(checkin);
            Assert.AreEqual(employee.Id, checkin.EmployeeCompanyHolderId);
            Assert.AreEqual(newCheckin.Tool.Name, checkin.Tool.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_A_Checkin_Of_A_Tool_When_CanCheckinToolBetweenCompanies_Is_False_And_The_Checkins_Between_This_Tool_Will_Create_Inconsistency_Between_Companies_And_Will_Create_Identical_Holders_Then_Should_Not_Delete_Checkin()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToDelete = new CheckinTool
            {
                Id = 6,
                CheckinDateTime = new DateTime(2014, 1, 21, 17, 15, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            //Act
            _checkinToolService.DeleteToolCheckin(checkinToDelete.Id);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_A_Checkin_Of_A_Tool_When_CanCheckinToolBetweenCompanies_Is_True_And_The_Checkins_Between_This_Tool_Will_Not_Create_Inconsistency_Between_Companies_But_Will_Create_Identical_Holders_Then_Should_Not_Delete_Checkin()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToDelete = new CheckinTool
            {
                Id = 6,
                CheckinDateTime = new DateTime(2014, 1, 21, 17, 15, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            //Act
            _checkinToolService.DeleteToolCheckin(checkinToDelete.Id);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Given_The_Delete_Of_A_Checkin_Of_A_Tool_When_CanCheckinToolBetweenCompanies_Is_True_And_The_Checkins_Between_This_Tool_Will_Not_Creating_Inconsistency_Between_Identical_Holders_Then_Should_Delete_Checkin()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToDelete = new CheckinTool
            {
                Id = 40,
                CheckinDateTime = new DateTime(2014, 1, 21, 17, 15, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };
            
            //Act
            _checkinToolService.DeleteToolCheckin(checkinToDelete.Id);

            //Assert
            Assert.IsNull(_checkinToolService.FindToolCheckin(checkinToDelete.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinCompanyToCompanyException))]
        public void Given_The_Delete_Of_A_Checkin_Of_A_Tool_When_CanCheckinToolBetweenCompanies_Is_False_And_The_Checkins_Between_This_Tool_Will_Not_Creating_Inconsistency_Between_Identical_Holders_But_Will_Create_Inconsistency_Between_Companies_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToDelete = new CheckinTool
            {
                Id = 40,
                CheckinDateTime = new DateTime(2014, 1, 21, 17, 15, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool { Id = 2, Name = "Ferramenta 2" }
            };

            //Act
            _checkinToolService.DeleteToolCheckin(checkinToDelete.Id);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinCompanyToCompanyException))]
        public void Given_The_Creation_Of_A_Checkin_When_This_Checkin_Is_For_A_Company_And_Checkin_After_That_One_Is_In_Company_And_CanCheckinToolBetweenCompanies_Is_False_Then_Should_Raise_Exeption()
        {
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var newCheckin = new CheckinTool
            {
                Id = 27,
                CheckinDateTime = new DateTime(2013, 12, 10, 13, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            _checkinToolService.CheckinTool(newCheckin);

            Assert.Fail();
        }

        [TestMethod]
        public void Should_Ignore_CompanyArea_When_Creating_A_Checkin_In_Employee()
        {
            //Arrange
            var newCheckin = _checkinToolService.FindToolCheckin(1);
            newCheckin.Id = 10;
            newCheckin.CompanyAreaId = 2;

            //Act
            _checkinToolService.CheckinTool(newCheckin);

            //Assert
            Assert.IsNull(_checkinToolService.FindToolCheckin(newCheckin.Id).CompanyAreaId);
        }

        [TestMethod]
        public void Should_Ignore_CompanyArea_When_Updating_Checkin_To_An_Employee()
        {
            //Arrange
            var newCheckin = new CheckinTool
            {
                Id = 4,
                CheckinDateTime = new DateTime(2013, 12, 10, 15, 32, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                },
                CompanyAreaId = 1
            };

            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.IsNull(_checkinToolService.FindToolCheckin(newCheckin.Id).CompanyAreaId);
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

            _companyServiceMock.Setup(x => x.FindCompanyCompanyAreas(It.Is<string>(str => str == "Portomare")))
                .Returns(companyAreas);
            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            Assert.IsNull(_checkinToolService.FindToolCheckin(newCheckin.Id).CompanyAreaId);
        }

        [TestMethod]
        public void Should_Update_Checkin_CompanyArea_When_CompanyArea_Not_Null_And_In_A_Company()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var companyAreas = new Collection<CompanyArea>
            {
                new CompanyArea
                {
                    Id = 1,
                    Name = "Portão de visitantes"
                }
            };

            var companyServiceMock = new Mock<ICompanyService>();
            companyServiceMock.Setup(x => x.ExistsCheckinOfToolInCompany(It.IsIn(4, 6)))
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
                    CheckinDateTime = new DateTime(2013, 12, 09, 12, 32, 00),
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
            var newCheckin = new CheckinTool
            {
                Id = 30,
                CheckinDateTime = new DateTime(2013, 12, 09, 12, 32, 00),
                EmployeeCompanyHolderId = 6,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(newCheckin);

            //Assert
            var checkin = _checkinToolService.FindToolCheckin(newCheckin.Id);

            Assert.IsNotNull(checkin);
            Assert.AreEqual(newCheckin.EmployeeCompanyHolderId, checkin.EmployeeCompanyHolderId);
            Assert.IsNull(checkin.CompanyAreaId);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_The_ToolBeforeChange_Was_The_First_And_CanCheckinToolBetweenCompanies_Is_False_And_Creating_Inconsistency_Between_Holders_Then_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToUpdate = new CheckinTool
            {
                Id = 8,
                CheckinDateTime = new DateTime(2013, 12, 16, 11, 02, 00), 
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_The_ToolBeforeChange_Was_The_First_And_CanCheckinToolBetweenCompanies_Is_True_And_Creating_Inconsistency_Between_Holders_Then_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 8,
                CheckinDateTime = new DateTime(2013, 12, 16, 11, 02, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_The_ToolBeforeChange_Was_The_Last_And_CanCheckinToolBetweenCompanies_Is_False_And_Creating_Inconsistency_Between_Holders_Then_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToUpdate = new CheckinTool
            {
                Id = 5,
                CheckinDateTime = new DateTime(2013, 12, 16, 11, 02, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_The_ToolBeforeChange_Was_The_Last_And_CanCheckinToolBetweenCompanies_Is_True_And_Creating_Inconsistency_Between_Holders_Then_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 5,
                CheckinDateTime = new DateTime(2013, 12, 16, 11, 02, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_The_ToolBeforeChange_Was_The_Only_And_CanCheckinToolBetweenCompanies_Is_False_And_Is_Creating_Inconsistency_Between_Holders_Then_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToUpdate = new CheckinTool
            {
                Id = 33,
                CheckinDateTime = new DateTime(2013, 12, 11, 11, 02, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool
                {
                    Name = "Ferramenta 3",
                    Id = 3
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }
        
        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_The_ToolBeforeChange_Is_Creating_Inconsistency_Between_Holders_And_CanCheckinToolBetweenCompanies_Is_False_And_Then_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToUpdate = new CheckinTool
            {
                Id = 7,
                CheckinDateTime = new DateTime(2013, 12, 10, 11, 02, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool
                {
                    Name = "Ferramenta 3",
                    Id = 3
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_The_ToolBeforeChange_Is_Creating_Inconsistency_Between_Holders_And_CanCheckinToolBetweenCompanies_Is_True_And_Then_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 7,
                CheckinDateTime = new DateTime(2013, 12, 10, 11, 02, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool
                {
                    Name = "Ferramenta 3",
                    Id = 3
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinCompanyToCompanyException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_The_ToolBeforeChange_Is_Creating_Inconsistency_Between_Companies_And_CanCheckinToolBetweenCompanies_Is_False_And_Then_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToUpdate = new CheckinTool
            {
                Id = 40,
                CheckinDateTime = new DateTime(2013, 12, 10, 11, 02, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool
                {
                    Name = "Ferramenta 3",
                    Id = 3
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_The_ToolBeforeChange_Is_Creating_Inconsistency_Between_Companies_But_CanCheckinToolBetweenCompanies_Is_True_And_Then_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 40,
                CheckinDateTime = new DateTime(2013, 12, 10, 11, 02, 00),
                EmployeeCompanyHolderId = 1,
                Tool = new Tool
                {
                    Name = "Ferramenta 3",
                    Id = 3
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            var checkin = _checkinToolService.FindToolCheckin(checkinToUpdate.Id);

            Assert.IsNotNull(checkin);
            Assert.AreEqual(checkin.Tool.Id, checkinToUpdate.Tool.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_The_ToolBeforeChange_Was_The_Only_And_CanCheckinToolBetweenCompanies_Is_True_And_Is_Creating_Inconsistency_Between_Holders_Then_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 33,
                CheckinDateTime = new DateTime(2013, 12, 11, 11, 02, 00),
                EmployeeCompanyHolderId = 2,
                Tool = new Tool
                {
                    Name = "Ferramenta 3",
                    Id = 3
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_The_ToolChanged_Had_No_Checkins_And_CanCheckinToolBetweenCompanies_Is_False_And_Is_Not_Creating_Inconsistency_Between_Holders_Then_Should_Update_CheckinTool()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToUpdate = new CheckinTool
            {
                Id = 41,
                CheckinDateTime = new DateTime(2013, 12, 16, 11, 02, 00),
                EmployeeCompanyHolderId = 1,
                Tool = new Tool
                {
                    Name = "Ferramenta 5",
                    Id = 5
                }
            };
            

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            var checkinUpdated = _checkinToolService.FindToolCheckin(checkinToUpdate.Id);

            Assert.IsNotNull(checkinUpdated);
            Assert.AreEqual(checkinUpdated.Tool.Id, checkinToUpdate.Tool.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinCompanyToCompanyException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_CanCheckinToolBetweenCompanies_Is_False_And_Is_Creating_Inconsistency_Between_Companies_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToUpdate = new CheckinTool
            {
                Id = 5,
                CheckinDateTime = new DateTime(2013, 12, 16, 11, 02, 00),
                EmployeeCompanyHolderId = 6,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };


            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Changed_And_CanCheckinToolBetweenCompanies_Is_True_And_Is_Creating_Inconsistency_Between_Companies_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 5,
                CheckinDateTime = new DateTime(2013, 12, 16, 11, 02, 00),
                EmployeeCompanyHolderId = 6,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };


            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            var checkin = _checkinToolService.FindToolCheckin(checkinToUpdate.Id);

            Assert.IsNotNull(checkin);
            Assert.AreEqual(checkin.Tool.Id, checkinToUpdate.Tool.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectExistsException<CheckinTool>))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Not_Changed_And_The_Tool_Has_Changed_And_CanCheckinToolBetweenCompanies_Is_True_And_Is_Same_CheckinDateTime_Of_Another_Checkin_In_The_Same_Tool_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 33,
                CheckinDateTime = new DateTime(2013, 12, 10, 16, 32, 00),
                EmployeeCompanyHolderId = 6,
                Tool = new Tool
                {
                    Name = "Ferramenta 3",
                    Id = 3
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Not_Changed_And_The_Tool_Has_Changed_And_CanCheckinToolBetweenCompanies_Is_True_And_Is_Not_Creating_Inconsistency_Between_Holders_Then_Should_Update_Checkin()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 5,
                CheckinDateTime = new DateTime(2013, 12, 10, 16, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            var checkin = _checkinToolService.FindToolCheckin(checkinToUpdate.Id);

            Assert.IsNotNull(checkin);
            Assert.AreEqual(checkin.Tool.Id, checkinToUpdate.Tool.Id);
        }

        [TestMethod]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Not_Changed_And_CanCheckinToolBetweenCompanies_Is_True_And_Is_Not_Creating_Inconsistency_Between_Holders_Then_Should_Update_Checkin()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 42,
                CheckinDateTime = new DateTime(2013, 12, 10, 16, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            var checkin = _checkinToolService.FindToolCheckin(checkinToUpdate.Id);

            Assert.IsNotNull(checkin);
            Assert.AreEqual(checkin.Tool.Id, checkinToUpdate.Tool.Id);
        }

        [TestMethod]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Not_Changed_And_CanCheckinToolBetweenCompanies_Is_True_And_Is_The_Only_Checkin_In_Tool_Then_Should_Update_Checkin()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 33,
                CheckinDateTime = new DateTime(2013, 12, 11, 16, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 4",
                    Id = 4
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            var checkin = _checkinToolService.FindToolCheckin(checkinToUpdate.Id);

            Assert.IsNotNull(checkin);
            Assert.AreEqual(checkin.Tool.Id, checkinToUpdate.Tool.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinCompanyToCompanyException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Not_Changed_And_CanCheckinToolBetweenCompanies_Is_False_And_Is_Creating_Inconsistency_Between_Companies_After_Change_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToUpdate = new CheckinTool
            {
                Id = 2,
                CheckinDateTime = new DateTime(2013, 12, 10, 16, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinCompanyToCompanyException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Not_Changed_And_CanCheckinToolBetweenCompanies_Is_False_And_Is_Creating_Inconsistency_Between_Companies_Before_Change_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToUpdate = new CheckinTool
            {
                Id = 40,
                CheckinDateTime = new DateTime(2013, 12, 10, 16, 32, 00),
                EmployeeCompanyHolderId = 3,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Not_Changed_And_The_Position_Between_Other_Checkins_Has_Not_Changed_And_CanCheckinToolBetweenCompanies_Is_True_And_Is_Not_Creating_Inconsistency_Between_Holders_Then_Should_Update_Checkin()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 6,
                CheckinDateTime = new DateTime(2013, 12, 10, 16, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            var checkin = _checkinToolService.FindToolCheckin(checkinToUpdate.Id);

            Assert.IsNotNull(checkin);
            Assert.AreEqual(checkin.Tool.Id, checkinToUpdate.Tool.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinCompanyToCompanyException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Not_Changed_And_The_Position_Between_Other_Checkins_Has_Not_Changed_And_CanCheckinToolBetweenCompanies_Is_False_And_Is_Creating_Inconsistency_Between_Companies_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToUpdate = new CheckinTool
            {
                Id = 6,
                CheckinDateTime = new DateTime(2013, 12, 10, 16, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Not_Changed_And_The_Position_Between_Other_Checkins_Has_Not_Changed_And_CanCheckinToolBetweenCompanies_Is_False_And_Is_Creating_Inconsistency_Between_Holders_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = false;

            var checkinToUpdate = new CheckinTool
            {
                Id = 4,
                CheckinDateTime = new DateTime(2013, 12, 10, 16, 32, 00),
                EmployeeCompanyHolderId = 1,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Not_Changed_And_The_Position_Between_Other_Checkins_Has_Not_Changed_And_CanCheckinToolBetweenCompanies_Is_True_And_Is_Creating_Inconsistency_Between_Holders_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 4,
                CheckinDateTime = new DateTime(2013, 12, 10, 16, 32, 00),
                EmployeeCompanyHolderId = 3,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Not_Changed_And_The_Position_Between_Other_Checkins_Has_Not_Changed_And_CanCheckinToolBetweenCompanies_Is_True_And_Is_Creating_Inconsistency_Between_Holders_And_Is_First_Checkin_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 30,
                CheckinDateTime = new DateTime(2013, 12, 09, 15, 32, 00),
                EmployeeCompanyHolderId = 3,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Not_Changed_And_The_Position_Between_Other_Checkins_Has_Not_Changed_And_CanCheckinToolBetweenCompanies_Is_True_And_Is_Not_Creating_Inconsistency_Between_Holders_And_Is_First_Checkin_Then_Should_Update_Checkin()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 30,
                CheckinDateTime = new DateTime(2013, 12, 09, 15, 32, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            var checkin = _checkinToolService.FindToolCheckin(checkinToUpdate.Id);

            Assert.IsNotNull(checkin);
            Assert.AreEqual(checkin.EmployeeCompanyHolderId, checkinToUpdate.EmployeeCompanyHolderId);
        }

        [TestMethod]
        [ExpectedException(typeof(CheckinHolderTwiceThenException))]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Not_Changed_And_The_Position_Between_Other_Checkins_Has_Not_Changed_And_CanCheckinToolBetweenCompanies_Is_True_And_Is_Creating_Inconsistency_Between_Holders_And_Is_Last_Checkin_Then_Should_Raise_Exception()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 42,
                CheckinDateTime = new DateTime(2013, 12, 20, 11, 02, 00),
                EmployeeCompanyHolderId = 4,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Given_An_Update_To_A_CheckinTool_When_CheckinDateTime_Has_Changed_And_The_Tool_Has_Not_Changed_And_The_Position_Between_Other_Checkins_Has_Not_Changed_And_CanCheckinToolBetweenCompanies_Is_True_And_Is_Not_Creating_Inconsistency_Between_Holders_And_Is_Last_Checkin_Then_Should_Update_Checkin()
        {
            //Arrange
            MjrSettings.Default.CanCheckinToolBetweenCompanies = true;

            var checkinToUpdate = new CheckinTool
            {
                Id = 42,
                CheckinDateTime = new DateTime(2013, 12, 20, 11, 02, 00),
                EmployeeCompanyHolderId = 1,
                Tool = new Tool
                {
                    Name = "Ferramenta 2",
                    Id = 2
                }
            };

            //Act
            _checkinToolService.UpdateToolCheckin(checkinToUpdate);

            //Assert
            var checkin = _checkinToolService.FindToolCheckin(checkinToUpdate.Id);

            Assert.IsNotNull(checkin.Id);
            Assert.AreEqual(checkin.EmployeeCompanyHolderId, checkinToUpdate.EmployeeCompanyHolderId);
        }

    }


    public class FakeCheckinToolRepository : ICheckinToolRepository
    {
        private readonly IList<CheckinTool> _checkins;
        

        public bool Created { get; set; }

        public FakeCheckinToolRepository()
        {
            _checkins = CheckinToolDummies.CreateCheckinTools();
        }


        public void Add(CheckinTool entitie)
        {
            _checkins.Add(entitie);
        }

        public void Remove(object entitie)
        {
            _checkins.Remove(_checkins.FirstOrDefault(c => c.Id == (int) entitie));
        }

        public void DeleteEntityPermanently(CheckinTool entitieToRemove)
        {
            throw new NotImplementedException();
        }

        public bool ImplementsIsDeletable(CheckinTool entityToRemove)
        {
            throw new NotImplementedException();
        }

        public void MakeEntityDeleted(object entitie, CheckinTool entitieToRemove)
        {
            throw new NotImplementedException();
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

        public CheckinTool FindEntity(object entityId)
        {
            throw new NotImplementedException();
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
