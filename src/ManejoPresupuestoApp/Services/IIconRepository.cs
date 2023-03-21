using ManejoPresupuestoApp.Models;

namespace ManejoPresupuestoApp.Services
{
    public interface IIconRepository
    {
        Task<IEnumerable<IconModel>> GetAll();
    }
}