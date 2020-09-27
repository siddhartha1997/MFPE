﻿using AuthenticateMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticateMicroservice.Repository
{
    public interface ITokenRep
    {
        public Authenticate AuthenticateUser(Authenticate login);
        public string GenerateJSONWebToken(Authenticate userInfo);
    }
}
