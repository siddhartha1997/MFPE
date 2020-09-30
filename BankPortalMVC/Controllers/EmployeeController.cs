using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BankPortalMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BankPortalMVC.Controllers
{
    public class EmployeeController : Controller
    {
        HttpClient client;
        public EmployeeController()
        {
            client = new HttpClient();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            string token = TokenInfo.StringToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string data = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("http://20.193.136.74/api/Customer/createCustomer", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                CustomerCreationStatus ob4 = JsonConvert.DeserializeObject<CustomerCreationStatus>(data1);
                return RedirectToAction("CreationStatus", ob4);
            }
            return BadRequest();
        }
        public IActionResult CreationStatus(CustomerCreationStatus ob4)
        {
            return View(ob4);
        }
        public IActionResult GetCustomerAccountDetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetCustomerAccountDetails(customerid cid)
        {
            return RedirectToAction("AccountStatus", cid);
            /*int acid = cid.id;
            //var ac = new List<AccountMsg>();
            HttpResponseMessage response = client.GetAsync("http://20.193.128.123/api/Account/getCustomerAccounts/" + acid).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<AccountMsg> ac = JsonConvert.DeserializeObject<List<AccountMsg>>(data);
                return RedirectToAction("AccountStatus", ac);
            }
            return BadRequest();*/
        }
        public IActionResult AccountStatus(customerid cid)
        {
            //List<AccountMsg> abc = ac;
            int acid = cid.id;
            HttpResponseMessage response = client.GetAsync("http://20.193.128.123/api/Account/getCustomerAccounts/" + acid).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<AccountMsg> ac = JsonConvert.DeserializeObject<List<AccountMsg>>(data);
                return View(ac);
            }
            return BadRequest();
        }
        public IActionResult CurrentAccountChecking()
        {
            HttpResponseMessage response = client.GetAsync("https://bankrule1997.azurewebsites.net/api/Rules/deductServiceChargeCurrent").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<RulesMsg> ac = JsonConvert.DeserializeObject<List<RulesMsg>>(data);
                return View(ac);
            }
            return BadRequest();
        }
        public IActionResult SavingsAccountChecking()
        {
            HttpResponseMessage response = client.GetAsync("https://bankrule1997.azurewebsites.net/api/Rules/deductServiceChargeSavings").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<RulesMsg> ac = JsonConvert.DeserializeObject<List<RulesMsg>>(data);
                return View(ac);
            }
            return BadRequest();
        }
    }
}
