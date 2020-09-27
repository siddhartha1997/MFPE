using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models
{
    public class CustomerCreationStatus
    {
        public string Message { get; set; }
        public int CustomerId { get; set; }
        public int CurrentAccountId { get; set; }
        public int SavingsAccountId { get; set; }
    }
}
