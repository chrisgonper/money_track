using ManejoPresupuestoApp.Validators;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuestoApp.Models
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        [Display(Name = "Categoria")]
        [NonZero]
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Cantidad")]
        [NonZero]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }
        public string Notes { get; set; }
    }
}
