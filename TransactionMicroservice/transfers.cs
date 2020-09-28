using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionMicroservice
{
    public class transfers
    {
        public int source_accid { get; set; }
        public int destination_accid { get; set; }
        public int amount { get; set; }
    }
}
