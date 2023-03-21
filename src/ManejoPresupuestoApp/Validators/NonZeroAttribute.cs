using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuestoApp.Validators
{
    public class NonZeroAttribute:ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || Convert.ToInt32(value) <= 0)
            {
                return new ValidationResult($"El campo {validationContext.DisplayName} es invalido");
            }
            return ValidationResult.Success;
        }
    }
}
