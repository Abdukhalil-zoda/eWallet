using eWallet.Data.DTO.Transaction;
using eWallet.Data.Enums;
using eWallet.Data.Models;
using eWallet.Repositories;

namespace eWallet.Services
{
    public class TransactionService : ITransactionService
    {
        public TransactionService(IBaseRepository<Transaction> transactionRepository,
            IBaseRepository<Wallet> walletRepository,
            IBaseRepository<User> userRepository)
        {
            TransactionRepository = transactionRepository;
            WalletRepository = walletRepository;
            UserRepository = userRepository;
        }

        public IBaseRepository<Transaction> TransactionRepository { get; }
        public IBaseRepository<Wallet> WalletRepository { get; }
        public IBaseRepository<User> UserRepository { get; }

        public IEnumerable<Transaction> LastMonthTransaction(Guid walletId) =>
            TransactionRepository.Where(p => p.Wallet == walletId && 
            p.DateAdded.Month == DateTime.UtcNow.Month &&
            p.DateAdded.Year == DateTime.UtcNow.Year);
        

        public async Task<Guid> NewTransaction(Guid walletId, int amount)
        {
            var wallet = WalletRepository.GetById(walletId);
            if (wallet == null) 
            {
                var transaction = new Transaction
                {
                    Status = TransactionStatus.Canceled,
                    Wallet = walletId,
                    TransactionAmount = amount,
                    Description = "Wallet not found"
                };

                return await TransactionRepository.Insert(transaction);
            }
            var acc = UserRepository.GetById(wallet.Owner);
            var total = wallet.Amount + amount;
            if (acc!.IsIdentified )
            {
                if (total > 100000)
                {
                    var transaction = new Transaction
                    {
                        Status = TransactionStatus.Canceled,
                        Wallet = walletId,
                        TransactionAmount = amount,
                        Description = "Wallet limit < 100000"
                    };

                    return await TransactionRepository.Insert(transaction);
                }
                else
                {
                    var transaction = new Transaction
                    {
                        Status = TransactionStatus.Success,
                        Wallet = walletId,
                        TransactionAmount = amount,
                    };

                    wallet.Amount += amount;
                    await WalletRepository.Update(wallet);

                    return await TransactionRepository.Insert(transaction);
                }
            }
            else
            {
                if (total > 10000)
                {
                    var transaction = new Transaction
                    {
                        Status = TransactionStatus.Canceled,
                        Wallet = walletId,
                        TransactionAmount = amount,
                        Description = "Wallet limit < 10000"
                    };

                    return await TransactionRepository.Insert(transaction);
                }
                else
                {
                    var transaction = new Transaction
                    {
                        Status = TransactionStatus.Success,
                        Wallet = walletId,
                        TransactionAmount = amount,
                    };

                    wallet.Amount += amount;
                    await WalletRepository.Update(wallet);

                    return await TransactionRepository.Insert(transaction);
                }
            }
            
        }
    }
}
