using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Mjr.Extensions
{
    public class NoFutureDateAttribute : ValidationAttribute
    {
        private const string DateFormat = "dd/MM/yyyy";
        private const string DefaultErrorMessage = "Data não pode ser futura.";

        public NoFutureDateAttribute()
            : base(DefaultErrorMessage)
        { }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
            {
                return true;
            }
            var dateValue = (DateTime)value;
            return !(dateValue.Date > DateTime.Now.Date);
        }

        private static DateTime ParseDate(string dateValue)
        {
            return DateTime.ParseExact(dateValue, DateFormat,
            CultureInfo.InvariantCulture);
        }
    }
}
