using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.Test.Model;

namespace WebSiteMjr.Domain.Test.services
{
    [TestClass]
    public class EmployeeServiceTest
    {
        [TestMethod]
        public void Should_Return_All_Employees_Not_Deleted()
        {
            //Arrange
            var employeesNotDeleted = EmployeeDummies.CreateListOfEmployees().Where(e => !e.IsDeleted);
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock.Setup(x => x.GetAllEmployeesNotDeleted()).Returns(employeesNotDeleted);

            var employeeService = new EmployeeService(employeeRepositoryMock.Object, null, null, new StubUnitOfWork());

            //Act
            var employees = employeeService.ListEmployeesNotDeleted();
            
            //Assert
            employeeRepositoryMock.VerifyAll();
            Assert.IsFalse(employees.Any(e => e.IsDeleted));
        }

        [TestMethod]
        public void Should_Return_All_Employees_Even_The_Deleted_Ones()
        {
            //Arrange
            var employeesList = EmployeeDummies.CreateListOfEmployees();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock.Setup(x => x.GetAll()).Returns(employeesList);

            var employeeService = new EmployeeService(employeeRepositoryMock.Object, null, null, new StubUnitOfWork());

            //Act
            var employees = employeeService.ListEmployee();
            
            //Assert
            employeeRepositoryMock.VerifyAll();
            Assert.IsTrue(employees.Any(e => e.IsDeleted));
        }

        [TestMethod]
        public void Given_A_Request_To_List_All_Active_Users_From_The_Company_Who_Is_Requesting_When_Requesting_Then_Should_Return_All_Active_Users_From_That_Company()
        {
            //Arrange
            var companyRequesting = CompanyDummies.CreatePortofinoCompany();
            var employeesNotDeletedFromCompany = EmployeeDummies.CreateListOfEmployees().Where(e => !e.IsDeleted && e.Company.Id == companyRequesting.Id);

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();

            employeeRepositoryMock.Setup(x => x.GetAllEmployeesFromCompanyNotDeleted(companyRequesting.Id)).Returns(employeesNotDeletedFromCompany);

            var employeeService = new EmployeeService(employeeRepositoryMock.Object, null, null, new StubUnitOfWork());

            //Act
            var employees = employeeService.ListEmployeesFromCompanyNotDeleted(companyRequesting.Id);

            ////Assert
            employeeRepositoryMock.VerifyAll();
            Assert.IsFalse(employees.Any(e => e.IsDeleted));
            Assert.IsFalse(employees.Any(e => e.Company.Id != companyRequesting.Id));
        }
    }
}
