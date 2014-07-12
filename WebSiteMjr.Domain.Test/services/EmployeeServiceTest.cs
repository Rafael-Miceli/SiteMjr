using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.services;
using WebSiteMjr.Domain.Test.Model;
using WebSiteMjr.EfData.DataRepository;

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

            var employeeService = new EmployeeService(employeeRepositoryMock.Object, new StubUnitOfWork());

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

            var employeeService = new EmployeeService(employeeRepositoryMock.Object, new StubUnitOfWork());

            //Act
            var employees = employeeService.ListEmployee();
            
            //Assert
            employeeRepositoryMock.VerifyAll();
            Assert.IsTrue(employees.Any(e => e.IsDeleted));
        }
    }
}
