using ManejoPresupuestoApp.Models;

namespace ManejoPresupuestoApp.Services
{
    public class IconRepository : IIconRepository
    {
        private readonly string _connectionString;

        public IconRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public Task<IEnumerable<IconModel>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
