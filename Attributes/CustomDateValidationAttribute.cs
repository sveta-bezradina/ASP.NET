using System.ComponentModel.DataAnnotations;

namespace lr_10.Attributes
{
    public class CustomDateValidationAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                if (date <= DateTime.Now)
                {
                    return false;
                }

                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
