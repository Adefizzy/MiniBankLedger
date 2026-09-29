
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Output;
using MiniBankLedger.Repositories.In_Memory;
using MiniBankLedger.Services;

namespace MiniBankLedger.Controllers;

class CustomerController(CustomerService customerService)
{
    public void CreateCustomer()
    {

        CustomerDto customerDto = CustomerInput.CreateCustomerInput();

        Customer? customer = customerService.CreateCustomer(customerDto);

        CustomerOutput.PrintCustomer(customer);

    }
}
