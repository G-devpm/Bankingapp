namespace BankingApp.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int FromAccountId { get; set; }
        public Account FromAccount { get; set; }
        public int ToAccountId { get; set; }
        public Account ToAccount { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
