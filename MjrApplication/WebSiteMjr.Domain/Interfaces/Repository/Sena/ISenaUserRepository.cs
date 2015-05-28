using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMjr.Domain.Interfaces.Repository.Sena
{
    public interface ISenaUserRepository
    {
        void Add(string email, string companyName);
        string GetGuidByEmail(string email);
    }
}
