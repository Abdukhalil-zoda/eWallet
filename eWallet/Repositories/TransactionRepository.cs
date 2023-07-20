using eWallet.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eWallet.Repositories
{
    public class TransactionRepository : IBaseRepository<Transaction>
    {
        public TransactionRepository(DataContext data)
        {
            Data = data;
        }
        public DataContext Data { get; }

        public async Task Delete(Transaction entity)
        {
            Data.Transactions.Remove(entity);
            await Data.SaveChangesAsync();
        }

        public IEnumerable<Transaction> GetAll() =>
            Data.Transactions;

        public Transaction GetById(Guid id) =>
            Data.Transactions.First(x => x.Id == id);

        public async Task Insert(Transaction entity)
        {
            Data.Transactions.Add(entity);
            await Data.SaveChangesAsync();
        }

        public async Task Update(Transaction entity)
        {
            Data.Entry(entity).State = EntityState.Modified;
            await Data.SaveChangesAsync();
        }

        public IEnumerable<Transaction> Where(Func<Transaction, bool> p) =>
            Data.Transactions.Where(p);
    }
}
