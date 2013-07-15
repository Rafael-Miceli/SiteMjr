using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository.GenericRepositorys;
using WebSiteMjr.EfData.UnitOfWork;

namespace WebSiteMjr.EfData.DataRepository
{
    public class CompanyRepository: GenericPersonRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IUnitOfWork<PersonsContext> uow) : base(uow)
        {}
    }
}
