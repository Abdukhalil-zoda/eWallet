using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Data.Models
{
    public class Wallet : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public required string Name { get; set; }
        public int Amount { get; set; } 
        public required Guid Owner { get; set; }
    }
}
