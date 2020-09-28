using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerMicroservice.Models;
using CustomerMicroservice.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CustomerMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CustomerController));
        ICustomerRepository db;
        public CustomerController(ICustomerRepository _db)
        {
            db = _db;
        }
        [HttpGet]
        [Route("getCustomerDetails/{id}")]
        public IActionResult getCustomerDetails(int id)
        {
            _log4net.Info("Get Customer Details is called with id:" + id);
            try
            {
                var obj = db.getCustomerDetails(id);
                if(obj == null)
                {
                    return NotFound();
                }
                return Ok(obj);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("createCustomer")]
        public IActionResult createCustomer([FromBody] customerDetails customer)
        {
            _log4net.Info("Creation of customer is initiated");
            try
            {
                var obj = db.createCustomer(customer);
                if(obj==null)
                {
                    return NotFound();
                }
                return Ok(obj);
                //_log4net.Info("New Customer account is created");
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
