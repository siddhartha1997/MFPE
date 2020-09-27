using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Repository
{
    public interface ICustomerRep
    {
        public CustomerCreationStatus createCustomer(Customer customer);
        public Customer getCustomerDetails(int CustId);
    }
}
