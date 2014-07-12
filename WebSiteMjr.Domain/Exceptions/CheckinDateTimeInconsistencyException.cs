using System;

namespace WebSiteMjr.Domain.Exceptions
{
    public class CheckinInconsistencyException: Exception
    {
        public override string Message
        {
            get
            {
                return "Modificação causa inconsistência entre movimentações anteriores.";
            }
        }
    }
}
