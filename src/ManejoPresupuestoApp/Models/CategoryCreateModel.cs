using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuestoApp.Models
{
    public class CategoryCreateModel:CategoryModel
    {
        public IEnumerable<SelectListItem> Groups { get; set; }
    }
}
