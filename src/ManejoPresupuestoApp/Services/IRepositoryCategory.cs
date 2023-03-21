using ManejoPresupuestoApp.Models;

namespace ManejoPresupuestoApp.Services
{
    public interface IRepositoryCategory
    {
        Task Create(CategoryModel categoryModel);
        Task Delete(int categoryId);
        Task<CategoryModel> Get(int categoryId);
        Task<IEnumerable<CategoryModel>> GetByGroupId(int groupId);
        Task<IEnumerable<CategoryDisplayModel>> GetByUserId(int userId);
    }
}
