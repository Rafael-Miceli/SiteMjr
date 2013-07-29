using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Model;
using WebSiteMjr.EfBaseData.UnitOfWork;
using WebSiteMjr.EfData.Context;
using WebSiteMjr.EfData.DataRepository.GenericRepositorys;
using WebSiteMjr.EfData.UnitOfWork;

namespace WebSiteMjr.EfData.DataRepository
{
    public class CompanyRepository: GenericPersonRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(UnitOfWork<PersonsContext> uow) : base(uow)
        {}
    }
}
