using System;

namespace WebSiteMjr.Domain.Exceptions
{
    public class CheckinDateTimeInconsistencyException: Exception
    {
        public override string Message
        {
            get
            {
                return "Modificação da data causa inconsistência entre movimentações anteriores.";
            }
        }
    }
}
