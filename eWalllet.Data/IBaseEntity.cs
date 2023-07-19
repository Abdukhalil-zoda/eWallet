using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWallet.Data
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; }
    }
}
