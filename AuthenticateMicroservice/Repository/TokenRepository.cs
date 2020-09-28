using AuthenticateMicroservice.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticateMicroservice.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private IConfiguration _config;
        public TokenRepository(IConfiguration config)
        {
            _config = config;
        }
        public static List<Authenticate> clientList = new List<Authenticate>
        {
            new Authenticate{userId=1,Password="1234",Roles="Employee"},
            new Authenticate{userId=2,Password="12345",Roles="Customer"}
        };
        public Authenticate AuthenticateUser(Authenticate login)
        {
            Authenticate user = null;
            foreach (var v in clientList)
            {
                if (v.userId == login.userId && v.Password == login.Password)
                {
                    user = new Authenticate { userId = login.userId, Password = login.Password };
                }
            }
            return user;
        }
        public string GenerateJSONWebToken(Authenticate userInfo)
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
    }
}
