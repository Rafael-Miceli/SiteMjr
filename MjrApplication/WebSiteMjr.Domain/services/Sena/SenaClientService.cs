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

        public async Task Create(string name)
        {
            if (!string.IsNullOrEmpty(await FindByName(name)))
                throw new Exception("Cliente ja existente");

            await _senaClientRepository.Add(name);
        }

        public async Task<string> FindByName(string name)
        {
            return await _senaClientRepository.GetClientGuidByName(name);
        }
    }
}
