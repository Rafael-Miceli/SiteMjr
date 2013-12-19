using System;
using System.ComponentModel.DataAnnotations;

namespace Mjr.Extensions
{
    public class NoFutureDateAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Data não pode ser futura.";

        public NoFutureDateAttribute()
            : base(DefaultErrorMessage)
        { }

        public override bool IsValid(object value)
        {
            if (!(value is DateTime))
            {
                return true;
            }
            var dateValue = (DateTime)value;
            return !(dateValue > DateTime.Now);
        }
    }
}
