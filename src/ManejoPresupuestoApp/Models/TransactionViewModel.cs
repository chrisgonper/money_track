using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuestoApp.Models
{
    public class TransactionViewModel:TransactionModel
    {
        [Display(Name = "Grupo")]
        public int GroupId { get; set; }
        public IEnumerable<SelectListItem> CategoryGroups { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

    }
}
