using System.Collections.Generic;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services.Stuffs
{
    public class StuffManufactureService : IStuffManufactureService
    {
        private readonly IStuffManufactureRepository _stuffManufactureRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public StuffManufactureService(IStuffManufactureRepository stuffManufactureRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _stuffManufactureRepository = stuffManufactureRepository;
        }
        
        public void CreateStuffManufacture(StuffManufacture stuffManufacture)
        {
            _stuffManufactureRepository.Add(stuffManufacture);
            _unitOfWork.Save();
        }

        public void UpdateStuffManufacture(StuffManufacture stuffManufacture)
        {
            _stuffManufactureRepository.Update(stuffManufacture);
            _unitOfWork.Save();
        }

        public void DeleteStuffManufacture(object stuffManufacture)
        {
            _stuffManufactureRepository.Remove(stuffManufacture);
            _unitOfWork.Save();
        }

        public IEnumerable<StuffManufacture> ListStuffManufacture()
        {
            return _stuffManufactureRepository.GetAll();
        }

        public StuffManufacture FindStuffManufacture(object idStuffManufacture)
        {
            return _stuffManufactureRepository.GetById(idStuffManufacture);
        }
        
        public StuffManufacture FindStuffManufactureByName(string name)
        {
            return _stuffManufactureRepository.GetStuffManufacturerByName(name);
        }
    }
}
