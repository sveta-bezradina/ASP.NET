using lr_10.Attributes;
using System.ComponentModel.DataAnnotations;

namespace lr_10.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Ім'я та прізвище є обов'язковим полем.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email є обов'язковим полем.")]
        [EmailAddress(ErrorMessage = "Введіть коректну адресу електронної пошти.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Бажану дату консультації потрібно вказати.")]
        [DataType(DataType.Date)]
        [CustomDateValidation(ErrorMessage = "Дата повинна бути в майбутньому і не може бути у вихідний день.")]
        public DateTime ConsultationDate { get; set; }

        [Required(ErrorMessage = "Необхідно вибрати продукт для консультації.")]
        public string Product { get; set; }

        public bool IsValidConsultationDate()
        {
            if (Product == "Основи" && ConsultationDate.DayOfWeek == DayOfWeek.Monday)
            {
                return false;
            }

            return true;
        }
    }
}
