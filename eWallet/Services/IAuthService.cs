using eWallet.Data.DTO.Auth;
using eWallet.Data.Models;

namespace eWallet.Services
{
    public interface IAuthService
    {
        AuthDTO? Login(string username, string password);
        Task AddUser(User user);
    }
}
