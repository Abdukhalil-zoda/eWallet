using eWallet.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace eWallet.Repositories
{
    public class WalletRepository : IBaseRepository<Wallet>
    {
        public WalletRepository(DataContext data)
        {
            Data = data;
        }
        public DataContext Data { get; }

        public async Task Delete(Wallet entity)
        {
            Data.Wallets.Remove(entity);
            await Data.SaveChangesAsync();
        }

        public IEnumerable<Wallet> GetAll() =>
            Data.Wallets;

        public Wallet? GetById(Guid id) =>
            Data.Wallets.FirstOrDefault(x => x.Id == id);

        public async Task<Guid> Insert(Wallet entity)
        {
            var id = Data.Wallets.Add(entity).Entity.Id;
            await Data.SaveChangesAsync();
            return id;
        }

        public async Task Update(Wallet entity)
        {
            Data.Entry(entity).State = EntityState.Modified;
            await Data.SaveChangesAsync();
        }

        public IEnumerable<Wallet> Where(Func<Wallet, bool> p) =>
            Data.Wallets.Where(p);
    }
}
