namespace BankingApp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public int BankUserId { get; set; }
        public BankUser BankUser { get; set; }
    }
}
