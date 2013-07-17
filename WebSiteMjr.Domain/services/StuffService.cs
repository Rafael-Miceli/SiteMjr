using System.Collections.Generic;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services
{
    public class StuffService
    {
        private readonly IStuffRepository _stuffRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StuffService(IStuffRepository stuffRepository, IUnitOfWork unitOfWork)
        {
            _stuffRepository = stuffRepository;
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

        public virtual Stuff FindStuff(object idStuff)
        {
            return _stuffRepository.GetById(idStuff);
        }
    }
}
