using AutoMapper;
using eWallet.Data.DTO.Auth;
using eWallet.Data.DTO.Transaction;
using eWallet.Data.DTO.Wallet;
using eWallet.Data.Models;
using System.Security.Cryptography;
using System.Text;

namespace eWallet
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<NewUserDTO, User>()
                .ForMember(dst => dst.PasswordHash, opt => opt.MapFrom(c => GetSH256Hash(c.Password)));
            CreateMap<User, WalletAccountDTO>();
            CreateMap<Transaction, TransactionDTO>();

        }

        string GetSH256Hash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("X2"));
                return sb.ToString();
            }
        }
    }
}
