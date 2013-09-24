using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;

namespace WebSiteMjr.Domain.services.Stuffs
{
    public class StuffCategoryService : IStuffCategoryService
    {
        private readonly IStuffCategoryRepository _stuffCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
    }
}
