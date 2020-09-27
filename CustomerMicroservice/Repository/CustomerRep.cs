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
    public class CustomerRep : ICustomerRep
    {
        public static List<Customer> customers = new List<Customer>
        {
            new Customer{CustId = 2,Name="SB",Address="Dumdum",DOB="05-09-1997",PanNo="CGLBP002"}
        };
        //int CustId = 1;
        Uri baseAddress = new Uri("https://localhost:44379/api/Account");
        HttpClient client;
        public CustomerRep()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public CustomerCreationStatus createCustomer(Customer customer)
        {
            customers.Add(customer);
            string data = JsonConvert.SerializeObject(customer.CustId);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/createAccount/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                customeraccount ob4 = JsonConvert.DeserializeObject<customeraccount>(data1);
                var ob = new CustomerCreationStatus();
                ob.CustomerId = customer.CustId;
                ob.Message = "Success. Current and Savings account also created";
                ob.CurrentAccountId = ob4.CAId;
                ob.SavingsAccountId = ob4.SAId;
                //CustId = CustId + 2;
                return ob;
            }
            return null;
        }

        public Customer getCustomerDetails(int CustId)
        {
            return customers.Where(c => c.CustId == CustId).FirstOrDefault();          
        }
    }
}
