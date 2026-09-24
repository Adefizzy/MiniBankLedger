
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Output;
using MiniBankLedger.Repositories.In_Memory;

namespace MiniBankLedger.Controllers;

class CustomerController
{
    public static void CreateCustomer()
    {
        try
        {

            CustomerDto customerDto = CustomerInput.CreateCustomerInput();

            Customer? customer = CustomerStore.CreateCustomer(customerDto);

            if (customer is not null)
            {
                CustomerOut.PrintCustomer(customer);
            }

        }
        catch (UniqueExceptions e)
        {
            Console.WriteLine($"********{e.Message}*********");
        }
    }
}
