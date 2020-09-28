using AccountMicroservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AccountMicroservice.Repository
{
    public class AccountRep : IAccountRep
    {
        int acid = 1;
        Uri baseAddress = new Uri("https://localhost:44304/api");   //Port No.
        HttpClient client;
        public AccountRep()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public static List<AccountStatement> accountStatements = new List<AccountStatement>()
        {
            new AccountStatement{AccId=202,
            Statements= new List<Statement>()
            {
                new Statement{date=21092020,Narration="Withdrawn",refno=12345,valueDate=01022020,withdrawal=1000.00,deposit=0.00,closingBalance=1000.00},
                new Statement{date=27092020,Narration="Deposited",refno=21345,valueDate=04022020,withdrawal=0.00,deposit=2000.00,closingBalance=3000.00}
                }
            }
         };
        public static List<customeraccount> customeraccounts = new List<customeraccount>()
        {
            new customeraccount{custId=2,CAId=201,SAId=202}
        };
        public static List<CurrentAccount> currentAccounts = new List<CurrentAccount>()
        {
            new CurrentAccount{CAId=201,CBal=1000}
        };
        public static List<SavingsAccount> savingsAccounts = new List<SavingsAccount>()
        {
            new SavingsAccount{SAId=202,SBal=500}
        };
        public List<CurrentAccount> GetCurrent()
        {
            return currentAccounts;
        }

        public List<SavingsAccount> GetSavings()
        {
            return savingsAccounts;
        }

        public List<AccountMsg> getCustomerAccounts(int id)
        {
            var a = customeraccounts.Find(c => c.custId == id);
            var ca = currentAccounts.Find(cac => cac.CAId == a.CAId);
            var sa = savingsAccounts.Find(sac => sac.SAId == a.SAId);
            var ac = new List<AccountMsg>
            {
                new AccountMsg{AccId=ca.CAId,AccType="Current Account",AccBal=ca.CBal},
                new AccountMsg{AccId=sa.SAId,AccType="Savings Account",AccBal=sa.SBal}
            };
            return ac;
        }
        public customeraccount createAccount(int id)
        {
            customeraccount a = new customeraccount
            {
                custId = id,
                CAId = (id * 100) + acid,
                SAId = (id * 100) + (acid + 1)
            };
            customeraccounts.Add(a);
            var cust = customeraccounts.Find(c => c.custId == id);
            CurrentAccount ca = new CurrentAccount
            {
                CAId = cust.CAId,
                CBal = 0.00
            };
            currentAccounts.Add(ca);
            SavingsAccount sa = new SavingsAccount
            {
                SAId = cust.SAId,
                SBal = 0.00
            };
            savingsAccounts.Add(sa);
            return cust;
        }

        public AccountMsg getAccount(int id)
        {
            if (id % 2 != 0)
            {
                var ca = currentAccounts.Find(a => a.CAId == id);
                var ac1 = new AccountMsg
                {
                    AccId = ca.CAId,
                    AccType = "Current Account",
                    AccBal = ca.CBal
                };
                return ac1;
            }
            var sa = savingsAccounts.Find(a => a.SAId == id);
            var ac = new AccountMsg
            {
                AccId = sa.SAId,
                AccType = "Savings Account",
                AccBal = sa.SBal
            };
            return ac;
        }

        public IEnumerable<Statement> getAccountStatement(int AccountId, int from_date, int to_date)
        {
            if (from_date != 0 || to_date != 0)
            {
                var accs = accountStatements.Find(a => a.AccId == AccountId);
                var s = accs.Statements;
                foreach (var n in s)
                {
                    if (n.date >= from_date && n.date <= to_date)
                    {
                        return s;
                    }
                }
            }
            var accs1 = accountStatements.Find(a => a.AccId == AccountId);
            var s1 = accs1.Statements;
            foreach (var n in s1)
            {
                if (n.date > 01092020 && n.date < 30092020)
                {
                    return s1;
                }
            }
            return null;
        }

        public AccountMsg deposit(dwacc value)
        {
            string data = JsonConvert.SerializeObject(value);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Transaction/deposit/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                Ts s1 = JsonConvert.DeserializeObject<Ts>(data1);
                if (s1.Message == "Success")
                {
                    if (value.AccountId % 2 == 0)
                    {
                        var sa = savingsAccounts.Find(a => a.SAId == value.AccountId);
                        sa.SBal = sa.SBal + value.Balance;
                        AccountMsg sob = new AccountMsg
                        {
                            AccId = value.AccountId,
                            AccType = "Deposited Correctly",
                            AccBal = sa.SBal
                        };
                        return sob;
                    }
                    var ca = currentAccounts.Find(a => a.CAId == value.AccountId);
                    ca.CBal = ca.CBal + value.Balance;
                    AccountMsg cob = new AccountMsg
                    {
                        AccId = value.AccountId,
                        AccType = "Deposited Correctly",
                        AccBal = ca.CBal
                    };
                    return cob;
                }
               /* if (value.AccountId % 2 == 0)
                {
                    var sad = savingsAccounts.Find(a => a.SAId == value.AccountId);
                    sad.SBal = sad.SBal + value.Balance;
                    AccountMsg sobd = new AccountMsg
                    {
                        AccId = value.AccountId,
                        AccType = "Deposited Correctly",
                        AccBal = sad.SBal
                    };
                    return sobd;
                }
                var cad = currentAccounts.Find(a => a.CAId == value.AccountId);
                cad.CBal = cad.CBal + value.Balance;
                AccountMsg cobd = new AccountMsg
                {
                    AccId = value.AccountId,
                    AccType = "Deposited Correctly",
                    AccBal = cad.CBal
                };
                return cobd;*/
            }
            return null;
        }

        public AccountMsg withdraw(dwacc value)
        {
            string data = JsonConvert.SerializeObject(value);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Transaction/withdraw/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                Ts s1 = JsonConvert.DeserializeObject<Ts>(data1);
                AccountMsg amsg = new AccountMsg();
                if (s1.Message == "No Warning")
                {
                    if (value.AccountId % 2 == 0)
                    {
                        var sa = savingsAccounts.Find(a => a.SAId == value.AccountId);
                        sa.SBal = sa.SBal - value.Balance;
                        if (sa.SBal >= 0)
                        {
                            amsg.AccId = value.AccountId;
                            amsg.AccType = "Withdrawn Successfully";
                            amsg.AccBal = sa.SBal;
                            return amsg;
                        }
                        else
                        {
                            sa.SBal = sa.SBal + value.Balance;
                            amsg.AccId = value.AccountId;
                            amsg.AccType = "Insufficient Fund";
                            amsg.AccBal = sa.SBal;
                            return amsg;
                        }
                    }
                    var car = currentAccounts.Find(a => a.CAId == value.AccountId);
                    car.CBal = car.CBal - value.Balance;
                    if (car.CBal >= 0)
                    {
                        amsg.AccId = value.AccountId;
                        amsg.AccType = "Withdrawn Successfully";
                        amsg.AccBal = car.CBal;
                        return amsg;
                    } 
                    else
                    {
                        car.CBal = car.CBal + value.Balance;
                        amsg.AccId = value.AccountId;
                        amsg.AccType = "Insufficient Fund";
                        amsg.AccBal = car.CBal;
                        return amsg;
                    }

                }
                if (value.AccountId % 2 == 0)
                {
                    var sa = savingsAccounts.Find(a => a.SAId == value.AccountId);
                    sa.SBal = sa.SBal - value.Balance;
                    if (sa.SBal >= 0)
                    {
                        amsg.AccId = value.AccountId;
                        amsg.AccType = "Withdrawn Successfully.Service charge applicable at the end of month";
                        amsg.AccBal = sa.SBal;
                        return amsg;
                    }
                    else
                    {
                        sa.SBal = sa.SBal + value.Balance;
                        amsg.AccId = value.AccountId;
                        amsg.AccType = "Insufficient Fund";
                        amsg.AccBal = sa.SBal;
                        return amsg;
                    }
                }
                var ca = currentAccounts.Find(a => a.CAId == value.AccountId);
                ca.CBal = ca.CBal - value.Balance;
                if (ca.CBal >= 0)
                {
                    amsg.AccId = value.AccountId;
                    amsg.AccType = "Withdrawn Successfully.Service Charge Applicable at the end of month";
                    amsg.AccBal = ca.CBal;
                    return amsg;
                }    
                else
                {
                    ca.CBal = ca.CBal + value.Balance;
                    amsg.AccId = value.AccountId;
                    amsg.AccType = "Insufficient Fund";
                    amsg.AccBal = ca.CBal;
                    return amsg;
                }
            }
            return null;
        }

        public transactionmsg transfer(transfers value)
        {
            double sb = 0.0, db = 0.0;
            string data = JsonConvert.SerializeObject(value);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Transaction/transfer/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                Ts s1 = JsonConvert.DeserializeObject<Ts>(data1);
                transactionmsg ob = new transactionmsg();
                if (s1.Message == "No Warning")
                {
                    if (value.source_accid % 2 == 0)
                    {
                        var sas = savingsAccounts.Find(a => a.SAId == value.source_accid);
                        sas.SBal = sas.SBal - value.amount;
                        if (sas.SBal >= 0)
                            sb = sas.SBal;
                        else
                        {
                            sas.SBal = sas.SBal + value.amount;
                            return null;
                        }
                    }
                    else
                    {
                        var cas = currentAccounts.Find(a => a.CAId == value.source_accid);
                        cas.CBal = cas.CBal - value.amount;
                        if (cas.CBal >= 0)
                            sb = cas.CBal;
                        else
                        {
                            cas.CBal = cas.CBal + value.amount;
                            return null;
                        }

                    }
                    if (value.destination_accid % 2 == 0)
                    {
                        var sa = savingsAccounts.Find(a => a.SAId == value.destination_accid);
                        sa.SBal = sa.SBal + value.amount;
                        db = sa.SBal;
                    }
                    else
                    {
                        var ca = currentAccounts.Find(a => a.CAId == value.destination_accid);
                        ca.CBal = ca.CBal + value.amount;
                        db = ca.CBal;
                    }
                    ob.sbal = sb;
                    ob.rbal = db;
                    ob.transferStatus = "Transfer Successfull";
                    return ob;
                }
                else
                {
                    if (value.source_accid % 2 == 0)
                    {
                        var sas = savingsAccounts.Find(a => a.SAId == value.source_accid);
                        sas.SBal = sas.SBal - value.amount;
                        if (sas.SBal >= 0)
                            sb = sas.SBal;
                        else
                        {
                            sas.SBal = sas.SBal + value.amount;
                            return null;
                        }

                    }
                    else
                    {
                        var cas = currentAccounts.Find(a => a.CAId == value.source_accid);
                        cas.CBal = cas.CBal - value.amount;
                        if (cas.CBal >= 0)
                            sb = cas.CBal;
                        else
                        {
                            cas.CBal = cas.CBal + value.amount;
                            return null;
                        }

                    }
                    if (value.destination_accid % 2 == 0)
                    {
                        var sa = savingsAccounts.Find(a => a.SAId == value.destination_accid);
                        sa.SBal = sa.SBal + value.amount;
                        db = sa.SBal;
                    }
                    else
                    {
                        var ca = currentAccounts.Find(a => a.CAId == value.destination_accid);
                        ca.CBal = ca.CBal + value.amount;
                        db = ca.CBal;
                    }
                    ob.sbal = sb;
                    ob.rbal = db;
                    ob.transferStatus = "Transfer Successfull.But Service Charge is applicable in sender account";
                    return ob;
                    //return "Sender Account Balance Rs." + sb + ".00\n" + "Receiver Account Balance Rs." + db + ".00\n but service charge will be deducted at the end of month from your account";

                }

            }
            return null;
        }
    }
    
}
