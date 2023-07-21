using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Data.DTO.Transaction
{
    public class NewTransactionDTO
    {
        public Guid WalletId{ get; set; }
        public int Amount { get; set; }
    }
}
