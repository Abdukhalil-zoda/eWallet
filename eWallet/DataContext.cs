using eWallet.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace eWallet
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }
        
        public required DbSet<User> Users { get; set; }
        public required DbSet<Wallet> Wallets { get; set; }
        public required DbSet<Transaction> Transactions { get; set; }
    }
}
