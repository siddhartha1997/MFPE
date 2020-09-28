using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionMicroservice.Repository
{
    public interface ITransactionRep
    {
        public TransactionMsg deposit(dwacc value);
        public TransactionMsg withdraw(dwacc value);
        public TransactionMsg transfer(transfers value);
    }
}
