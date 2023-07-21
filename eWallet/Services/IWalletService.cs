using eWallet.Data.DTO.Wallet;
using eWallet.Data.Models;

namespace eWallet.Services
{
    public interface IWalletService
    {
        public User? CheckAcc(Guid walletId);
        public Task<Guid> CreateWallet(Guid userId, string name);
        public int? Balance(Guid walletId);
    }
}
