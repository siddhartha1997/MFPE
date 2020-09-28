using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesMicroservice.Repository
{
    public interface IRulesRep
    {
        public List<RulesMsg> evaluateMinBalCurrent();
        public List<RulesMsg> evaluateMinBalSavings();
        public ServiceCharge getServiceCharges();
        
        public RuleStatus evaluateMinBal(dwacc value);



    }
}
