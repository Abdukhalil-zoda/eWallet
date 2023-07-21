using eWallet.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Data.DTO.Transaction
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public required Guid Wallet { get; set; }
        public required int TransactionAmount { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.Waiting;
        public string? Description { get; set; }
    }
}
