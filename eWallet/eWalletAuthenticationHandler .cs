using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

namespace eWallet
{
    public class eWalletAuthenticationHandler : AuthenticationHandler<JwtBearerOptions>
    {
        public eWalletAuthenticationHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IConfiguration config) : base(options, logger, encoder, clock)
        {

            SECRET_KEY = config["JWT:Key"]!.ToString();
        }
        private readonly string SECRET_KEY;
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

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("X-UserId", out var userIdHeader) ||
            !Request.Headers.TryGetValue("X-Digest", out var digestHeader))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            try
            {
                var data = string.Join(".", digestHeader.ToString().Split('.').SkipLast(1));
                var sign = digestHeader.ToString().Split('.').Last();
                if (sign != ComputeHmacSha1(data, SECRET_KEY))
                {
                    return AuthenticateResult.Fail("Unauthorized");
                }
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
            

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userIdHeader!),
                
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

        }
    }
}
