using Dapper;
using ManejoPresupuestoApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace ManejoPresupuestoApp.Services
{
    public class RepositoryCategory : IRepositoryCategory
    {
        private readonly string _connectionString;
        public RepositoryCategory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Create(CategoryModel categoryModel)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>("CategoryCreate",
                                                            categoryModel,
                                                            commandType: CommandType.StoredProcedure);
            categoryModel.CategoryId = id;
        }

        public async Task<IEnumerable<CategoryModel>> GetByGroupId(int groupId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CategoryModel>("CategoryGetByGroupId",
                                                                new { GroupId = groupId },
                                                                commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<CategoryDisplayModel>> GetByUserId(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CategoryDisplayModel>("CategoryGetByUserId",
                                                                new { UserId = userId },
                                                                commandType: CommandType.StoredProcedure);
        }

        public async Task<CategoryModel> Get(int categoryId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<CategoryModel>("CategoryGetById",
                                                                                new { CategoryId = categoryId },
                                                                                commandType: CommandType.StoredProcedure);
        }

        public async Task Delete(int categoryId)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync("CategoryDeleteById",
                                                      new { CategoryId = categoryId },
                                                      commandType: CommandType.StoredProcedure);
        }
    }
}
