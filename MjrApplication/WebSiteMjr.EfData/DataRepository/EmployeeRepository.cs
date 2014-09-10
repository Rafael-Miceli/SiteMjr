using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository.GenericRepositorys;

namespace WebSiteMjr.EfData.DataRepository
{
    public class EmployeeRepository : GenericPersonRepository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(UnitOfWork<PersonsContext> uow) : base(uow)
        {}

        public Employee GetEmployeeByName(string name)
        {
            return Get(n => n.Name.ToLower() == name.ToLower());
        }

        public IEnumerable<Employee> GetAllEmployeesNotDeleted()
        {
            return GetAll().Where(e => !e.IsDeleted);
        }

        public IEnumerable<Employee> GetAllEmployeesFromCompanyNotDeleted(int companyId)
        {
            var all = GetAll();
            return all.Where(e => !e.IsDeleted && e.Company.Id == companyId);
        }

        public Employee GetEmployeeByEmail(string email)
        {
            return Get(n => n.Email.ToLower() == email.ToLower());
        }
    }
}
