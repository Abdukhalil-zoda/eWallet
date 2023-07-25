using eWallet.Data.DTO.Auth;
using eWallet.Data.Models;
using eWallet.Repositories;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace eWallet.Services
{
    public class AuthService : IAuthService
    {
        public AuthService(IBaseRepository<User> userRepository, IBaseRepository<Wallet> walletRepository, IConfiguration config)
        {
            UserRepository = userRepository;
            WalletRepository = walletRepository;
            Configuration = config;
            SECRET_KEY = Configuration["JWT:Key"]!.ToString();
            SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        }
        public IBaseRepository<User> UserRepository { get; }
        public IBaseRepository<Wallet> WalletRepository { get; }

        public readonly IConfiguration Configuration;

        private readonly string SECRET_KEY;
        private readonly double Expires = 3;
        private readonly SymmetricSecurityKey SIGNING_KEY;
        private JwtSecurityTokenHandler securityTokenHandler { get; } = new JwtSecurityTokenHandler();

        public string GetJwtToken(User User)
        {
            var credentials = new SigningCredentials(SIGNING_KEY, "HS1");
            var header = new JwtHeader(credentials);

            DateTime Expiry = DateTime.UtcNow.AddDays(Expires);

            var claims = new List<Claim>();
            foreach (var walet in WalletRepository.Where(p => p.Owner == User.Id))
            {
                claims.Add(new Claim(walet.Id.ToString(), JsonConvert.SerializeObject(walet)));
            }
            var tokenDescriptor = new SecurityTokenDescriptor()
            {

                AdditionalHeaderClaims = header,
                Subject = new ClaimsIdentity(claims),
                Expires = Expiry,
                SigningCredentials = credentials,
            };

            var base64UrlHeader = Base64UrlEncode(JsonConvert.SerializeObject(header));
            var payload = JsonConvert.SerializeObject(tokenDescriptor, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

            var base64UrlPayload = Base64UrlEncode(payload);

            var dataToSign = $"{base64UrlHeader}.{base64UrlPayload}";
            var signature = ComputeHmacSha1(dataToSign, SECRET_KEY);

            var jwtToken = $"{dataToSign}.{signature}";
            return jwtToken;
        }

        private string ComputeHmacSha1(string input, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            using (var hmacSha1 = new HMACSHA1(keyBytes))
            {
                var hashBytes = hmacSha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Base64UrlEncode(Convert.ToBase64String(hashBytes));
            }
        }


        private string Base64UrlEncode(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
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
        public AuthDTO? Login(string username, string password)
        {
            var user = UserRepository.Where(p => p.UserName == username && p.PasswordHash == GetSH256Hash(password)).FirstOrDefault();
            if (user == null) return null;
            HMACSHA1 hmacSHA1 = new HMACSHA1();

            var token = GetJwtToken(user);
            return new AuthDTO { UserId = user.Id, Digest = token };
        }

        public async Task AddUser(User user)
        {
            await UserRepository.Insert(user);
        }
    }
}
