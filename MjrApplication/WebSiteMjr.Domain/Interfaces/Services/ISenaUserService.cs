using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface ISenaUserService
    {
        void Create(string email, string companyName);
        string FindByEmail(string email);
    }
}
