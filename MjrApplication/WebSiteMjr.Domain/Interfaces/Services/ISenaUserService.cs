using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMjr.Domain.Interfaces.Services
{
    public interface ISenaUserService
    {
        void Add(string email, string name);
        string FindByEmail(string email);
    }
}
