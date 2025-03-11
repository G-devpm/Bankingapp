using BankingApp.Models;
using BankingApp.Data;

namespace BankingApp.Data
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByNumberAsync(string accountNumber);
        Task UpdateAccountAsync(Account account);
    }

}
