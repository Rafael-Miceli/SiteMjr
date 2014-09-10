using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository;

namespace WebSiteMjr.EfPersonDataTest.DataRepository
{
    [TestClass]
    public class EmployeeRepostiroyIntegrationTest
    {
        private UnitOfWork<PersonsContext> _uow;

        [TestInitialize]
        public void Initialize()
        {
            _uow = new UnitOfWork<PersonsContext>();
        }

        [Ignore]
        [TestMethod]
        public void Should_Return_All_Employees_Not_Deleted()
        {
            //Arrange
            var employeeRepository = new EmployeeRepository(_uow);

            //Act
            var employees = employeeRepository.GetAllEmployeesNotDeleted();

            //Assert
            Assert.IsFalse(employees.Any(e => e.IsDeleted));
        }

        [Ignore]
        [TestMethod]
        public void Should_Not_Delete_Employees_From_Database()
        {
            //Arrange
            const string employeeName = "Funcionario-Test";
            var employee = new Employee
            {
                Name = employeeName,
                LastName = "Testeee"
            };

            var employeeRepository = new EmployeeRepository(_uow);
            employeeRepository.Add(employee);
            _uow.Save();

            var employeeExpected = employeeRepository.GetEmployeeByName(employee.Name);
            
            //Act
            employeeRepository.Remove(employeeExpected.Id);
            _uow.Save();

            //Assert
            Assert.IsNotNull(employeeExpected);
            Assert.IsTrue(employeeExpected.IsDeleted);
            Assert.AreEqual(employeeName + " (Excluido)", employeeExpected.Name);

            employeeRepository.DeleteEntityPermanently(employeeExpected);
            _uow.Save();

            Assert.IsNull(employeeRepository.GetEmployeeByName(employee.Name));
        }

        [Ignore]
        [TestMethod]
        public void Should_Not_Mark_As_Delete_Twice()
        {
            //Arrange
            const string employeeName = "Funcionario-Test";
            var employee = new Employee
            {
                Name = employeeName,
                LastName = "Testeee"
            };

            var employeeRepository = new EmployeeRepository(_uow);
            employeeRepository.Add(employee);
            _uow.Save();

            var employeeExpected = employeeRepository.GetEmployeeByName(employee.Name);

            //Act
            employeeRepository.Remove(employeeExpected.Id);
            _uow.Save();

            employeeRepository.Remove(employeeExpected.Id);
            _uow.Save();

            //Assert
            Assert.IsNotNull(employeeExpected);
            Assert.IsTrue(employeeExpected.IsDeleted);
            Assert.AreEqual(employeeName + " (Excluido)", employeeExpected.Name);

            employeeRepository.DeleteEntityPermanently(employeeExpected);
            _uow.Save();

            Assert.IsNull(employeeRepository.GetEmployeeByName(employee.Name));
        }

        
    }
}
