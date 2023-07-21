using eWallet.Data.Models;

namespace eWallet.Services
{
    public interface ITransactionService
    {
        public Task<Guid> NewTransaction(Guid walletId, int amount);
        public IEnumerable<Transaction> LastMonthTransaction(Guid walletId);
    }
}
