using ManejoPresupuestoApp.Validators;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuestoApp.Models
{
    public class CategoryModel
    {
        [Display(Name = "Grupo")]
        [Required(ErrorMessage = "Seleccione un grupo")]
        [NonZero]
        public int GroupId { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre")]
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
    }
}
