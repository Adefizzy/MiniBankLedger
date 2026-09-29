
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Input;
using MiniBankLedger.Services;

namespace MiniBankLedger.Controllers;

class CustomerController(CustomerService customerService)
{
    public CustomersResponse? CreateCustomer()
    {

        CustomerDto customerDto = CustomerInput.CreateCustomerInput();

        return customerService.CreateCustomer(customerDto);

    }

    public CustomersResponse? SearchCustomer()
    {
        string idOrEmail = CustomerInput.SearchCustomer();

        return customerService.SearchCustomer(idOrEmail);
    }

    public CustomersResponse[] AllCustomers()
    {
        return customerService.AllCustomers();
    }
}
