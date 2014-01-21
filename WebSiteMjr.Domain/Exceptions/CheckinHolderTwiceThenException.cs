using System;

namespace WebSiteMjr.Domain.Exceptions
{
    public class CheckinHolderTwiceThenException : Exception
    {
        public override string Message
        {
            get
            {
                return "Não é possível haver movimentação duas vezes para o mesmo funcionário seguidamente.";
            }
        }
    }
}
