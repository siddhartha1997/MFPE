
using CustomerMicroservice.Controllers;
using CustomerMicroservice.Models;
using CustomerMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CustomerTesting
{
    public class Tests
    {
        List<Customer> c1;
        
        Customer customer=new Customer {CustId=2, Name="DK",Address="Dumdum",DOB="05-09-1997",PanNo="CGLBP002"};
       
       CustomerCreationStatus status = new CustomerCreationStatus { CustomerId = 1, Message = "Success. Current and Savings account also created" };
        [SetUp]
        public void Setup()
        {
            c1 = new List<Customer>()
            {
                new Customer{CustId=1, Name="SB",Address="Dumdum",DOB="05-09-1997",PanNo="CGLBP002"},
                 new Customer{CustId=2, Name="DK",Address="Dumdum",DOB="05-09-1997",PanNo="CGLBP002"},
                  new Customer{CustId=3, Name="SS",Address="Dumdum",DOB="05-09-1997",PanNo="CGLBP002"}

            };
        }

        [Test]
        public void getcustomers_ValidInputPassed_OkRequest()
        {
           // int id = 1;
            
            var mock = new Mock<CustomerRep>();
           CustomerController obj = new CustomerController(mock.Object);
            var data = obj.getCustomerDetails(1);
            var res = data as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void GetCustomer_InvalidInputPassed_ReturnsBadRequest()
        {
            // int id = 1;

            var mock = new Mock<CustomerRep>();
            CustomerController obj = new CustomerController(mock.Object);
            var data = obj.getCustomerDetails(0);
            var res = data as ObjectResult;
            Assert.AreEqual(404, res.StatusCode);
        }

        [Test]
        public void Getcustomers_ReturnsNotnullList()
        {
            // int id = 1;

            var mock = new Mock<CustomerRep>();
            CustomerController obj = new CustomerController(mock.Object);
            var data = obj.getCustomerDetails(1);
            var res = data as ObjectResult;
            Assert.IsNotNull(data);
        }

        

        [Test]
        public void Createcustomers_ValidInputPassed_ReturnsOkResult()
        {
           

            var mock = new Mock<ICustomerRep>();
            mock.Setup(p => p.createCustomer(customer)).Returns(status);
            CustomerController obj = new CustomerController(mock.Object);
            var data = obj.createCustomer(customer);
            var res = data as ObjectResult;
            Assert.AreEqual(200,res.StatusCode);
        }

        [Test]
        public void Createcustomers_InvalidInputPassed_ReturnsBadRequest()
        {



            var mock = new Mock<CustomerRep>();
           
            CustomerController obj = new CustomerController(mock.Object);
            Customer cz = new Customer();
            var data = obj.createCustomer(cz);
            var res = data as BadRequestResult;
            Assert.AreEqual(400, res.StatusCode);


        }
    }
}