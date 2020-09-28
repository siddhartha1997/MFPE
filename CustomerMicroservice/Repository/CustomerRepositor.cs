using CustomerMicroservice.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMicroservice.Repository
{
    public class CustomerRepositor : ICustomerRepository
    {
        public static List<customerDetails> customers = new List<customerDetails>
        {
            new customerDetails{customerId = 2,name="SB",address="Dumdum",dateOfBirth="05-09-1997",panNumber="CGLBP002"}
        };
        //int CustId = 1;
        Uri baseAddress = new Uri("https://localhost:44379/api/Account");
        HttpClient client;
        public CustomerRepositor()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public customerCreationStatus createCustomer(customerDetails customer)
        {
            customers.Add(customer);
            string data = JsonConvert.SerializeObject(customer.customerId);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/createAccount/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                customerAccountDetails ob4 = JsonConvert.DeserializeObject<customerAccountDetails>(data1);
                var ob = new customerCreationStatus();
                ob.customerId = customer.customerId;
                ob.message = "Success. Current and Savings account also created";
                ob.currentAccountId = ob4.currentAccountId;
                ob.savingsAccountId = ob4.savingAccountId;
                return ob;
            }
            return null;
        }

        public customerDetails getCustomerDetails(int customerId)
        {
            return customers.Where(c => c.customerId == customerId).FirstOrDefault();          
        }
    }
}
