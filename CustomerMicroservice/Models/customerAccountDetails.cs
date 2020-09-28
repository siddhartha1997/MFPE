using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models
{
    public class customerAccountDetails
    {
        public int customerId { get; set; }
        public int currentAccountId { get; set; }
        public int savingAccountId { get; set; }
    }
}
