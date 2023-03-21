using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuestoApp.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "{0} es requerido")]
        [EmailAddress(ErrorMessage = "Escribe un correo valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }
    }
}
