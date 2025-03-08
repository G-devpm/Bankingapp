using BankingApp.Data;
using BankingApp.Models;
using BankingApp.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
namespace BankingApp.Tests
{
    public class TransferServiceTests
    {
        [Fact]
        public async Task TransferMoneyAsync_SufficientBalance_ReturnsTrue()
        {
            var mockRepo = new Mock<IAccountRepository>();
            mockRepo.Setup(r => r.GetAccountByNumberAsync("123"))
            .ReturnsAsync(new Account { Id = 1, AccountNumber = "123", Balance = 1000, BankUserId = 1 });
            mockRepo.Setup(r => r.GetAccountByNumberAsync("456"))
            .ReturnsAsync(new Account { Id = 2, AccountNumber = "456", Balance = 500, BankUserId = 1 });
            var options = new DbContextOptionsBuilder<BankDbContext>().UseInMemoryDatabase("TestDb").Options;
            var context = new BankDbContext(options);
            var service = new TransferService(mockRepo.Object, context);
            var result = await service.TransferMoneyAsync(new TransferRequest { FromAccountNumber = "123", ToAccountNumber = "456", Amount = 200 }, "1");
            Assert.True(result);
        }
        [Fact]
        public async Task GetAllTransfersAsync_ReturnsTransfers()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>().UseInMemoryDatabase("TestDb").Options;
            var context = new BankDbContext(options);
            context.Transfers.Add(new Transfer { Id = 1, Amount = 200, FromAccountId = 1, ToAccountId = 2 });
        }
    }
}