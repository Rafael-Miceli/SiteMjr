using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository.GenericRepositorys;

namespace WebSiteMjr.EfData.DataRepository
{
    public class EmployeeRepository : GenericPersonRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(UnitOfWork<PersonsContext> uow) : base(uow)
        {
        }
    }
}
