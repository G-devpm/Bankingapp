using BankingApp.Data;
using BankingApp.Models;
using BankingApp.Services;

public class TransferService : ITransferService
{
    private readonly IAccountRepository _accountRepository;
    private readonly BankDbContext _context;

    public TransferService(BankDbContext context)//IAccountRepository accountRepository, 
    {
        _accountRepository = accountRepository;
        _context = context;
    }

    public async Task<bool> TransferMoneyAsync(TransferRequest request, string userId)
    {
        var fromAccount = await _accountRepository.GetAccountByNumberAsync(request.FromAccountNumber);
        var toAccount = await _accountRepository.GetAccountByNumberAsync(request.ToAccountNumber);

        if (fromAccount == null || toAccount == null || fromAccount.BankUserId.ToString() != userId || fromAccount.Balance < request.Amount)
        {
            return false;
        }

        fromAccount.Balance -= request.Amount;
        toAccount.Balance += request.Amount;

        await _accountRepository.UpdateAccountAsync(fromAccount);
        await _accountRepository.UpdateAccountAsync(toAccount);
        return true;
    }

    public async Task<List<Transfer>> GetAllTransfersAsync()
    {
        return await _context.Transfers
            .Include(t => t.FromAccount)
            .Include(t => t.ToAccount)
            .ToListAsync();
    }
}