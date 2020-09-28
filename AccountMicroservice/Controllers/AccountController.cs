using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AccountMicroservice.Models;
using AccountMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        IAccountRep db;
        public AccountController(IAccountRep _db)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(AccountController));
            db = _db;
        }
        //int acid = 1;
        /*public static List<customeraccount> customeraccounts = new List<customeraccount>()
        {
            new customeraccount{custId=1,CAId=101,SAId=102}
        };*/
        // GET: api/<AccountController>
        [HttpGet]
        [Route("getCurrentAccountList")]
        public IActionResult GetCurrent()
        {
            try
            {
                var ob = db.GetCurrent();
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
        [HttpGet]
        [Route("getSavingAccountList")]
        public IActionResult GetSavings()
        {
            try
            {
                var ob = db.GetSavings();
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
        // GET api/<AccountController>/5
        [HttpGet]
        [Route("getCustomerAccounts/{id}")]
        public IActionResult getCustomerAccounts(int id)
        {
            _log4net.Info(" Got Customer Account");
            try
            {
                var ob = db.getCustomerAccounts(id);
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

        // POST api/<AccountController>
        [HttpPost]
        [Route("createAccount")]
        public IActionResult createAccount([FromBody] int id)
        {
            //GetListRep ob = new GetListRep();
            //var customeraccounts = ob.GetCustomeraccountsList();
            _log4net.Info("Account Created");
            try
            {
                var ob = db.createAccount(id);
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
        [HttpGet]
        [Route("getAccount/{id}")]
        public IActionResult getAccount(int id)
        {
            //GetListRep ob = new GetListRep();
            //var customeraccounts = ob.GetCustomeraccountsList();
            _log4net.Info(" Getting Account Info");
            try
            {
                var ob = db.getAccount(id);
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
        [HttpGet]
        [Route("getAccountStatement/{AccountId}/{from_date}/{to_date}")]
        public IActionResult getAccountStatement(int AccountId, int from_date, int to_date)
        {
            _log4net.Info("Account Statement Shown");
            try
            {
                var ob = db.getAccountStatement(AccountId, from_date, to_date);
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
        [Route("deposit")]
        public IActionResult deposit([FromBody] dwacc value)
        {
            _log4net.Info(" Amount Deposited ");
            try
            {
                var ob = db.deposit(value);
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
        [Route("withdraw")]
        public IActionResult withdraw([FromBody] dwacc value)
        {
            _log4net.Info("Amount Withdrawn");
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
            _log4net.Info("Amount Transferred");
            try
            {
                var ob = db.transfer(value);
                if (ob == null)
                    return NotFound("Either Zero Balance or Link Failure");
                return Ok(ob);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
