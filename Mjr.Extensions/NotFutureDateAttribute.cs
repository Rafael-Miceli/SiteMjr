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
                DateTime dateTimeValue;
                if (DateTime.TryParse(value.ToString(), out dateTimeValue))
                {
                    return !(dateTimeValue > DateTime.UtcNow.ConvertToTimeZone().AddHours(1));
                }

                return true;
            }

            var dateValue = (DateTime)value;
            return !(dateValue > DateTime.UtcNow.ConvertToTimeZone());
        }
    }

    public class DateTimeIsValidAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Data Inválida.";
        public DateTimeIsValidAttribute()
            : base(DefaultErrorMessage)
        { }

        public override bool IsValid(object value)
        {
            DateTime dateTimeValue;
            var parsed = DateTime.TryParse(value.ToString(), out dateTimeValue);
            return parsed;
        }
    }
}
