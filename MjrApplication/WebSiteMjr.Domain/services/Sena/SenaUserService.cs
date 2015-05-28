using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Interfaces.Repository.Sena;
using WebSiteMjr.Domain.Interfaces.Services;

namespace WebSiteMjr.Domain.services.Sena
{
    public class SenaUserService : ISenaUserService
    {
        private readonly ISenaUserRepository _senaUserRepository;

        public SenaUserService(ISenaUserRepository senaUserRepository)
        {
            _senaUserRepository = senaUserRepository;
        }

        public void Create(string email, string companyName)
        {
            if (!string.IsNullOrEmpty(FindByEmail(email)))
                throw new Exception("Usuario existente");

            _senaUserRepository.Add(email, companyName);
        }

        public string FindByEmail(string email)
        {
            return _senaUserRepository.GetGuidByEmail(email);
        }
    }
}
