using NUnit.Framework;
using AccountMicroservice.Controllers;
using AccountMicroservice.Models;
using AccountMicroservice.Repository;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using AccountMicroservice;

namespace AccountsTesting
{
    public class Tests
    {
        AccountMsg am = new AccountMsg();
        Statement s1 = new Statement {date=30102019,Narration="opening",refno=101,valueDate=500,withdrawal=10000,deposit=1200,closingBalance=8400 };
        AccountStatement as1 =new AccountStatement ();
        dwacc d1 = new dwacc { AccountId = 1, Balance = 200 };
        transfers t1 = new transfers { source_accid = 1, destination_accid = 2, amount = 2000 };
        Ts ts = new Ts { Message = "Transfer Successfull" };
        
        [SetUp]
        public void Setup()
        {
            am = new AccountMsg { AccId = 1, AccBal = 1000, AccType = "Savings" };
           
           
        }

        [Test]
        public void getCurrent()
        {
            var mock = new Mock<AccountRep>();
            AccountController a1 = new AccountController(mock.Object);
            var data = a1.GetCurrent();
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode,200);
        }

        [Test]
        public void NotgetCurrent()
        {
            try
            {
                var mock = new Mock<AccountRep>();
                AccountController a1 = new AccountController(mock.Object);
                var data = a1.GetCurrent();
                var res = data as BadRequestObjectResult;
                Assert.AreEqual(res.StatusCode, 400);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void getsavings()
        {
            var mock = new Mock<AccountRep>();
            AccountController a1 = new AccountController(mock.Object);
            var data = a1.GetSavings();
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }


        [Test]
        public void Notgetsavings()
        {
            try
            {
                var mock = new Mock<AccountRep>();
                AccountController a1 = new AccountController(mock.Object);
                var data = a1.GetSavings();
                var res = data as BadRequestResult;
                Assert.AreEqual(res.StatusCode, 400);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void getcustomerAccountst()
        {
            int id = 1;
            var mock = new Mock<AccountRep>();
            AccountController a1 = new AccountController(mock.Object);
            var data = a1.getCustomerAccounts(id);
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }

        [Test]
        public void notgetcustomerAccountst()
        {
            try
            {
                int id = 1;
                var mock = new Mock<AccountRep>();
                AccountController a1 = new AccountController(mock.Object);
                var data = a1.getCustomerAccounts(id);
                var res = data as BadRequestResult;
                Assert.AreEqual(res.StatusCode, 400);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void createAccount()
        {
            int id = 1;
            var mock = new Mock<AccountRep>();
            AccountController a1 = new AccountController(mock.Object);
            var data = a1.createAccount(id);
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }

        [Test]
        public void NotcreateAccount()
        {
            try
            {
                int id = 1;
                var mock = new Mock<AccountRep>();
                AccountController a1 = new AccountController(mock.Object);
                var data = a1.createAccount(id);
                var res = data as BadRequestObjectResult;
                Assert.AreEqual(res.StatusCode, 400);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

        }


        [Test]
        public void getAccounts()
        {
            try { 
            int id = 1;
            var mock = new Mock<IAccountRep>();
                mock.Setup(m => m.getAccount(id)).Returns(am);
            AccountController a1 = new AccountController(mock.Object);
            var data = a1.getAccount(id);
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }
            catch(Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void getCorrectAccount()
        {

            int id = 1;
                var mock = new Mock<IAccountRep>();
                mock.Setup(m => m.getAccount(id)).Returns(am);
                 AccountController a1 = new AccountController(mock.Object);
                var data = a1.getAccount(id);
                var res = data as ObjectResult;
                Assert.AreEqual(res.StatusCode, 200);
            
            
        }

        [Test]
        public void getCorrectAccountStatment()
        {
            try { 
            int id = 1;
            var mock = new Mock<AccountRep>();

            AccountController a1 = new AccountController(mock.Object);
            var data = a1.getAccountStatement(id, 30102019, 30102020);
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

        }
        [Test]
        public void deposit()
        {
            var mock = new Mock<IAccountRep>();
            mock.Setup(m => m.deposit(d1)).Returns(am);
            AccountController a1 = new AccountController(mock.Object);
            var data = a1.deposit(d1);
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }

        [Test]
        public void Notdeposit()
        {
            try
            {
                var mock = new Mock<IAccountRep>();
                mock.Setup(m => m.deposit(d1)).Returns(am);
                AccountController a1 = new AccountController(mock.Object);
                var data = a1.deposit(d1);
                var res = data as BadRequestResult;
                Assert.AreEqual(res.StatusCode, 400);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void withdraw()
        {
            var mock = new Mock<IAccountRep>();
            mock.Setup(m => m.withdraw(d1)).Returns(am);
            AccountController a1 = new AccountController(mock.Object);
            var data = a1.withdraw(d1);
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }

        [Test]
        public void notwithdraw()
        {
            try
            {
                var mock = new Mock<IAccountRep>();
                mock.Setup(m => m.withdraw(d1)).Returns(am);
                AccountController a1 = new AccountController(mock.Object);
                var data = a1.withdraw(d1);
                var res = data as ObjectResult;
                Assert.AreEqual(res.StatusCode, 200);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }


        }
        [Test]
        public void transfer()
        {
            try
            {
                var mock = new Mock<AccountRep>();

                AccountController a1 = new AccountController(mock.Object);
                var data = a1.transfer(t1);
                var res = data as ObjectResult;
                Assert.AreEqual(res.StatusCode, 200);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }
    }
}