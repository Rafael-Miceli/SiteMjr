using System.Collections.Generic;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services.Stuffs
{
    public class StuffCategoryService : IStuffCategoryService
    {
        private readonly IStuffCategoryRepository _stuffCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public void CreateStuffCategory(StuffCategory stuffCategory)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateStuffCategory(StuffCategory stuffCategory)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteStuffCategory(object stuffCategory)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<StuffCategory> ListStuffCategory()
        {
            throw new System.NotImplementedException();
        }

        public StuffCategory FindStuffCategory(object idStuffCategory)
        {
            throw new System.NotImplementedException();
        }
    }
}
