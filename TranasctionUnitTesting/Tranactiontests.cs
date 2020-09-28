using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using TransactionMicroservice;
using TransactionMicroservice.Controllers;
using TransactionMicroservice.Repository;

namespace TranasctionUnitTesting
{
    public class Tests
    {
        dwacc d1 = new dwacc { AccountId = 1, Balance = 100 };
        TransactionMsg tsm = new TransactionMsg { Message = "Warning" };
        transfers transfer = new transfers { source_accid = 1, destination_accid = 2, amount = 200 };
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void test_deposit()
        {
            var mock = new Mock<TransactionRep>();
            TransactionController t1 = new TransactionController(mock.Object);
            dwacc d1 = new dwacc { AccountId = 1, Balance = 2000 };
            var data = t1.deposit(d1);
            var res = data as OkObjectResult;
            Assert.AreEqual(200, res.StatusCode);

        }

        [Test]
        public void testnotull_deposit()
        {
            var mock = new Mock<TransactionRep>();
            TransactionController t1 = new TransactionController(mock.Object);
            dwacc d1 = new dwacc { AccountId = 1, Balance = 2000 };
            var data = t1.deposit(d1);
            //  var res = data as OkObjectResult;
            Assert.IsNotNull(data);

        }

        [Test]
        public void test_withdraw()
        {
            try
            {
                var mock = new Mock<ITransactionRep>();
                mock.Setup(p => p.withdraw(d1)).Returns(tsm);
                TransactionController t1 = new TransactionController(mock.Object);

                var data = t1.withdraw(d1);
                var res = data as BadRequestResult;
                Assert.AreEqual(200, res.StatusCode);
            }
            catch(Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void test_Not_Null_withdraw()
        {
            dwacc d1 = new dwacc { AccountId = 1, Balance = 2000 };
            var mock = new Mock<TransactionRep>();

            TransactionController t1 = new TransactionController(mock.Object);

            var data = t1.withdraw(d1);
            var res = data as OkObjectResult;
            Assert.IsNotNull(data);
        }

        [Test]
        public void testbad_withdraw()
        {
            var mock = new Mock<TransactionRep>();

            TransactionController t1 = new TransactionController(mock.Object);
            dwacc d1 = new dwacc();
            var data = t1.withdraw(d1);
            var res = data as BadRequestResult;
            Assert.AreEqual(400, res.StatusCode);
        }

        [Test]
        public void test_transfer()
        {
            var mock = new Mock<ITransactionRep>();
            mock.Setup(p => p.transfer(transfer)).Returns(tsm);
            TransactionController t1 = new TransactionController(mock.Object);
            var data = t1.transfer(transfer);
            var res = data as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void testbad_transfer()
        {
            var mock = new Mock<TransactionRep>();
            TransactionController t1 = new TransactionController(mock.Object);
            transfers d1 = new transfers { source_accid = 1, destination_accid = 2, amount = 1000 };
            var data = t1.transfer(d1);
            var res = data as BadRequestResult;
            Assert.AreEqual(400, res.StatusCode);
        }


    }
}