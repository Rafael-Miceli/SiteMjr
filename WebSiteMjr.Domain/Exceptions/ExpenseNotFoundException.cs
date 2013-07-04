using System;
using System.Runtime.Serialization;

namespace ClubManager.Domain.Exceptions
{
    public class ExpenseNotFoundException: Exception
    {
        public ExpenseNotFoundException(string message)
            :base(message)
        {
            
        }

        public ExpenseNotFoundException(string message, Exception inner)
            : base(message, inner)
        {

        }

        protected ExpenseNotFoundException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {
            
        }
    }
}
