using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Models
{
    public class AccountStatement
    {
        public int AccId { get; set; }
        public List<Statement> Statements { get; set; }
    }
}
