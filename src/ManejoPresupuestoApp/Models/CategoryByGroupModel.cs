namespace ManejoPresupuestoApp.Models
{
    public class CategoryByGroupModel
    {
        public string GroupName { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}
