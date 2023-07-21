using eWallet.Data.DTO.Wallet;
using eWallet.Data.Models;
using eWallet.Repositories;
using System.Runtime.InteropServices;

namespace eWallet.Services
{
    public class WalletService : IWalletService
    {
        public IBaseRepository<Wallet> WalletRepository { get; }
        public IBaseRepository<User> UserRepository { get; }

        public WalletService(IBaseRepository<Wallet> walletRepository, IBaseRepository<User> userRepository)
        {
            WalletRepository = walletRepository;
            UserRepository = userRepository;
        }
        public User? CheckAcc(Guid walletId)
        {
            var wallet = WalletRepository.GetById(walletId);
            if (wallet == null) return null;
            var user = UserRepository.GetById(wallet.Owner);

            return user;
        }

        public async Task<Guid> CreateWallet(Guid userId, string name)
        {
            var wallet = new Wallet { Owner = userId, Name = name };
            return await WalletRepository.Insert(wallet);

        }

        public int? Balance(Guid walletId)
        {
            return WalletRepository.GetById(walletId)?.Amount;
        }

    }
}
