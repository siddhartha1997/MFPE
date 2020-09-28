using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RulesMicroservice;
using RulesMicroservice.Controllers;
using RulesMicroservice.Repository;
using System;
using System.Collections.Generic;

namespace RulesTesting
{
    public class Tests
    {
        dwacc d1 = new dwacc { AccountId = 1, Balance = 2000 };
        List<RuleStatus> rul = new List<RuleStatus>();
        List<RulesMsg> rm = new List<RulesMsg>();
        ServiceCharge s1 = new ServiceCharge { servicechargebal = 200 };
        [SetUp]
        public void Setup()
        {
            rul = new List<RuleStatus>()
            {
                new RuleStatus{Message="Service Charge Applicable"},
                new RuleStatus{Message="Service Charge Not Applicable"}
            };

            rm = new List<RulesMsg>()
            {
                new RulesMsg{AccId=1,AccBal=2000,Message="Service Not Charge Applicable"},
                new RulesMsg{AccId=2,AccBal=500,Message="Service Charge Applicable"}
            };
        }

        [Test]
        public void getServiceCharges()
        {
            var mock = new Mock<IRulesRep>();
            mock.Setup(p => p.getServiceCharges()).Returns(s1);
            RulesController obj = new RulesController(mock.Object);
            var data = obj.getServiceCharges();
            var res = data as OkObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }

        [Test]
        public void getNullServiceCharges()
        {
            try { 
            var mock = new Mock<IRulesRep>();
            mock.Setup(p => p.getServiceCharges()).Returns(s1);
            RulesController obj = new RulesController(mock.Object);
            var data = obj.getServiceCharges();
            var res = data as BadRequestResult;
            Assert.AreEqual(400, res.StatusCode);
             }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void evaluateCorrectMinBal_savings()
        {
            try
            {
                var mock = new Mock<IRulesRep>();
                mock.Setup(p => p.evaluateMinBalSavings()).Returns(rm);
                RulesController obj = new RulesController(mock.Object);
                var data = obj.evaluateMinBalSavings();
                var res = data as OkObjectResult;
                Assert.AreEqual(200, res.StatusCode);
            }
            catch(Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }
        [Test]
        public void evaluateInCorrectMinBal_savings()
        {
            try
            {
                var mock = new Mock<IRulesRep>();
                mock.Setup(p => p.evaluateMinBalSavings()).Returns(rm);
                RulesController obj = new RulesController(mock.Object);
                var data = obj.evaluateMinBalSavings();
                var res = data as BadRequestObjectResult;
                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void evaluateCorrectMinBal_Current()
        {
            try
            {
                var mock = new Mock<IRulesRep>();
                mock.Setup(p => p.evaluateMinBalCurrent()).Returns(rm);
                RulesController obj = new RulesController(mock.Object);
                var data = obj.evaluateMinBalCurrent();
                var res = data as OkObjectResult;
                Assert.AreEqual(200, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void evaluate_InCorrectMinBal_Current()
        {
            try
            {
                var mock = new Mock<IRulesRep>();
                mock.Setup(p => p.evaluateMinBalCurrent()).Returns(rm);
                RulesController obj = new RulesController(mock.Object);
                var data = obj.evaluateMinBalCurrent();
                var res = data as BadRequestObjectResult;
                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }
    }
}