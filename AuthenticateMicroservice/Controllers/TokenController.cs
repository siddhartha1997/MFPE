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
        ITokenRepository tokenRepository;
        public TokenController(ITokenRepository _tokenRepository)
        {
            tokenRepository = _tokenRepository;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Authenticate login)
        {
            _log4net.Info("Authentication initiated");
            IActionResult response = Unauthorized();
            var user = tokenRepository.AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = tokenRepository.GenerateJSONWebToken(user);
                _log4net.Info("Token Generated");
                response = Ok(tokenString);
            }
            return response;
        }
    }
}