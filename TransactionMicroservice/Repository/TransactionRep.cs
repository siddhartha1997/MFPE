using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TransactionMicroservice.Repository
{
    public class TransactionRep : ITransactionRep
    {
         Uri baseAddress = new Uri("https://localhost:44356/api");   //Port No.
        HttpClient client;

        public TransactionRep()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;

        }
        public TransactionMsg deposit(dwacc value)
        {
            TransactionMsg ob = new TransactionMsg();
            ob.Message = "Success";
            return ob;
        }

        public TransactionMsg transfer(transfers value)
        {
            dwacc ob4 = new dwacc { AccountId = value.source_accid, Balance = value.amount };
            TransactionMsg ob = new TransactionMsg();
            string data = JsonConvert.SerializeObject(ob4);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Rules/evaluateMinBal/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                RuleStatus s1 = JsonConvert.DeserializeObject<RuleStatus>(data1);
                if (s1.Message == "Warning")
                {
                    ob.Message = "Warning";
                    return ob;
                }
                ob.Message = "No Warning";
                return ob;
            }



            return null;



        }

        public TransactionMsg withdraw(dwacc value)
        {
            TransactionMsg ob = new TransactionMsg();
            string data = JsonConvert.SerializeObject(value);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Rules/evaluateMinBal/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                RuleStatus s1 = JsonConvert.DeserializeObject<RuleStatus>(data1);
                if (s1.Message == "Warning")
                {
                    ob.Message = "Warning";
                    return ob;
                }
                ob.Message = "No Warning";
                return ob;
            }
            
            
            
            return null;
        }
    }
}
