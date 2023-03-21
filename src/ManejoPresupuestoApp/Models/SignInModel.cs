using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuestoApp.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "{0} es requerido")]
        [EmailAddress(ErrorMessage = "Escribe un correo valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Display(Name = "Recuerdame")]
        public bool RememberMe { get; set; }
    }
}
