using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Models
{
    public class transactionmsg
    {
        public double sbal { get; set; }
        public double rbal { get; set; }
        public string transferStatus { get; set; }
    }
}
