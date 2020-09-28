using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RulesMicroservice.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RulesMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RulesController));
        IRulesRep db;
        public RulesController(IRulesRep _db)
        {
            db = _db;
        }
        // GET: api/<RulesController>
        /*    [HttpGet]
            public IEnumerable<Account> Get()
            {
                _log4net.Info("Account List Obtained");
                return accountList;
            }*/

        // GET api/<RulesController>/5
        [HttpGet]
        [Route("deductServiceChargeCurrent")]
        public IActionResult evaluateMinBalCurrent()
        {
            _log4net.Info("Minimum Balance for current Account Initiated");
            try
            {
                var ob = db.evaluateMinBalCurrent();
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
        [HttpGet]
        [Route("deductServiceChargeSavings")]
        public IActionResult evaluateMinBalSavings()
        {
            _log4net.Info("Minimum Balance for current Account Initiated");
            try
            {
                var ob = db.evaluateMinBalSavings();
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
            //return ls1;
        }
        [HttpGet]
        [Route("showServiceCharges")]
        public IActionResult getServiceCharges()
        {
            _log4net.Info("Service Charges logged");
            try
            {
                var ob = db.getServiceCharges();
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
        [Route("evaluateMinBal")]
        public IActionResult evaluateMinBal([FromBody] dwacc value)
        {
            //dwacc ob = new dwacc { AccountId = id, Balance = bid };
            try
            {
                var obj = db.evaluateMinBal(value);
                if (obj == null)
                {
                    return NotFound();
                }
                return Ok(obj);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
