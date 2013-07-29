using System.Collections;
using System.Collections.Generic;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services
{
    public class StuffService
    {
        private readonly IStuffRepository _stuffRepository;
        private readonly IStuffCategoryRepository _stuffCategoryRepository;
        private readonly IStuffManufactureRepository _stuffManufactureRepository;

        private readonly IUnitOfWork _unitOfWork;

        public StuffService(IStuffRepository stuffRepository, IStuffCategoryRepository stuffCategoryRepository, IStuffManufactureRepository stuffManufactureRepository, IUnitOfWork unitOfWork)
        {
            _stuffRepository = stuffRepository;
            _stuffCategoryRepository = stuffCategoryRepository;
            _stuffManufactureRepository = stuffManufactureRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateStuff(Stuff stuff)
        {
            _stuffRepository.AddGraph(stuff);
            _unitOfWork.Save();
        }

        public void UpdateStuff(Stuff stuff)
        {
            _stuffRepository.Update(stuff);
            _unitOfWork.Save();
        }

        public void DeleteStuff(object stuff)
        {
            _stuffRepository.Remove(stuff);
            _unitOfWork.Save();
        }

        public IEnumerable<Stuff> ListStuff()
        {
            return _stuffRepository.GetAll();
        }

        public Stuff FindStuff(object idStuff)
        {
            return _stuffRepository.GetById(idStuff);
        }

        public IEnumerable<StuffCategory> ListStuffCategories()
        {
            return _stuffCategoryRepository.GetAll();
        }

        public IEnumerable<StuffManufacture> ListStuffManufacures()
        {
            return _stuffManufactureRepository.GetAll();
        }

        public void CreateStuffCategory(StuffCategory stuffCategory)
        {
            _stuffCategoryRepository.Add(stuffCategory);
            _unitOfWork.Save();
        }

        public StuffCategory FindStuffCategoryByName(string name)
        {
            return _stuffCategoryRepository.GetStuffCategoryByName(name);
        }

        public void CreateStuffManufacture(StuffManufacture stuffManufacture)
        {
            _stuffManufactureRepository.Add(stuffManufacture);
            _unitOfWork.Save();
        }

        public StuffManufacture FindStuffManufactureByName(string name)
        {
            return _stuffManufactureRepository.GetStuffManufacturerByName(name);
        }
    }
}
