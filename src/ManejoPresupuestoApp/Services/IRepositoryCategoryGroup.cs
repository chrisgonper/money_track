using ManejoPresupuestoApp.Models;

namespace ManejoPresupuestoApp.Services
{
    public interface IRepositoryCategoryGroup
    {
        Task Create(CategoryGroupModel categoryGroup);
        Task Delete(int categoryGroupId);
        Task<CategoryGroupModel> Get(int groupId);
        Task<IEnumerable<CategoryGroupModel>> GetByUserId(int userId);
    }
}
