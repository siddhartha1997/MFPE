using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Repository
{
    public interface ICustomerRepository
    {
        public customerCreationStatus createCustomer(customerDetails customer);
        public customerDetails getCustomerDetails(int customerId);
    }
}
