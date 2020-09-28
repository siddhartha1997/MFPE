using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models
{
    public class customerDetails
    {
        public int customerId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string dateOfBirth { get; set; }
        public string panNumber { get; set; }
    }
}
