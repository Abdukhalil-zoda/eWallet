using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Data.DTO.Wallet
{
    public class WalletAccountDTO
    {
        public bool IsIdentified { get; set; } = false;
        public required string UserName { get; set; }
        public required string FullName { get; set; }
    }
}
