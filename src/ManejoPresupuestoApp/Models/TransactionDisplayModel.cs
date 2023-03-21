namespace ManejoPresupuestoApp.Models
{
    public class TransactionDisplayModel
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public string GroupName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
    }
}
