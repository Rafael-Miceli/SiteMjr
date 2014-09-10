using System;
using WebSiteMjr.Domain.Interfaces.Model;

namespace WebSiteMjr.Domain.Exceptions
{

    public class EmployeeWithExistentEmailException : Exception
    {
        public override string Message
        {
            get
            {
                return "Este E-mail já existe para outro funcionário";
            }
        }
    }
}
