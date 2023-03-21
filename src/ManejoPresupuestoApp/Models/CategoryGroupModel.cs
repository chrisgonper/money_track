using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuestoApp.Models
{
    public class CategoryGroupModel
    {
        public int GroupId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido!")]
        [Display(Name = "Nombre")]
        public string GroupName { get; set; }
        public int UserId { get; set; }
    }
}
