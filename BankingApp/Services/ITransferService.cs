using BankingApp.Models;

namespace BankingApp.Services
{
    public interface ITransferService
    {
        Task<bool> TransferMoneyAsync(TransferRequest request, string userId);
        Task<List<Transfer>> GetAllTransfersAsync();
    }

    public class TransferRequest
    {
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}