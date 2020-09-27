using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using AuthenticateMicroservice.Model;
using AuthenticateMicroservice.Repository;

namespace AuthenticateMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenController));
        //private IConfiguration _config;
        ITokenRep db;
        public TokenController(/*IConfiguration config*/ITokenRep _db)
        {
            //_config = config;
            db = _db;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Authenticate login)
        {

            _log4net.Info("Authentication initiated");
            IActionResult response = Unauthorized();
            var user = db.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = db.GenerateJSONWebToken(user);
                response = Ok(tokenString);
            }
            return response;
        }
        /*private string GenerateJSONWebToken(Authenticate userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /*private Authenticate AuthenticateUser(Authenticate login)
        {
            Authenticate user = null;
            List<Authenticate> ob = new List<Authenticate>
            {
                new Authenticate{UserID=1,Password="1234",Roles="Employee"},
                new Authenticate{UserID=2,Password="12345",Roles="Customer"}
            };
            foreach (var v in ob)
            {
                if (v.UserID == login.UserID && v.Password == login.Password)
                {
                    user = new Authenticate { UserID = login.UserID, Password = login.Password };
                }
            }
            return user;
        }*/
    }
}