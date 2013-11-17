using System.Collections.Generic;
using WebSiteMjr.Domain.Exceptions;
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
        
        public StuffCategoryService(IStuffCategoryRepository stuffCategoryRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _stuffCategoryRepository = stuffCategoryRepository;
        }
        
        public void CreateStuffCategory(StuffCategory stuffCategory)
        {
            if (CategoryExists(stuffCategory))
                throw new ObjectExistsException<StuffCategory>();

            _stuffCategoryRepository.Add(stuffCategory);
            _unitOfWork.Save();
        }

        private bool CategoryExists(StuffCategory stuffCategory)
        {
            return FindStuffCategoryByName(stuffCategory.Name) != null;
        }

        public void UpdateStuffCategory(StuffCategory stuffCategory)
        {
            _stuffCategoryRepository.Update(stuffCategory);
            _unitOfWork.Save();
        }

        public void DeleteStuffCategory(object stuffCategory)
        {
            _stuffCategoryRepository.Remove(stuffCategory);
            _unitOfWork.Save();
        }

        public IEnumerable<StuffCategory> ListStuffCategory()
        {
            return _stuffCategoryRepository.GetAll();
        }

        public StuffCategory FindStuffCategory(object idStuffCategory)
        {
            return _stuffCategoryRepository.GetById(idStuffCategory);
        }
        
        public StuffCategory FindStuffCategoryByName(string name)
        {
            return _stuffCategoryRepository.GetStuffCategoryByName(name);
        }
    }
}
