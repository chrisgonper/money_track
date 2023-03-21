using ManejoPresupuestoApp.Models;

namespace ManejoPresupuestoApp.Services
{
    public interface IRepositoryUser
    {
        Task<int> Create(User user);
        Task<User> GetByEmail(string normalizedEmail);
        int GetCurrentUserId();
    }
}
