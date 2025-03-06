using BankingApp.Models;

namespace BankingApp.Data
{
    public class IAccountRepository
    {
        Task<Account> GetAccountByNumberAsync(string accountNumber);
        Task UpdateAccountAsync(Account account);
    }
}
