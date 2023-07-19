using eWallet.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Data.Models
{
    public class Transaction : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public required Guid Wallet { get; set; }
        public required int TransactionAmount { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.Waiting;
        public string? Description { get; set; }
    }
}
