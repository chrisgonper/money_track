using Dapper;
using ManejoPresupuestoApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ManejoPresupuestoApp.Services
{
    public class RepositoryTransaction : IRepositoryTransaction
    {
        private readonly string _connectionString;
        public RepositoryTransaction(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Create(TransactionModel transactionModel)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>("TransactionCreate",
                                                            transactionModel,
                                                            commandType: CommandType.StoredProcedure);
            transactionModel.TransactionId = id;
        }

        public async Task<IEnumerable<TransactionDisplayModel>> GetByUserId(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<TransactionDisplayModel>("TransactionGetByUserId",
                                                                new { UserId = userId },
                                                                commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<TransactionDisplayModel>> GetByMonth(int userId, int month, int year)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<TransactionDisplayModel>("TransactionGetByMonth",
                                                                new { UserId = userId, Month = month, Year = year },
                                                                commandType: CommandType.StoredProcedure);
        }
        public async Task<TransactionModel> GetById(int transactionId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<TransactionModel>("TransactionGetById",
                                                                new { TransactionId = transactionId },
                                                                commandType: CommandType.StoredProcedure);
        }

        public async Task Delete(int transactionId)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.ExecuteAsync("TransactionDeleteById", 
                                                      new { TransactionId = transactionId }, 
                                                      commandType: CommandType.StoredProcedure);
        }
    }
}
