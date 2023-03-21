using Dapper;
using ManejoPresupuestoApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Claims;

namespace ManejoPresupuestoApp.Services
{
    public class RepositoryUser : IRepositoryUser
    {
        private readonly string _connectionString;
        private readonly HttpContext _httpContext;
        public RepositoryUser(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            this._httpContext = httpContextAccessor.HttpContext;
        }
        public int GetCurrentUserId()
        {
            if (this._httpContext.User.Identity.IsAuthenticated)
            {
                var idClaim = this._httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                int userId = int.Parse(idClaim.Value);
                return userId;
            }
            else
            {
                throw new ApplicationException("El usuario no esta autenticado");
            }
        }
        public async Task<int> Create(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>("UserCreate",
                                                            user,
                                                            commandType: CommandType.StoredProcedure);
            return id;
        }
        public async Task<User> GetByEmail(string normalizedEmail)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<User>("UserGetByEmail",
                                                                                new { NormalizedEmail = normalizedEmail },
                                                                                commandType: CommandType.StoredProcedure);
        }
    }
}
