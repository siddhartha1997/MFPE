using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TransactionMicroservice.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransactionMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TransactionController));
        /*Uri baseAddress = new Uri("https://localhost:44356/api");   //Port No.
        HttpClient client;

        public TransactionController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;

        }
        // GET: api/<TransactionController>
        /* [HttpGet]
         public IEnumerable<string> Get()
         {
             return new string[] { "value1", "value2" };
         }

         // GET api/<TransactionController>/5
         [HttpGet("{id}")]
         public string Get(int id)
         {
             return "value";
         }

         // POST api/<TransactionController>
         [HttpPost]
         public void Post([FromBody] string value)
         {
         }

         // PUT api/<TransactionController>/5
         [HttpPut("{id}")]
         public void Put(int id, [FromBody] string value)
         {
         }

         // DELETE api/<TransactionController>/5
         [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }*/
        ITransactionRep db;
        public TransactionController(ITransactionRep _db)
        {
            db = _db;
        }
        [HttpPost]
        [Route("deposit")]
        public IActionResult deposit([FromBody]dwacc value)
        {
            try
            {
                var ob = db.deposit(value);
                if(ob==null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("withdraw")]
        public IActionResult withdraw([FromBody] dwacc value)
        {
            /* string data = JsonConvert.SerializeObject(value);
             StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

             HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Rules/evaluateMinBal/", content).Result;
             if (response.IsSuccessStatusCode)
             {
                 string data1 = response.Content.ReadAsStringAsync().Result;
                 if (data1 == "Warning")
                 {
                     return "Warning";
                 }
                 return "No Warning";
             }*/
            try
            {
                var ob = db.withdraw(value);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("transfer")]
        public IActionResult transfer([FromBody] transfers value)
        {
            /*dwacc sa = new dwacc
            {
                AccountId = value.source_accid,
                Balance = value.amount
            };
         /*   string data = JsonConvert.SerializeObject(sa);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Rules/evaluateMinBal/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                if(data1=="Warning")
                {
                    return "Warning";
                }
                return "No Warning";
            }*/
            try
            {
                var ob = db.transfer(value);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
