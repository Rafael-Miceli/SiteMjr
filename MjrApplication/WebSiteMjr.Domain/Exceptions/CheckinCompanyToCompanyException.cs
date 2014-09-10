using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMjr.Domain.Exceptions
{
    public class CheckinCompanyToCompanyException : Exception
    {
        public override string Message
        {
            get
            {
                return "Movimentação da ferramenta de uma empresa para outra empresa não é possível.";
            }
        }
    }
}
