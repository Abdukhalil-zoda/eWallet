using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eWallet.Data.DTO.Auth
{
    public class AuthDTO
    {
        [JsonPropertyName("X-UserId")]
        public required Guid UserId { get; set; }
        [JsonPropertyName("X-Digest")]
        public required string Digest { get; set; }
    }
}
