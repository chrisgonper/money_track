using ManejoPresupuestoApp.Models;

namespace ManejoPresupuestoApp.Services
{
    public interface IRepositoryTransaction
    {
        Task Create(TransactionModel transactionModel);
        Task Delete(int transactionId);
        Task<TransactionModel> GetById(int transactionId);
        Task<IEnumerable<TransactionDisplayModel>> GetByMonth(int userId, int month, int year);
        Task<IEnumerable<TransactionDisplayModel>> GetByUserId(int userId);
    }
}
