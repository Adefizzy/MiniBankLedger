
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Input;
using MiniBankLedger.Services;

namespace MiniBankLedger.Controllers;

class CustomerController(CustomerService customerService)
{
    public Customer? CreateCustomer()
    {

        CustomerDto customerDto = CustomerInput.CreateCustomerInput();

        return customerService.CreateCustomer(customerDto);

    }
}
