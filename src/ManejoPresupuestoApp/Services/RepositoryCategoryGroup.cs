using Dapper;
using ManejoPresupuestoApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace ManejoPresupuestoApp.Services
{
    public class RepositoryCategoryGroup : IRepositoryCategoryGroup
    {
        private readonly string _connectionString;

        public RepositoryCategoryGroup(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<CategoryGroupModel> Get(int groupId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<CategoryGroupModel>("CategoryGroupGetById",
                                                                                new { GroupId = groupId },
                                                                                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CategoryGroupModel>> GetByUserId(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CategoryGroupModel>("CategoryGroupGetByUserId",
                                                                        new { UserId = userId },
                                                                        commandType: CommandType.StoredProcedure);
        }
        public async Task Create(CategoryGroupModel categoryGroup)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>("CategoryGroupCreate", 
                                                            categoryGroup,
                                                            commandType: CommandType.StoredProcedure);
            categoryGroup.GroupId = id;
        }
        public async Task Delete(int groupId)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync("CategoryGroupDeleteById",
                                                      new { GroupId = groupId },
                                                      commandType: CommandType.StoredProcedure);
        }
    }
}
