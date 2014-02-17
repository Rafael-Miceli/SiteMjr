using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Interfaces.Repository;
using WebSiteMjr.Domain.Interfaces.Services;
using WebSiteMjr.Domain.Model;

namespace WebSiteMjr.Domain.services
{
    public class HolderService: IHolderService
    {
        private readonly IHolderRepository _holderRepository;

        public HolderService(IHolderRepository holderRepository)
        {
            _holderRepository = holderRepository;
        }

        public IEnumerable<Holder> ListHolder()
        {
            return _holderRepository.GetAll();
        }

        public Holder FindHolder(object idHolder)
        {
            return _holderRepository.GetById(idHolder);
        }

        public Holder FindHolderByName(string holderName)
        {
            return _holderRepository.GetHolderByName(holderName);
        }

        public IEnumerable<string> ListEmployeeCompanyHolderName()
        {
            var holders = ListHolder().ToList();
            return holders.Any() ? holders.Select(h => h.Name) : null;
        }

        public IEnumerable<string> ListNotDeletedHolderName()
        {
            var notDeletedHolders = _holderRepository.GetAllHoldersNotDeleted();
            return notDeletedHolders.Select(h => h.Name);
        }
    }
}
