using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankDbContext _context;

        public AccountRepository(BankDbContext context) => _context = context;

        public async Task<Account> GetAccountByNumberAsync(string accountNumber)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}
