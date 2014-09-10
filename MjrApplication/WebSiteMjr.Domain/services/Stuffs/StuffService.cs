using System.Collections.Generic;
using System.Linq;
using WebSiteMjr.Domain.Interfaces.Model;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Interfaces.Uow;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services.Stuffs
{
    public class StuffService: IStuffService
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
            _stuffRepository.AddOrUpdateGraph(stuff);
            _unitOfWork.Save();
        }

        public void UpdateStuff(Stuff stuff)
        {
            stuff.State = State.Modified;

            if (stuff.StuffCategory != null)
                stuff.StuffCategory.State = State.Modified;

            if (stuff.StuffManufacture != null)
                stuff.StuffManufacture.State = State.Modified;

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
    }
}
