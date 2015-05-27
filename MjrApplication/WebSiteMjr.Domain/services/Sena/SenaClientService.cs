using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Interfaces.Repository.Sena;
using WebSiteMjr.Domain.Interfaces.Services;

namespace WebSiteMjr.Domain.services.Sena
{
    public class SenaClientService : ISenaClientService
    {
        private readonly ISenaClientRepository _senaClientRepository;

        public SenaClientService(ISenaClientRepository senaClientRepository)
        {
            _senaClientRepository = senaClientRepository;
        }

        public void Create(string name)
        {
            _senaClientRepository.Add(name);
        }

        public string FindByName(string name)
        {
            return _senaClientRepository.GetByName(name);
        }
    }
}
