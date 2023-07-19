using eWallet.Repositories;

namespace eWallet.Services
{
    public interface IBaseService<T>
    {
        public IBaseRepository<T> Repository { get; }
    }
}
