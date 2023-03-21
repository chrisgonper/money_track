namespace ManejoPresupuestoApp.Models
{
    public class TransactionGroupByDateModel
    {
        public DateTime TransactionDate { get; set; }
        public IEnumerable<TransactionDisplayModel> Transactions { get; set; }
    }
}
