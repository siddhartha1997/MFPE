using AuthenticateMicroservice.Controllers;
using AuthenticateMicroservice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using AuthenticateMicroservice.Model;
using AuthenticateMicroservice.Repository;

namespace AuthenticateMicroserviceUnit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsTokenNotNullIsTokenNotNull_When_ValidUserCredentialsAreUsed()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            var TokenObj = new TokenRepository(config.Object);
            var Result = TokenObj.AuthenticateUser(new Authenticate { userId = 1, Password = "1234", Roles = "Employee" });
            Assert.IsNotNull(Result);
        }

        [Test]
        public void IsTokenNull_When_InvalidUserCredentialsAreUsed()
        {
            Mock<IConfiguration> config = new Mock<IConfiguration>();
            var TokenObj = new TokenRepository(config.Object);
            var Result = TokenObj.AuthenticateUser(new Authenticate() { userId = 1, Password = "12345",Roles="Employee" });
            Assert.IsNull(Result);
        }
    }
}