using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BankPortalMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BankPortalMVC.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44373/api");
        HttpClient client;
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Auth(User user)
        {
            string content = JsonConvert.SerializeObject(user);
            StringContent data = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                response = client.PostAsync(client.BaseAddress + "/Token", data).Result;
            }
            catch
            {
                return RedirectToAction("Error");
            }
            string token = response.Content.ReadAsStringAsync().Result;
            if (token != "error")
            {
                TokenInfo.StringToken = token;
                TokenInfo.UserID = user.UserID;
                if (user.Roles == "Customer")
                    return RedirectToAction("Index", "Customer");
                else
                    return RedirectToAction("Index", "Employee");

            }
            TokenInfo.StringToken = "";
            TokenInfo.UserID = 0;
            return RedirectToAction("Error");
        }
        public IActionResult Logout()
        {
            TokenInfo.StringToken = null;
            TokenInfo.UserID = 0;
            return RedirectToAction("Login");
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}
