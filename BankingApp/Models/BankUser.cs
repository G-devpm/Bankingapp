namespace BankingApp.Models
{
    using Microsoft.AspNetCore.Identity;

    public class BankUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
