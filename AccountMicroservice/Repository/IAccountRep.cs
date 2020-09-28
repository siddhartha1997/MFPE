using AccountMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Repository
{
    public interface IAccountRep
    {
        public List<CurrentAccount> GetCurrent();
        public List<SavingsAccount> GetSavings();
        public List<AccountMsg> getCustomerAccounts(int id);
        public customeraccount createAccount(int id);
        public AccountMsg getAccount(int id);
        public IEnumerable<Statement> getAccountStatement(int AccountId, int from_date, int to_date);
        public AccountMsg deposit(dwacc value);
        public AccountMsg withdraw(dwacc value);
        public transactionmsg transfer(transfers value);

    }
}
