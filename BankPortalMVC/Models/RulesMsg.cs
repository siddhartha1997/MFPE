﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankPortalMVC.Models
{
    public class RulesMsg
    {
        public int AccId { get; set; }
        public double AccBal { get; set; }
        public string Message { get; set; }
    }
}
