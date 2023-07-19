using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Data.Models
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public bool IsIdentified { get; set; } = false;
        public required string FullName { get; set; }
        public required string PasswordHash { get; set; }
    }
}
